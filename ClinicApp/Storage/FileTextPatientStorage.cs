
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO DE TEXTO (una línea por paciente, separado por '|')
public class FileTextPatientStorage : IPatientStore
{
    private const string FilePath = "patients.txt";

    void IPatientStore.Add(Patient patient)
    {
        string line = $"{patient.GetId()}|{patient.GetName()}|{patient.GetEmail()}";
        File.AppendAllLines(FilePath, new[] { line });
    }

    IEnumerable<Patient> IPatientStore.GetAll()
    {
        var list = new List<Patient>();
        if (!File.Exists(FilePath))
        {
            return list;
        }

        foreach (var line in File.ReadAllLines(FilePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split('|');
            var id = Guid.Parse(parts[0]);
            var name = parts[1];
            var email = parts[2];
            list.Add(new Patient(id, name, email));
        }

        return list;
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
}
