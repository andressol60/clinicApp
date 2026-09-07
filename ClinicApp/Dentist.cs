namespace ClinicApp;

public class Dentist
{   //NO SE ENCAPSULA LOS DATOS 
    //public Guid id;
    //public string nm;
    //public string em;

    //REFACTORY :encapsulamiento de datos protejemos datos
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _email;

    public Dentist(string name, string email)
    {
        ContactValidator.Validate(name, email);
        _name = name;
        _email = email;
        _id = Guid.NewGuid();

        ClinicManager.GetInstance().AllDentists.Add(this);
    }

    public Dentist(Guid id, string name, string email)
    {
        ContactValidator.Validate(name, email);
        _id = id;
        _name = name;
        _email = email;
    }

    public Guid GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetEmail()
    {
        return _email;
    }
    //


}
