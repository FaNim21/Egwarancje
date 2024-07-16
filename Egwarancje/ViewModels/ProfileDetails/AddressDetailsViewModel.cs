using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using EgwarancjeDbLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.ViewModels.ProfileDetails;

public partial class AddressDetailsViewModel : BaseViewModel
{
    private readonly UserService _service;

    [ObservableProperty] private string? country;
    [ObservableProperty] private string? province;
    [ObservableProperty] private string? zipCode;
    [ObservableProperty] private string? city;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? number;

    public AddressDetailsViewModel(UserService service)
    {
        _service = service;
    }

    [RelayCommand]
    public async Task UpdateDetails()
    {
        if (!Country.IsNullOrEmpty() && !Province.IsNullOrEmpty() && !ZipCode.IsNullOrEmpty() && !City.IsNullOrEmpty() && !Street.IsNullOrEmpty() && !Number.IsNullOrEmpty())
        {
            //_service.User.Addresses.Address = Country;
            //_service.User.Province = Province;
            //_service.User.ZipCode = ZipCode;
            bool success = await _service.UpdateUserAsync();
            if (success)
            {
                await Application.Current!.MainPage!.DisplayAlert("Message", "Zmiany zostały zaaktualizowane", "OK");
            }
            await MopupService.Instance.PopAsync();
        }
        else
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Coś poszło nie tak", "OK");
        }
    }
}
