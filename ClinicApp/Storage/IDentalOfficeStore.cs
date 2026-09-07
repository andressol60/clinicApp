
namespace ClinicApp.Storage
{
    public interface IDentalOfficeStore
    {
        void Add(DentalOffice office);
        DentalOffice GetById(Guid id);
        IEnumerable<DentalOffice> GetAll();
    }
}
