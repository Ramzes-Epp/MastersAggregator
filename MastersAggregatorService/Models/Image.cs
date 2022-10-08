namespace MastersAggregatorService.Models;

public class Image : BaseModel
{
    private bool Equals(Image other)
    {
        return ImageUrl == other.ImageUrl;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Image)obj);
    }

    public override int GetHashCode()
    {
        return ImageUrl.GetHashCode();
    }

    public string ImageUrl { get; set; }
    public string ImageDescription { get; set; } = "Описание по умолчанию";
}