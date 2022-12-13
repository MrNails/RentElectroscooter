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
            => UserProfile != null && electroScooter != null && electroScooter.UserId == null;

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
                        Value = VehicleStatus.Available.ToString(),
                        ComprasionType = ComprasionType.AND
                    }
                };
                var electroScooters = await _electroScooterService.GetElectroScootersAsync(_session.Jwt, conditions);

                foreach (var scooter in electroScooters)
                    Items.Add(scooter);
            }
            catch (Exception ex)
            {
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
                    await Shell.Current.DisplayAlert("Error", "Error renting specified electro scooter. Try again!", "OK");
                }
                else
                    CurrentElement.UserId = _session.UserProfile.UserId;
            }
            catch (Exception ex)
            {
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

                var result = await _electroScooterService.RentElectroScooterAsync(_session.Jwt, electroScooter);

                if (result != string.Empty)
                {
                    _logger.LogError("Cannot rent scooter: {Message}", result);
                    await Shell.Current.DisplayAlert("Error", "Error renting specified electro scooter. Try again!", "OK");
                }
                else
                    CurrentElement.UserId = _session.UserProfile.UserId;
            }
            catch (Exception ex)
            {
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
        private void MoveToProfilePage(Layout contentViewContainer)
        {
            try
            {
                contentViewContainer.Children.Clear();
                contentViewContainer.ZIndex = 2;
                contentViewContainer.IsVisible = true;

                if (UserProfile == null)
                {
                    contentViewContainer.Children.Add(_serviceProvider.GetService<SignInContentView>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Move to profile error.");
            }
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
