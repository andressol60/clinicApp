
namespace ClinicApp.Storage;

//aquie ya es como clase 
public class InMemoryDentistStore : IDentistStore
{
    public readonly List<Dentist> _dentists = new List<Dentist>();
    void IDentistStore.Add(Dentist dentist)
    {
        _dentists.Add(dentist);

    }

    IEnumerable<Dentist> GetAll()
    {
        return _dentists;
    }

    public Dentist GetyById(Guid id)
    {
        foreach (var dentist in _dentists)
        {
            if (dentist.GetId() == id)
            {
                return dentist;
            }
        }
        return null;

    }

    IEnumerable<Dentist> IDentistStore.GetAll()
    {
        return GetAll();
    }
}


