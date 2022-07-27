namespace _2022NetCoreWithReact07.DTOs.NasaImages.Input;

public class NasaQueryParameters
{
    public NasaQueryParameters(string query, string startYear, string endYear, string mediaType)
    {
        Query = query;
        StartYear = startYear;
        EndYear = endYear;
        MediaType = mediaType;
    }

    public string Query { get; set; }
    public string StartYear { get; set; }
    public string EndYear { get; set; }
    public string MediaType { get; set; }

    public override string ToString()
    {
        return $"Query={Query}, StartYear={StartYear}, EndYear={EndYear}, MediaType={MediaType}";
    }
}