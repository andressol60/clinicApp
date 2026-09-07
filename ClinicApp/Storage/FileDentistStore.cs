
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO BINARIO
public class FileDentistStore : IDentistStore
{
    private const string FilePath = "dentists.dat";

    void IDentistStore.Add(Dentist dentist)
    {
        using var stream = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
        using var writer = new BinaryWriter(stream);

        writer.Write(dentist.GetId().ToByteArray());
        writer.Write(dentist.GetName());
        writer.Write(dentist.GetEmail());
    }

    IEnumerable<Dentist> IDentistStore.GetAll()
    {
        var list = new List<Dentist>();
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
            list.Add(new Dentist(id, name, email));
        }

        return list;
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
}
