namespace ClinicApp;

public class DentalOfficeValidator
{
    public static void ValidateName(string name)
    {
        if (name == null || name == "")
        {
            throw new ArgumentException("El nombre no puede estar vacío");
        }
    }
}
