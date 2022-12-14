using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using RentElectroScooter.CoreModels.Models;
using RentElectroScooter.UI.Services;
using RentElectroScooter.CoreModels.DTO;
using Microsoft.Extensions.Logging;
using RentElectroScooter.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using RentElectroScooter.UI.Views.Pages;

namespace RentElectroScooter.UI.ViewModels
{
    public sealed partial class MainPageVM : ItemManagerVM<ElectroScooter>
    {
        private readonly ElectroScooterService _electroScooterService;
        private readonly Session _session;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        private bool _isInternalLoad;

        public MainPageVM(ILogger logger, IServiceProvider serviceProvider, Session session, ElectroScooterService electroScooterService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _electroScooterService = electroScooterService;
            _session = session;
          
            _session.PropertyChanged += (s, arg) =>
            {
                if (arg.PropertyName == nameof(Session.UserProfile))
                    OnPropertyChanged(nameof(MainPageVM.UserProfile));
            };

            Items.CollectionChanged += Items_CollectionChanged;
        }

        public UserProfile UserProfile => _session.UserProfile;

        public bool CanSetSelectedIndex(int idx) => idx >= -1 && idx < Items.Count;

        public bool CanRentElectroScooter(ElectroScooter electroScooter)
            => UserProfile != null && electroScooter != null && electroScooter.UserId == null &&
            Items.FirstOrDefault(i => i.UserId == UserProfile.UserId) == null;

        public bool CanReturnElectroScooter(ElectroScooter electroScooter)
            => UserProfile != null && electroScooter != null && electroScooter.UserId == UserProfile.UserId;

        public bool CanMoveToProfilePage(Layout contentViewContainer) => contentViewContainer != null;

        [RelayCommand]
        private async Task LoadElectroScooters()
        {
            try
            {
                IsBusy = _isInternalLoad = true;

                var conditions = new List<FieldCondition>
                {
                    new FieldCondition
                    {
                        FieldName = nameof(ElectroScooter.Status),
                        Value = ((int)VehicleStatus.Available).ToString(),
                        ComprasionType = ComprasionType.OR
                    }
                };

                if (_session.UserProfile != null)
                {
                    conditions.Add(new FieldCondition
                    {
                        FieldName = nameof(ElectroScooter.UserId),
                        Value = _session.UserProfile.UserId.ToString(),
                        ComprasionType = ComprasionType.AND
                    });
                }

                var electroScooters = await _electroScooterService.GetElectroScootersAsync(_session.Jwt, conditions);

                Items.Clear();

                foreach (var scooter in electroScooters)
                    Items.Add(scooter);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error loading electro scooters.", "OK")
                    .ConfigureAwait(false);

                _logger.LogError(ex, "Error loading electro scooters.");
            }
            finally
            {
                IsBusy = _isInternalLoad = false;
            }
        }

