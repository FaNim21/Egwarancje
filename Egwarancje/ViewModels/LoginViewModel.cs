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
        List<User> users = database.Users.ToList();
        Trace.WriteLine(users.Count);

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
