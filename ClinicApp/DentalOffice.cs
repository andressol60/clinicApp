namespace ClinicApp;

public class DentalOffice
{
    public Guid _id;
    public string _name;

    public DentalOffice(string name)
    {
        DentalOfficeValidator.ValidateName(name);

        _id = Guid.NewGuid();
        _name = name;

        ClinicManager.GetInstance().AllOffices.Add(this);
    }

    public DentalOffice(Guid id, string name)
    {
        DentalOfficeValidator.ValidateName(name);
        _id = id;
        _name = name;
    }

    public Guid GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }
}
