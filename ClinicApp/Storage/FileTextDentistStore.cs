
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO DE TEXTO
public class FileTextDentistStore : IDentistStore
{
    private const string FilePath = "dentists.txt";

    void IDentistStore.Add(Dentist dentist)
    {
        string line = $"{dentist.GetId()}|{dentist.GetName()}|{dentist.GetEmail()}";
        File.AppendAllLines(FilePath, new[] { line });
    }

    IEnumerable<Dentist> IDentistStore.GetAll()
    {
        var list = new List<Dentist>();
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
