using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.CoreModels.Models;
using RentElectroScooter.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.ViewModels
{
    public partial class UserVM : BindableClass
    {
        private readonly ILogger _logger;
        private readonly Session _session;
        private readonly UserService _userService;

        public UserVM(ILogger logger, Session session, UserService userService)
        {
            _logger = logger;
            _session = session;
            _userService = userService;
        }

        public bool CanAuthorize(AuthData authData) => authData != null;

        public UserProfile UserProfile => _session.UserProfile;

        public Action<UserProfile> Authorized { get; set; }

        [RelayCommand(CanExecute = nameof(CanAuthorize))]
        private async Task Authorize(AuthData authData)
        {
            var error = authData.Error;
            if (error != string.Empty)
            {
                await App.Current.MainPage.DisplayAlert("Error", error, "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var authRes = await _userService.Authenticate(authData);

                if (authRes.Item2 != System.Net.HttpStatusCode.OK)
                {
                    if (authRes.Item2 != System.Net.HttpStatusCode.Conflict &&
                        authRes.Item2 != System.Net.HttpStatusCode.Unauthorized)
                    _logger.LogError("Authorization error. {CodeText}({Code}): {Message}", 
                        authRes.Item2.ToString(), 
                        ((int)authRes.Item2).ToString(),
                        authRes.Item1);

                    await App.Current.MainPage.DisplayAlert("Error", $"Cannot authorized. {authRes.Item1}", "OK");
                }
                else
                {
                    _session.Jwt = authRes.Item1;

                    var userProfileRes = await _userService.GetProfile(_session.Jwt);

                    if (userProfileRes.Item2 != System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogError("Cannot retreive user profile. {CodeText}({Code})",
                            userProfileRes.Item2.ToString(), ((int)authRes.Item2).ToString());

                        await App.Current.MainPage.DisplayAlert("Error", $"Cannot retreive user profile.", "OK");
                    }
                    else
                    {
                        _session.UserProfile = userProfileRes.Item1;
                        Authorized?.Invoke(_session.UserProfile);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured while sending authorizing request.\nTry again later.", "OK");

                _logger.LogError(ex, "An error occured while authorizing.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Register(RegisterData regData)
        {
            try
            {
                IsBusy = true;

                var authRes = await _userService.Register(regData);

                if (authRes.Item2 != System.Net.HttpStatusCode.OK)
                {
                    if (authRes.Item2 != System.Net.HttpStatusCode.Conflict)
                        _logger.LogError("Registration error. {CodeText}({Code}): {Message}",
                            authRes.Item2.ToString(),
                            ((int)authRes.Item2).ToString(),
                            authRes.Item1);

                    await App.Current.MainPage.DisplayAlert("Error", $"Cannot register. {authRes.Item1}", "OK");
                }
                else
                {
                    _session.Jwt = authRes.Item1;

                    var userProfileRes = await _userService.GetProfile(_session.Jwt);

                    if (userProfileRes.Item2 != System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogError("Cannot retreive user profile. {CodeText}({Code})",
                            userProfileRes.Item2.ToString(), ((int)authRes.Item2).ToString());

                        await App.Current.MainPage.DisplayAlert("Error", $"Cannot retreive user profile.", "OK");
                    }
                    else
                    {
                        _session.UserProfile = userProfileRes.Item1;
                        Authorized?.Invoke(_session.UserProfile);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured while sending registration request.\nTry again later.", "OK");

                _logger.LogError(ex, "An error occured while authorizing.");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
