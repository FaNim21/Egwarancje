using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;

namespace Egwarancje.ViewModels;

public partial class UserProfileViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;


    public UserProfileViewModel(LocalDatabaseContext database)
    {
        this.database = database;
    }

    [RelayCommand]
    public async Task Logout()
    {
        database.User = null;
        await Shell.Current.GoToAsync("///Login");
    }
}
