namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class Datum
{
    public string Center { get; set; }
    public string Title { get; set; }
    public List<string> Keywords { get; set; }
    public string NasaId { get; set; }
    public DateTime DateCreated { get; set; }
    public string MediaType { get; set; }
    public string Description { get; set; }
    public string Description508 { get; set; }
    public string SecondaryCreator { get; set; }
    public string Location { get; set; }
    public List<string> Album { get; set; }
    public string Photographer { get; set; }
}