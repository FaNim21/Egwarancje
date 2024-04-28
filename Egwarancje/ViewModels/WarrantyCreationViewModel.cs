using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantyCreationViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;
    public readonly Order order;

    public Warranty warranty;

    [ObservableProperty] private ObservableCollection<WarrantySpec> warrantySpecs = [];
    [ObservableProperty] private string? comment;
    [ObservableProperty] private WarrantyStatusType? status;
    [ObservableProperty] private DateTime dateOfWarranty;


    public WarrantyCreationViewModel(LocalDatabaseContext database, Order order, List<OrderSpec> orderSpecs)
    {
        this.database = database;
        this.order = order;

        dateOfWarranty = DateTime.Now;
        warranty = new()
        {
            DateOfWarranty = DateOfWarranty,
            Status = WarrantyStatusType.Awaitng,
            OrderId = order.Id,
            Order = order!,
        };

        for (int i = 0; i < orderSpecs.Count; i++)
        {
            var currentOrderSpec = orderSpecs[i];

            WarrantySpec warrantySpec = new()
            {
                OrderSpecId = currentOrderSpec.Id,
                OrderSpec = currentOrderSpec,
                Comments = ""
            };
            WarrantySpecs.Add(warrantySpec);
        }
    }

    [RelayCommand]
    public async Task ShowSpecDetails(WarrantySpec warranty)
    {
        await MopupService.Instance.PushAsync(new WarrantySpecView(new WarrantySpecViewModel(database, warranty)));
    }

    [RelayCommand]
    public async Task Confirm()
    {
        bool result = await Application.Current!.MainPage!.DisplayAlert("Potwierdzenie", "Czy na pewno zgłosić tą gwarancję?", "Tak", "Nie");
        if (!result) return;

        warranty.Comments = Comment;
        await database.Warranties.AddAsync(warranty);

        for (int i = 0; i < WarrantySpecs.Count; i++)
        {
            var spec = WarrantySpecs[i];
            WarrantySpec warrantySpec = new()
            {
                Comments = spec.Comments,
                OrderSpecId = spec.OrderSpecId,
                OrderSpec = spec.OrderSpec,
                WarrantyId = warranty.Id,
                Warranty = warranty,
            };

            await database.WarrantiesSpecs.AddAsync(warrantySpec);

            spec.Attachments ??= [];
            for (int j = 0; j < spec.Attachments.Count; j++)
            {
                var attachment = spec.Attachments[j];

                Attachment newAttachment = new()
                {
                    ImagePath = attachment.ImagePath,
                    WarrantySpecId = warrantySpec.Id,
                    WarrantySpec = warrantySpec,
                };

                await database.Attachments.AddAsync(newAttachment);
            }
        }

        await database.SaveChangesAsync();

        await Application.Current!.MainPage!.DisplayAlert("Dodano", "Pomyślnie zgłoszono gwarancję", "OK");
        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    public async Task Back()
    {
        bool result = await Application.Current!.MainPage!.DisplayAlert("Potwierdzenie", "Czy na pewno chcesz wrócić do zamówień?", "Tak", "Nie");
        if (!result) return;

        await Shell.Current.Navigation.PopAsync();
    }
}
