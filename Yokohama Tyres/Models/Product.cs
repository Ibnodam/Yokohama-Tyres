using System;
using System.Collections.Generic;

namespace Yokohama_Tyres.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Article { get; set; } = null!;

    public decimal? MinPriceForAgent { get; set; }

    public string? ImagePath { get; set; }

    public int ProductTypeId { get; set; }

    public int? PeopleCount { get; set; }

    public int? WorkshopNumber { get; set; }

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();

    public virtual ProductType ProductType { get; set; } = null!;
}
