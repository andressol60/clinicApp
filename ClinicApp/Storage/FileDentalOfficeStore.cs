
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO BINARIO
public class FileDentalOfficeStore : IDentalOfficeStore
{
    private const string FilePath = "offices.dat";

    public void Add(DentalOffice office)
    {
        using var stream = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
        using var writer = new BinaryWriter(stream);

        writer.Write(office.GetId().ToByteArray());
        writer.Write(office.GetName());
    }

    public IEnumerable<DentalOffice> GetAll()
    {
        var list = new List<DentalOffice>();
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
            list.Add(new DentalOffice(id, name));
        }

        return list;
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
}
