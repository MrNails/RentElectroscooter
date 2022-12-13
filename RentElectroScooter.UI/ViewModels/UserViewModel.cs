using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.ViewModels
{
    public partial class UserViewModel : BindableClass
    {
        private readonly ILogger _logger;
        private readonly Session _session;
        private readonly UserService _userService;

        public UserViewModel(ILogger logger, Session session, UserService userService)
        {
            _logger = logger;
            _session = session;
            _userService = userService;
        }

        [RelayCommand]
        private async Task Authorize(AuthData authData)
        {
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

                    await Shell.Current.DisplayAlert("Error", $"Cannot authorized. {authRes.Item1}", "OK");
                }
                else
                {
                    _session.Jwt = authRes.Item1;

                    
                }
            }
            catch (Exception ex)
            {
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

                    await Shell.Current.DisplayAlert("Error", $"Cannot register. {authRes.Item1}", "OK");
                }
                else
                {
                    _session.Jwt = authRes.Item1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while authorizing.");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
