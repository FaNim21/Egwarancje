﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.ViewModels.Orders;
using Egwarancje.Views.Orders;
using Egwarancje.Views.Warranties;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Warranties;

public partial class WarrantyCreationViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty] private Order selectedOrder;
    [ObservableProperty] private ObservableCollection<Order> orders = [];

    [ObservableProperty] private ObservableCollection<WarrantySpec> warrantySpecs = [];
    [ObservableProperty] private string? comment;
    [ObservableProperty] private DateTime dateOfWarranty;

    public Warranty warranty;


    public WarrantyCreationViewModel(UserService serivce)
    {
        this.service = serivce;

        Orders = new(serivce.User.Orders ?? []);

        /*
        this.order = order;
            OrderId = order.Id,
            Order = order!,
         */

        dateOfWarranty = DateTime.Now;
        warranty = new()
        {
            DateOfWarranty = DateOfWarranty,
            Status = WarrantyStatusType.Awaitng,
        };
    }

    public void AddWarranties(List<OrderSpec> orderSpecs)
    {
        WarrantySpecs.Clear();
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
    public async Task ShowOrderSpecsPanel()
    {
        if (SelectedOrder == null) return;
        await MopupService.Instance.PushAsync(new OrderSpecsView(new OrderSpecsViewModel(service, this, SelectedOrder)));
    }

    [RelayCommand]
    public async Task ShowSpecDetails(WarrantySpec warranty)
    {
        await MopupService.Instance.PushAsync(new WarrantySpecView(new WarrantySpecViewModel(warranty)));
    }

    [RelayCommand]
    public async Task Confirm()
    {
        bool result = await Application.Current!.MainPage!.DisplayAlert("Potwierdzenie", "Czy na pewno zgłosić tą gwarancję?", "Tak", "Nie");
        if (!result) return;

        warranty.Comments = Comment;
        Warranty? dbWarranty = await service.CreateWarrantyAsync(warranty);
        if (dbWarranty is null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Dodano", "Problem ze zgłoszeniem gwarancji", "OK");
            return;
        }

        dbWarranty.Order = selectedOrder;
        dbWarranty.OrderId = selectedOrder.Id;

        dbWarranty.WarrantySpecs ??= [];
        for (int i = 0; i < WarrantySpecs.Count; i++)
        {
            var spec = WarrantySpecs[i];
            WarrantySpec warrantySpec = new()
            {
                Comments = spec.Comments,
                OrderSpecId = spec.OrderSpecId,
                OrderSpec = spec.OrderSpec,
                WarrantyId = dbWarranty.Id,
                Warranty = dbWarranty,
            };

            WarrantySpec? dbSpec = await service.CreateWarrantySpecAsync(warrantySpec);
            if (dbSpec is null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Dodano", "Problem ze zgłoszeniem specfikacji gwarancji", "OK");
                return;
            }

            dbSpec.OrderSpecId = warrantySpec.OrderSpecId;
            dbSpec.OrderSpec = warrantySpec.OrderSpec;
            dbSpec.WarrantyId = warrantySpec.WarrantyId;
            dbSpec.Warranty = warrantySpec.Warranty;
            dbWarranty.WarrantySpecs.Add(dbSpec);

            dbSpec.Attachments ??= [];
            spec.Attachments ??= [];
            for (int j = 0; j < spec.Attachments.Count; j++)
            {
                var attachment = spec.Attachments[j];

                Attachment newAttachment = new()
                {
                    ImagePath = attachment.ImagePath,
                    WarrantySpecId = dbSpec.Id,
                    WarrantySpec = dbSpec,
                };

                Attachment? dbAttachment = await service.CreateAttachmentAsync(newAttachment);
                if (dbAttachment is null)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Dodano", "Problem ze zgłoszeniem załącznika w specfikacji gwarancji", "OK");
                    return;
                }

                dbAttachment.WarrantySpecId = newAttachment.WarrantySpecId;
                dbAttachment.WarrantySpec = newAttachment.WarrantySpec;
                dbSpec.Attachments.Add(dbAttachment);
            }
        }

        service.User.Warranties ??= [];
        service.User.Warranties.Add(dbWarranty);

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
