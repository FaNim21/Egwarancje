using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Egwarancje.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private string? email;

    [ObservableProperty]
    private string? password;


    public LoginViewModel(LocalDatabaseContext database)
    {
        this.database = database;
    }

    [RelayCommand]
    public async Task Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Uzupełnij dane logowania", "OK");
            return;
        }

        User? user = database.Users
            .Include(u => u.Orders!).ThenInclude(o => o.OrderSpecs)
            .FirstOrDefault(u => u.Email.Equals(Email) && u.Password.Equals(Password));
        if (user == null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Nieprawidłowe dane logowania", "OK");
            return;
        }

        database.User = user;

        await Shell.Current.GoToAsync("///MainTab//OrderPanel");
    }

    [RelayCommand]
    public async Task Register()
    {
        await Shell.Current.GoToAsync("///Register");
    }

    [RelayCommand]
    public async Task ResetPassword()
    {
        await Shell.Current.GoToAsync("///Recover");
    }
}
