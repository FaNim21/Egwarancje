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
    private readonly UserService service;
    [ObservableProperty] private string oldPassword;
    [ObservableProperty] private string password;
    [ObservableProperty] private string passwordAgain;

    public PasswordChangeViewModel(UserService service)
    {
        this.service = service;
    }

    [RelayCommand]
    public async Task ChangePassword()
    {
        if (Password.Equals(PasswordAgain) && OldPassword.Equals(service.User.Password))
        {
            service.User.Password = Password;
            bool success = await service.UpdateUserAsync();
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
