namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class MinimalImageData : IEquatable<MinimalImageData>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DateCreated { get; set; }
    public string? Href { get; set; }

    public bool Equals(MinimalImageData? other)
    {
        var allMatch = new List<bool>();

        allMatch.Add(string.Equals(this.Title, other.Title));
        allMatch.Add(string.Equals(this.Description, other.Description));
        allMatch.Add(DateTime.Equals(this.DateCreated, other.DateCreated));
        allMatch.Add(string.Equals(this.Href, other.Href));

        return allMatch.All(x => x);
    }
}