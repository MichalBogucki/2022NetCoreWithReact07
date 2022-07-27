namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class MinimalImageData
{
    public string? Title { get; }
    public string? Description { get; }
    public string? Href { get; }

    public MinimalImageData(string? title, string? description, string? href)
    {
        Title = title;
        Description = description;
        Href = href;
    }
}