namespace MastersAggregatorService.Models;

public class User : BaseModel
{
    private bool Equals(User other)
    {
        return Pfone == other.Pfone;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return Pfone.GetHashCode();
    }

    public string Name { get; set; }
    public string FirstName { get; set; }
    public string Pfone { get; set; }
}