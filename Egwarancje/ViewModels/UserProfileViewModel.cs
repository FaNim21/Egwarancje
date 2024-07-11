using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using Egwarancje.ViewModels.ProfileDetails;
using Egwarancje.Views.ProfileDetails;

namespace Egwarancje.ViewModels;

public partial class UserProfileViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty] private User? user;


    public UserProfileViewModel(UserService service)
    {
        this.service = service;
        User = service.User;
    }

    [RelayCommand]
    public async Task Save()
    {
        bool success = await service.UpdateUserAsync();
        if (success)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Zmiany zostały zaaktualizowane", "OK");
        }
    }

    [RelayCommand]
    public async Task Logout()
    {
        Preferences.Set("RememberMe", false);
        Preferences.Remove("email");
        Preferences.Remove("password");
        service.Logout();
        await Shell.Current.GoToAsync("///Home");
    }

    [RelayCommand]
    public async Task OpenPersonalData()
    {
        await MopupService.Instance.PushAsync(new PersonalDataView(new PersonalDataViewModel(service)));
    }

    [RelayCommand]
    public async Task OpenPasswordChange()
    {
        await MopupService.Instance.PushAsync(new PasswordChangeView(new PasswordChangeViewModel(service)));
    }
}
