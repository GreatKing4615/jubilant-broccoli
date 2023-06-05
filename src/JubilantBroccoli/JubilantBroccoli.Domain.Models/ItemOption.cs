using JubilantBroccoli.Domain.Core.Contracts;
using JubilantBroccoli.Domain.Core.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace JubilantBroccoli.Domain.Models;

public class ItemOption : IHaveId, IEquatable<ItemOption>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<Item> Items { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ItemType Type { get; set; }

    public bool Equals(ItemOption? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name && Type == other.Type && Items.Equals(other.Items);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ItemOption)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, (int)Type, Items);
    }
}