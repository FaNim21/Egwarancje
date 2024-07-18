using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views.ProfileDetails;
using EgwarancjeDbLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.ViewModels.ProfileDetails;

public partial class AddressViewModel : BaseViewModel
{
    private readonly UserService _service;

    [ObservableProperty] private ObservableCollection<Address> addresses = new();

    public AddressViewModel(UserService service)
    {
        _service = service;
        LoadAddresses();
    }

    public void LoadAddresses()
    {
        Addresses.Clear();
        var userAddresses = _service.User.Addresses;
        if (userAddresses != null)
        {
            foreach (var address in userAddresses)
            {
                Addresses.Add(address);
            }
        }
    }

    [RelayCommand]
    private async Task AddNewAddress()
    {
        await MopupService.Instance.PushAsync(new AddressDetailsView(new AddressDetailsViewModel(_service, this)));
    }

    [RelayCommand]
    private async Task EditAddress(Address address)
    {
        await MopupService.Instance.PushAsync(new AddressDetailsView(new AddressDetailsViewModel(_service, address, this)));
    }

    [RelayCommand]
    private async Task DeleteAddress(Address address)
    {
        await _service.DeleteAddressAsync(address);
        _service.User.Addresses?.Remove(address);
        LoadAddresses();
    }

}
