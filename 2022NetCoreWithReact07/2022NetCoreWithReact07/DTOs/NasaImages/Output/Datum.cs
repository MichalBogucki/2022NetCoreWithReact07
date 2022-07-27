using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace _2022NetCoreWithReact07.DTOs.NasaImages.Output;

public class Datum
{
    public string Center { get; set; }
    public string? Title { get; set; }
    public List<string> Keywords { get; set; }

    [JsonProperty("nasa_id")]
    public string NasaId { get; set; }

    [JsonProperty("date_created")] 
    public DateTime? DateCreated { get; set; }

    [JsonProperty("media_type")]
    public string MediaType { get; set; }

    public string? Description { get; set; }

    [JsonProperty("description_508")]
    public string Description508 { get; set; }

    [JsonProperty("secondary_creator")]
    public string SecondaryCreator { get; set; }
    public string Location { get; set; }
    public List<string> Album { get; set; }
    public string Photographer { get; set; }
}