        [RelayCommand(CanExecute = nameof(CanRentElectroScooter))]
        private async Task RentElectroScooter(ElectroScooter electroScooter)
        {
            try
            {
                IsBusy = _isInternalLoad = true;

                var result = await _electroScooterService.RentElectroScooterAsync(_session.Jwt, electroScooter);

                if (result != string.Empty)
                {
                    _logger.LogError("Cannot rent scooter: {Message}", result);
                    await App.Current.MainPage.DisplayAlert("Error", "Error renting specified electro scooter. Try again!", "OK");
                }
                else
                {
                    CurrentElement.UserId = _session.UserProfile.UserId;
                    _session.UserProfile.Balance -= electroScooter.AdditionalData.PricePerTime;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error renting specified electro scooter. Try again!", "OK");

                _logger.LogError(ex, "Error renting electro scooter {ElectroScooterId}.", CurrentElement.Id);
            }
            finally
            {
                IsBusy = _isInternalLoad = false;
            }
        }

        [RelayCommand(CanExecute = nameof(CanReturnElectroScooter))]
        private async Task ReturnElectroScooter(ElectroScooter electroScooter)
        {
            try
            {
                IsBusy = _isInternalLoad = true;

                var result = await _electroScooterService.ReturnElectroScooterAsync(_session.Jwt, electroScooter);

                if (result != string.Empty)
                {
                    _logger.LogError("Cannot return scooter: {Message}", result);
                    await App.Current.MainPage.DisplayAlert("Error", "Error returning specified electro scooter. Try again!", "OK");
                }
                else
                    CurrentElement.UserId = null;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error returning specified electro scooter. Try again!", "OK");

                _logger.LogError(ex, "Error renting electro scooter {ElectroScooterId}.", CurrentElement.Id);
            }
            finally
            {
                IsBusy = _isInternalLoad = false;
            }
        }

        [RelayCommand(CanExecute = nameof(CanSetSelectedIndex))]
        private void SetSelectedIndex(int idx) => CurrentIndex = idx;

        [RelayCommand]
        private void SetSelectedItem(ElectroScooter electroScooter) => CurrentIndex = Items.IndexOf(electroScooter);

        [RelayCommand(CanExecute = nameof(CanMoveToProfilePage))]
        private async Task MoveToProfilePage(Layout contentViewContainer)
        {
            try
            {
                contentViewContainer.Children.Clear();
                contentViewContainer.ZIndex = 2;
                contentViewContainer.IsVisible = true;

                //if (_session.UserProfile == null)
                //{
                //    _session.UserProfile = new UserProfile
                //    {
                //        Name = "Test",
                //        Balance = 100,
                //        RegistrationAt = DateTime.Now.AddMinutes(-Random.Shared.Next(1000, 30000)),
                //        TotalDrivenTime = TimeSpan.FromMinutes(Random.Shared.Next(30, 600)),
                //        TotalDrivenDistance = (float)Random.Shared.NextDouble() * 10,
                //        UserId = Guid.NewGuid(),
                //    };
                //}

                if (UserProfile == null)
                {
                    var authCV = _serviceProvider.GetService<SignInContentView>();
                    var regCV = _serviceProvider.GetService<RegisterContentView>();

                    authCV.UserVM.Authorized = regCV.UserVM.Authorized =
                        async up =>
                        {
                            await Shell.Current.GoToAsync(new ShellNavigationState(nameof(UserProfilePage)), true);
                            contentViewContainer.Children.Clear();
                            contentViewContainer.ZIndex = -1;
                            contentViewContainer.IsVisible = false;
                        };

                    authCV.MoveToRegistrationViewCommand ??= new Command(obj =>
                    {
                        regCV.MoveToAuthorizationViewCommand ??= new Command(obj =>
                        {
                            contentViewContainer.Children.Clear();
                            contentViewContainer.Children.Add(authCV);
                        });

                        contentViewContainer.Children.Clear();
                        contentViewContainer.Children.Add(regCV);
                    });

                    contentViewContainer.Children.Add(authCV);
                }
                else
                {
                    await Shell.Current.GoToAsync(new ShellNavigationState(nameof(UserProfilePage)), true);
                    contentViewContainer.Children.Clear();
                    contentViewContainer.ZIndex = -1;
                    contentViewContainer.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured while moving to profile page.", "OK");

                _logger.LogError(ex, "Move to profile error.");
            }
        }

        [RelayCommand]
        private async Task MoveToESAdditionalInfoPage(ElectroScooter electroScooter)
        {
            await Shell.Current.GoToAsync(new ShellNavigationState(nameof(ESAdditionalInfoPage)), true,
                new Dictionary<string, object>
                {
                    { "AdditionalInfo", electroScooter.AdditionalData }
                });
        }

        [RelayCommand]
        private async Task LogOut()
        {
            _session.UserProfile = null;
            _session.Jwt = string.Empty;

            _ = LoadElectroScooters().ConfigureAwait(false);
            await App.Current.MainPage.DisplayAlert("Information", "Logged out", "OK");
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_isInternalLoad)
                return;

            //switch (e.Action)
            //{
            //    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
            //        foreach (ElectroScooter scooter in e.NewItems)
            //            _dbContext.ElectroScooters.Add(scooter);
            //        break;
            //    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
            //        foreach (ElectroScooter scooter in e.OldItems)
            //            _dbContext.ElectroScooters.Remove(scooter);
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
