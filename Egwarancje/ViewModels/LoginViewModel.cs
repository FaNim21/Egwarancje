using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
//using Egwarancje.Context;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using System.Diagnostics;

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
        //List<User> users = database.Users.ToList();
        //Trace.WriteLine(users.Count);
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await App.Current!.MainPage!.DisplayAlert("Message", "Uzupełnij dane logowania", "OK");
            return;
        }
        User? user = database.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

        if (user == null)
        {
            await App.Current!.MainPage!.DisplayAlert("Message", "Nieprawidłowe dane logowania", "OK");
            return;
        }

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
