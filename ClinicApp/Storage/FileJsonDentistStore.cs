
using System.Text.Json;

namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO JSON
public class FileJsonDentistStore : IDentistStore
{
    private const string FilePath = "dentists.json";

    private class DentistDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }

    void IDentistStore.Add(Dentist dentist)
    {
        var list = ReadDtos();
        list.Add(new DentistDto
        {
            Id = dentist.GetId(),
            Name = dentist.GetName(),
            Email = dentist.GetEmail()
        });

        File.WriteAllText(FilePath, JsonSerializer.Serialize(list));
    }

    IEnumerable<Dentist> IDentistStore.GetAll()
    {
        return ReadDtos().Select(d => new Dentist(d.Id, d.Name, d.Email));
    }

    Dentist IDentistStore.GetyById(Guid id)
    {
        foreach (var dentist in ((IDentistStore)this).GetAll())
        {
            if (dentist.GetId() == id)
            {
                return dentist;
            }
        }
        return null;
    }

    private List<DentistDto> ReadDtos()
    {
        if (!File.Exists(FilePath))
        {
            return new List<DentistDto>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<DentistDto>();
        }

        return JsonSerializer.Deserialize<List<DentistDto>>(json) ?? new List<DentistDto>();
    }
}
