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

    private Address? _address;

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

    public AddressDetailsViewModel(UserService service, Address address) : this(service)
    {
        _service = service;
        _address = address;

        if (address != null)
        {
            Country = address.Country;
            Province = address.Province;
            ZipCode = address.ZipCode;
            City = address.City;
            Street = address.Street;
            Number = address.Number;
        }
    }

    [RelayCommand]
    public async Task UpdateDetails()
    {
        if (!Country.IsNullOrEmpty() && !Province.IsNullOrEmpty() && !ZipCode.IsNullOrEmpty() && !City.IsNullOrEmpty() && !Street.IsNullOrEmpty() && !Number.IsNullOrEmpty())
        {
            if (_address != null)
            {
                _address.Country = Country;
                _address.City = City;
                _address.ZipCode = ZipCode;
                _address.Street = Street;
                _address.Number = Number;
            }
            else
            {
                var newAddress = new Address
                {
                    Country = Country,
                    Province = Province,
                    ZipCode = ZipCode,
                    City = City,
                    Street = Street,
                    Number = Number
                };
                if (_service.User.Addresses == null)
                {
                    _service.User.Addresses = new List<Address>();
                }

                _service.User.Addresses.Add(newAddress);
            }

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
