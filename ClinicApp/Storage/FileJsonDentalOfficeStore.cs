
using System.Text.Json;

namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO JSON
public class FileJsonDentalOfficeStore : IDentalOfficeStore
{
    private const string FilePath = "offices.json";

    private class DentalOfficeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
    }

    public void Add(DentalOffice office)
    {
        var list = ReadDtos();
        list.Add(new DentalOfficeDto { Id = office.GetId(), Name = office.GetName() });

        File.WriteAllText(FilePath, JsonSerializer.Serialize(list));
    }

    public IEnumerable<DentalOffice> GetAll()
    {
        return ReadDtos().Select(d => new DentalOffice(d.Id, d.Name));
    }

    public DentalOffice GetById(Guid id)
    {
        foreach (var office in GetAll())
        {
            if (office.GetId() == id)
            {
                return office;
            }
        }
        return null;
    }

    private List<DentalOfficeDto> ReadDtos()
    {
        if (!File.Exists(FilePath))
        {
            return new List<DentalOfficeDto>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<DentalOfficeDto>();
        }

        return JsonSerializer.Deserialize<List<DentalOfficeDto>>(json) ?? new List<DentalOfficeDto>();
    }
}
