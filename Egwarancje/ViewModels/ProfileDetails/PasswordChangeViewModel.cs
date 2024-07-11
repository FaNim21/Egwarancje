using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.ViewModels.ProfileDetails;

public partial class PasswordChangeViewModel : BaseViewModel
{
    private readonly UserService _service;
    [ObservableProperty] private string oldPassword;
    [ObservableProperty] private string password;
    [ObservableProperty] private string passwordAgain;

    public PasswordChangeViewModel(UserService service)
    {
        _service = service;
    }

    [RelayCommand]
    public async Task ChangePassword()
    {
        if (Password.Equals(PasswordAgain) && OldPassword.Equals(_service.User.Password))
        {
            _service.User.Password = Password;
            bool success = await _service.UpdateUserAsync();
            if (success)
            {
                await Application.Current!.MainPage!.DisplayAlert("Message", "Zmiany zostały zaaktualizowane", "OK");
            }
            await MopupService.Instance.PopAsync();
        }
        else
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Hasła nie są identyczne, lub stare hasło jest błędne", "OK");
        }
    }
}
