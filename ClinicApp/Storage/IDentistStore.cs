
namespace ClinicApp.Storage;

public interface IDentistStore
{
    void Add(Dentist dentist);
    Dentist GetyById(Guid id);  //Guid:un identificador unico 
    IEnumerable<Dentist> GetAll(); //REGRESE UN GENERICO 


}

