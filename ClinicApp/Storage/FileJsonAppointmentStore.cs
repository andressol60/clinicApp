
using System.Text.Json;

namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO JSON
public class FileJsonAppointmentStore : IAppointmentStore
{
    private const string FilePath = "appointments.json";

    private class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid Pid { get; set; }
        public Guid Did { get; set; }
        public Guid Oid { get; set; }
        public DateTime Dt1 { get; set; }
        public DateTime Dt2 { get; set; }
        public int St { get; set; }
        public bool Flag1 { get; set; }
    }

    public void Add(Appointment appointment)
    {
        var list = ReadDtos();
        list.Add(new AppointmentDto
        {
            Id = appointment.id,
            Pid = appointment._patientId,
            Did = appointment._dentistId,
            Oid = appointment._officeId,
            Dt1 = appointment.dt1,
            Dt2 = appointment.dt2,
            St = appointment.st,
            Flag1 = appointment.flag1
        });

        File.WriteAllText(FilePath, JsonSerializer.Serialize(list));
    }

    public IEnumerable<Appointment> GetAll()
    {
        return ReadDtos().Select(d =>
            new Appointment(d.Id, d.Pid, d.Did, d.Oid, d.Dt1, d.Dt2, d.St, d.Flag1));
    }

    public Appointment GetById(Guid id)
    {
        foreach (var appointment in GetAll())
        {
            if (appointment.id == id)
            {
                return appointment;
            }
        }
        return null;
    }

    private List<AppointmentDto> ReadDtos()
    {
        if (!File.Exists(FilePath))
        {
            return new List<AppointmentDto>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<AppointmentDto>();
        }

        return JsonSerializer.Deserialize<List<AppointmentDto>>(json) ?? new List<AppointmentDto>();
    }
}
