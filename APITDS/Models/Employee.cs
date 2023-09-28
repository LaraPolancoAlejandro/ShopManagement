using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APITDS.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
