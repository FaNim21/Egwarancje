using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using EgwarancjeDbLibrary.Models;
using System.Text.RegularExpressions;

namespace Egwarancje.ViewModels.Auth;

public partial class RegisterViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty] private string? name;
    [ObservableProperty] private string? email;
    [ObservableProperty] private string? phoneNumber;
    [ObservableProperty] private string? password;
    [ObservableProperty] private string? passwordAgain;
    [ObservableProperty] private string? nip;
    [ObservableProperty] private string? companyName;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? zipCode;
    [ObservableProperty] private string? country;

    [ObservableProperty] private bool isCompanyAccount;


    public RegisterViewModel(UserService service)
    {
        this.service = service;
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Home");
    }

    [RelayCommand]
    public async Task FinalizeRegister()
    {
        if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordAgain))
        {
            await Application.Current!.MainPage!.DisplayAlert("Niepełne dane", "Uzupełnij wszystkie dane wymagagane do rejestracji", "OK");
            return;
        }

        if (!IsValidEmail(Email))
        {
            await Application.Current!.MainPage!.DisplayAlert("Niepoprawny adres email", "Podano niepoprawny adres email. Poprawny format to: example@example.com", "OK");
            return;
        }

        if (PhoneNumber.ToString().Length != 9)
        {
            await Application.Current!.MainPage!.DisplayAlert("Niepoprawny numer telefonu", "Numer telefonu musi składać się z 9 cyfr", "OK");
            return;
        }

        if (!Password.Equals(PasswordAgain))
        {
            await Application.Current!.MainPage!.DisplayAlert("Hasła", "Pola hasło oraz hasło ponownie nie są identyczne", "OK");
            return;
        }

        User newUser = new()
        {
            Name = Name,
            Email = Email,
            PhoneNumber = int.Parse(PhoneNumber),
            Password = Password
        };

        bool success = await service.RegisterAsync(newUser);
        if (!success)
        {
            await Application.Current!.MainPage!.DisplayAlert("Rejestracja", "Istnieje już użytkownik o podanym adresie e-mail", "OK");
            return;
        }

        await Application.Current!.MainPage!.DisplayAlert("Rejestracja", "Zarejestrowano pomyślnie użytkownika", "OK");
        await Shell.Current.GoToAsync("///Home");
    }

    private bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }
}
