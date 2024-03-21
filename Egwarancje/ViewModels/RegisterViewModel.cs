using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using System.Diagnostics;

namespace Egwarancje.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private int phoneNumber;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string passwordAgain;


    public RegisterViewModel(LocalDatabaseContext database)
    {
        this.database = database;
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Login");
    }

    [RelayCommand]
    public async Task FinalizeRegister()
    {
        if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber.ToString()) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordAgain)) 
        {
            await App.Current!.MainPage!.DisplayAlert("Message", "Uzupełnij wszystkie dane wymagagane do rejestracji", "OK");
            return;
        }

        if(Password != PasswordAgain)
        {
            await App.Current!.MainPage!.DisplayAlert("Message", "Pola hasło oraz hasło ponownie nie są identyczne", "OK");
            return;
        }

        User? user = database.Users.FirstOrDefault(u => u.Email == Email);
        if (user == null)
        {
            User newUser = new User
            {
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Password = Password
            };

            database.Users.Add(newUser);

            await database.SaveChangesAsync();

            await App.Current!.MainPage!.DisplayAlert("Message", "Zarejestrowano pomyślnie użytkownika", "OK");
        }

        else
        {
            await App.Current!.MainPage!.DisplayAlert("Message", "Istnieje już użytkownik o podanym adresie e-mail", "OK");
            return;
        }

        await Shell.Current.GoToAsync("///Login");
    }
}
