using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.ViewModels;

public partial class UserProfileViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty] private User? user;


    public UserProfileViewModel(LocalDatabaseContext database)
    {
        this.database = database;
        user = database.User;
    }

    [RelayCommand]
    public async Task Logout()
    {
        database.User = null;
        await Shell.Current.GoToAsync("///Login");
    }
}
