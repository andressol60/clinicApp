
namespace ClinicApp.Storage
{
    public class InMemoryPatientStore : IPatientStore

    {
        public readonly List<Patient> _patients = new List<Patient>();
        void IPatientStore.Add(Patient patient)
        {
            _patients.Add(patient);
           
        }

        IEnumerable<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetyById(Guid id)
        {
            foreach (var patient in _patients)
            {
                if (patient.GetId() == id)
                {
                    return patient;
                }
            }
            return null;
        }

        IEnumerable<Patient> IPatientStore.GetAll()
        {
            return GetAll();
        }
    }
}
