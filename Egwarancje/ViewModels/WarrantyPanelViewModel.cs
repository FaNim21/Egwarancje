using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantyPanelViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private ObservableCollection<Warranty> warranties;

    public WarrantyPanelViewModel(LocalDatabaseContext database)
    {
        this.database = database;
    }
}

