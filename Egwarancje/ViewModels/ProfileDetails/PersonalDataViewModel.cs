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

public partial class PersonalDataViewModel : BaseViewModel
{
    private readonly UserService service;
    [ObservableProperty] private string name;
    [ObservableProperty] private string email;
    [ObservableProperty] private string phoneNumber;
    [ObservableProperty] private string? nip;
    [ObservableProperty] private string? companyName;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? zipCode;
    [ObservableProperty] private string? country;

    public PersonalDataViewModel(UserService service)
    {
        this.service = service;
        Name = service.User.Name;
        Email = service.User.Email;
        PhoneNumber = service.User.PhoneNumber.ToString();
    }

    [RelayCommand]
    public async Task SaveUserData()
    {
        service.User.Name = Name;
        service.User.Email = Email;
        service.User.PhoneNumber = int.Parse(PhoneNumber);

        bool success = await service.UpdateUserAsync();
        if (success)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Zmiany zostały zaaktualizowane", "OK");
        }
        await MopupService.Instance.PopAsync();
    }
}
