namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class Item
{
    public string Href { get; set; }
    public List<Datum> Data { get; set; }
    public List<Link> Links { get; set; }
}