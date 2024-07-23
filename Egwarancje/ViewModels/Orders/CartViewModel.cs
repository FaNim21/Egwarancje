using Egwarancje.Services;

namespace Egwarancje.ViewModels.Orders;

public partial class CartViewModel : BaseViewModel
{
    private readonly UserService _userService;


    public CartViewModel(UserService userService)
    {
        _userService = userService;
    }
}
