using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? ImagePath { get; set; }
    public string? Structure { get; set; }
}

public class ProductConfigurator
{
    public string? Name { get; set; }
    public string? ImagePath { get; set; }
    public ProductStructure? Structure { get; set; }
}

public struct ProductStructure
{
    public Resources[] Resources { get; set; }
}

public class Resources
{
    public string Name { get; set; }

    public string SelectedMaterials { get; set; } = string.Empty;
    public string SelectedColor { get; set; } = string.Empty;

    public string?[] Materials { get; set; }
    public string?[] Colors { get; set; }
}

