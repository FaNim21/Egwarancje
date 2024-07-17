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
    private readonly AddressViewModel _addressViewModel;

    private Address? _address;

    [ObservableProperty] private string? country;
    [ObservableProperty] private string? province;
    [ObservableProperty] private string? zipCode;
    [ObservableProperty] private string? city;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? number;

    public AddressDetailsViewModel(UserService service, AddressViewModel addressViewModel)
    {
        _service = service;
        _addressViewModel = addressViewModel;
    }

    public AddressDetailsViewModel(UserService service, Address address, AddressViewModel addressViewModel) : this(service, addressViewModel)
    {
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

                var address = await _service.CreateAddress(newAddress);
                _service.User.Addresses ??= [];
                _service.User.Addresses.Add(address);
            }

            _addressViewModel.LoadAddresses();
            await MopupService.Instance.PopAsync();
        }
        else
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Coś poszło nie tak", "OK");
        }
    }
}
