
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO DE TEXTO
public class FileTextDentalOfficeStore : IDentalOfficeStore
{
    private const string FilePath = "offices.txt";

    public void Add(DentalOffice office)
    {
        string line = $"{office.GetId()}|{office.GetName()}";
        File.AppendAllLines(FilePath, new[] { line });
    }

    public IEnumerable<DentalOffice> GetAll()
    {
        var list = new List<DentalOffice>();
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
