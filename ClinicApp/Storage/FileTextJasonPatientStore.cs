
using System.Text.Json;

namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO JSON
public class FileTextJasonPatientStore : IPatientStore
{
    private const string FilePath = "patients.json";

    // DTO: solo se usa para poder serializar/deserializar con System.Text.Json
    // sin tener que exponer propiedades públicas en Patient (que está encapsulado).
    private class PatientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";           //inicializar como vacia
        public string Email { get; set; } = "";
    }

    void IPatientStore.Add(Patient patient)
    {
        var list = ReadDtos();
        list.Add(new PatientDto
        {
            Id = patient.GetId(),
            Name = patient.GetName(),
            Email = patient.GetEmail()
        });

        File.WriteAllText(FilePath, JsonSerializer.Serialize(list));
    }

    IEnumerable<Patient> IPatientStore.GetAll()
    {
        return ReadDtos().Select(d => new Patient(d.Id, d.Name, d.Email));
    }

    Patient IPatientStore.GetyById(Guid id)
    {
        foreach (var patient in ((IPatientStore)this).GetAll())
        {
            if (patient.GetId() == id)
            {
                return patient;
            }
        }
        return null;
    }

    private List<PatientDto> ReadDtos()
    {
        if (!File.Exists(FilePath))
        {
            return new List<PatientDto>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<PatientDto>();
        }

        return JsonSerializer.Deserialize<List<PatientDto>>(json) ?? new List<PatientDto>();
    }
}
