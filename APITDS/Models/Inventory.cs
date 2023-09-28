using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITDS.Models;

public partial class Inventory
{
    public int Id { get; set; }
    public int? StoreId { get; set; }
    public int? EmployeeId { get; set; }

    public DateTime Date { get; set; }

    public string Flavor { get; set; } = null!;

    public bool IsSeasonFlavor { get; set; }

    public int Quantity { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Store? Store { get; set; }
}


