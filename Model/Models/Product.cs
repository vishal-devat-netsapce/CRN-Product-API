using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    [JsonIgnore]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
