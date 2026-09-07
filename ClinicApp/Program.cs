using ClinicApp.Storage;

namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        //IPatientStore patientStore = new FileTextPatientStorage();       // texto
        //IDentistStore dentistStore = new FileTextDentistStore();         // texto
        //IDentalOfficeStore officeStore = new FileTextDentalOfficeStore(); // texto
        //IAppointmentStore appointmentStore = new FileTextAppointmentStore(); // texto

        IPatientStore patientStore = new FileTextJasonPatientStore();       // JSON
        IDentistStore dentistStore = new FileJsonDentistStore();             // JSON
        IDentalOfficeStore officeStore = new FileJsonDentalOfficeStore();   // JSON
        IAppointmentStore appointmentStore = new FileJsonAppointmentStore(); // JSON

        Patient patient = new Patient("John Doe", "johndoe@email.com");
        Dentist dentist = new Dentist("Dr. Smith", "dentist@gmail.com");
        DentalOffice dentalOffice = new DentalOffice("Consultorio de limpieza dental");

        patientStore.Add(patient);
        dentistStore.Add(dentist);
        officeStore.Add(dentalOffice);


        try
        {
            Appointment appointment = new Appointment(
           //patient.id,
           patient.GetId(),
                 //dentist.id,
                 dentist.GetId(),
                dentalOffice.GetId(),
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2)
            );

            Console.WriteLine("Cita guardada. Id: " + appointment.id);
            Console.WriteLine("Estado inicial (1=pendiente): " + appointment.st);
            Console.WriteLine("Enviando correo a: " + patient.GetEmail());

            if (appointment.st == 1)
            {
                appointment.st = 2;
                appointment.flag1 = false;
                Console.WriteLine("Cita cancelada. Estado: " + appointment.st);
            }

            Appointment appointment2 = new Appointment(
                patient.GetId(),
                dentist.GetId(),
                dentalOffice.GetId(),
                DateTime.UtcNow.AddHours(3),
                DateTime.UtcNow.AddHours(4)
            );

            appointment2.DoIt2();
            Console.WriteLine("Despues de DoIt2() — estado: " + appointment2.st);

            Console.WriteLine("Total de pacientes: " + ClinicManager.GetInstance().AllPatients.Count);
            Console.WriteLine("Total de citas: " + ClinicManager.GetInstance().AllAppointments.Count);

        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR PROGRAMA");
        }
    }
}
