namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class Item
{
    public string? Href { get; set; }
    public List<Datum> Data { get; set; }
    public List<Link> Links { get; set; }

    public MinimalImageData GetMinimalData()
    {
        var firstData = Data.FirstOrDefault();
        var firstLink = Links.FirstOrDefault();
        
        return new MinimalImageData()
            {
                Title = firstData?.Title,
                Description = firstData?.Description,
                DateCreated = firstData?.DateCreated,
                Href = firstLink?.Href
            };
    }
}