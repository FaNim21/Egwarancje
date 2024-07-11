using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Microsoft.IdentityModel.Tokens;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.ViewModels.ProfileDetails;

public partial class AddressViewModel : BaseViewModel
{
    private readonly UserService _service;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? zipCode;
    [ObservableProperty] private string? country;

    public AddressViewModel(UserService service)
    {
        _service = service;
    }

    [RelayCommand]
    public async Task UpdateDetails()
    {
        if (!Street.IsNullOrEmpty() && !ZipCode.IsNullOrEmpty() && !Country.IsNullOrEmpty())
        {
            //_service.User.Street = Street;
            //_service.User.ZipCode = ZipCode;
            //_service.User.Country = Country;
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
