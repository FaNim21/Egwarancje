using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
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

    private async void LoadAddresses()
    {
        //var userAddresses = await _service.GetUserAddressesAsync();
    }

}
