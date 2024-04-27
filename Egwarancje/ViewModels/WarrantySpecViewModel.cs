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

public partial class WarrantySpecViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;
    private readonly WarrantySpec warrantySpec;
    private readonly WarrantyDetailsViewModel warrantyDetails;

    public WarrantySpecViewModel(LocalDatabaseContext database, WarrantyDetailsViewModel warrantyDetails, WarrantySpec warrantySpec)
    {
        this.database = database;
        this.warrantyDetails = warrantyDetails;
        this.warrantySpec = warrantySpec;
    }
}
