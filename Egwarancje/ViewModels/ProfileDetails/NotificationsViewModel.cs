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

public partial class NotificationsViewModel : BaseViewModel
{
    private readonly UserService _service;

    public NotificationsViewModel(UserService service)
    {
        _service = service;
    }
}
