
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO BINARIO
public class FilePatientStore : IPatientStore
{
    private const string FilePath = "patients.dat";

    void IPatientStore.Add(Patient patient)
    {
        using var stream = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
        using var writer = new BinaryWriter(stream);

        writer.Write(patient.GetId().ToByteArray());
        writer.Write(patient.GetName());
        writer.Write(patient.GetEmail());
    }

    IEnumerable<Patient> IPatientStore.GetAll()
    {
        var list = new List<Patient>();
        if (!File.Exists(FilePath))
        {
            return list;
        }

        using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream);

        while (stream.Position < stream.Length)
        {
            var id = new Guid(reader.ReadBytes(16));
            var name = reader.ReadString();
            var email = reader.ReadString();
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
