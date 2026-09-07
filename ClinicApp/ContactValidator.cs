
namespace ClinicApp
{

    public class ContactValidator
    {
        //CLASE EN DONDE SE APLICO PRINCIPIO DE RESPONSABILIDAD UNICA, YA QUE SE ENCARGA DE VALIDAR LOS DATOS DE CONTACTO DE PACIENTES Y DENTISTAS
        public static void Validate(string name, string email)
        {

            if (name == null || name == "")
            {
                throw new ArgumentException("El nombre no puede estar vacío");
            }
            if (email == null || email == "")
            {
                throw new ArgumentException("El email no puede estar vacío");
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("El email debe contener un '@'");
            }
        }

        public static void ValidateName(string name)
        {
            if (name == null || name == "")
            {
                throw new ArgumentException("El nombre no puede estar vacío");
            }
        }
    }
}
