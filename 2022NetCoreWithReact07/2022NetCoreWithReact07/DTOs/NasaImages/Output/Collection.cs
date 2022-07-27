namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class Collection
{
    public string Version { get; set; }
    public string Href { get; set; }
    public List<Item> Items { get; set; }
    public Metadata Metadata { get; set; }
    public List<Link> Links { get; set; }
}