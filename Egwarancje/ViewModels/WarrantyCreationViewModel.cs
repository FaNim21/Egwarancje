using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

        Attachment attachment = new()
        {
            ImagePath = "",
            WarrantySpecId = 1
        };

        await database.Attachments.AddAsync(attachment);
        try
        {
            await database.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            Trace.WriteLine("ATTACHMENT");
            Trace.WriteLine($"DbUpdateException: {dbEx.Message}");
            Trace.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
            Trace.WriteLine(dbEx.InnerException?.StackTrace);

            foreach (var entry in database.ChangeTracker.Entries())
            {
                Trace.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
        }

        /*warranty.Comments = Comment;
        await database.Warranties.AddAsync(warranty);
        await database.SaveChangesAsync();

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
            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Trace.WriteLine("SPEC");
                Trace.WriteLine($"DbUpdateException: {dbEx.Message}");
                Trace.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
                Trace.WriteLine(dbEx.InnerException?.StackTrace);

                foreach (var entry in database.ChangeTracker.Entries())
                {
                    Trace.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                }
            }

            spec.Attachments ??= [];
            for (int j = 0; j < spec.Attachments.Count; j++)
            {
                var attachment = spec.Attachments[j];

                Attachment newAttachment = new()
                {
                    Image = attachment.Image,
                    WarrantySpecId = warrantySpec.Id,
                    WarrantySpec = warrantySpec,
                };

                await database.Attachments.AddAsync(newAttachment);
                try
                {
                    await database.SaveChangesAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    Trace.WriteLine("ATTACHMENT");
                    Trace.WriteLine($"DbUpdateException: {dbEx.Message}");
                    Trace.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
                    Trace.WriteLine(dbEx.InnerException?.StackTrace);

                    foreach (var entry in database.ChangeTracker.Entries())
                    {
                        Trace.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                    }
                }
            }

        }*/





        /*for (int i = 0; i < WarrantySpecs.Count; i++)
        {
            try
            {
                var current = WarrantySpecs[i];
                current.WarrantyId = warranty.Id;
                current.Warranty = warranty;
                await database.WarrantiesSpecs.AddAsync(current);
                //await database.SaveChangesAsync();

                if (current.Attachments != null && current.Attachments.Count != 0)
                {
                    for (int j = 0; j < current.Attachments.Count; j++)
                    {
                        var attachment = current.Attachments[j];
                        await database.Attachments.AddAsync(attachment);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }*/

        /*try
        {
            await database.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            Trace.WriteLine($"DbUpdateException: {dbEx.Message}");
            Trace.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
            Trace.WriteLine(dbEx.InnerException?.StackTrace);

            foreach (var entry in database.ChangeTracker.Entries())
            {
                Trace.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
        }*/


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


    /*[RelayCommand]
    public async Task Confirm()
    {
        bool result = await Application.Current!.MainPage!.DisplayAlert("Potwierdzenie", "Czy na pewno zgłosić tą gwarancję?", "Tak", "Nie");
        if (!result) return;

        warranty.Comments = Comment;
        await database.Warranties.AddAsync(warranty);
        await database.SaveChangesAsync();

        List<Attachment> attachments = [];
        for (int i = 0; i < warrantySpecs.Count; i++)
        {
            var current = WarrantySpecs[i];
            if (current.Attachments != null && current.Attachments.Count != 0)
                for (int j = 0; j < current.Attachments.Count; j++)
                    attachments.Add(current.Attachments[j]);
            current.Attachments = null;
        }

        for (int i = 0; i < WarrantySpecs.Count; i++)
        {
            try
            {
                var current = WarrantySpecs[i];
                current.WarrantyId = warranty.Id;
                current.Warranty = warranty;
                await database.WarrantiesSpecs.AddAsync(current);
                await database.SaveChangesAsync();

                *//*if (current.Attachments != null && current.Attachments.Count != 0)
                {
                    foreach (var attachment in current.Attachments)
                    {
                        attachment.WarrantySpecId = current.Id;
                        await database.Attachments.AddAsync(attachment);
                        await database.SaveChangesAsync();
                    }
                }*//*
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        for (int i = 0; i < attachments.Count; i++)
        {
            var current = attachments[i];
            current.WarrantySpecId = current.WarrantySpec!.Id;
            current.WarrantySpec.Attachments = [];
            current.WarrantySpec.Attachments.Add(current);
            await database.Attachments.AddAsync(current);
            await database.SaveChangesAsync();
        }

        await Application.Current!.MainPage!.DisplayAlert("Dodano", "Pomyślnie zgłoszono gwarancję", "OK");
        await Shell.Current.Navigation.PopAsync();
    }*/
}
