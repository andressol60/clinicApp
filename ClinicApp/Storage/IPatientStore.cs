
namespace ClinicApp.Storage
{
    public interface IPatientStore
    {
        void Add(Patient patient);
        Patient GetyById(Guid id);  //Guid:un identificador unico 
        IEnumerable<Patient> GetAll(); //REGRESE UN GENERICO 



    }
}
