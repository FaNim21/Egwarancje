using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class NotificationsView
{
	public NotificationsView(NotificationsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}