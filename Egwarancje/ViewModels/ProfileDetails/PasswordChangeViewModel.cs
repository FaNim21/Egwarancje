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
    [ObservableProperty] private string password;
    [ObservableProperty] private string passwordAgain;

    public PasswordChangeViewModel(UserService service)
    {
        this.service = service;
        Password = service.User.Password;
        PasswordAgain = service.User.Password;
    }

    [RelayCommand]
    public async Task ChangePassword()
    {
        if (password == passwordAgain)
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
            await Application.Current!.MainPage!.DisplayAlert("Message", "Hasła nie są identyczne", "OK");
        }
    }
}
