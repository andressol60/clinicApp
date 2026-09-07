
using System.Globalization;

namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO DE TEXTO
public class FileTextAppointmentStore : IAppointmentStore
{
    private const string FilePath = "appointments.txt";

   
    public void Add(Appointment appointment)
    {
        string line = string.Join('|',
            appointment.id,
            appointment.pid,
            appointment.did,
            appointment.oid,
            appointment.dt1.ToString("o", CultureInfo.InvariantCulture),
            appointment.dt2.ToString("o", CultureInfo.InvariantCulture),
            appointment.st,
            appointment.flag1);

        File.AppendAllLines(FilePath, new[] { line });
    }

    public IEnumerable<Appointment> GetAll()
    {
        var list = new List<Appointment>();
        if (!File.Exists(FilePath))
        {
            return list;
        }

        foreach (var line in File.ReadAllLines(FilePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split('|');
            var id = Guid.Parse(parts[0]);
            var pid = Guid.Parse(parts[1]);
            var did = Guid.Parse(parts[2]);
            var oid = Guid.Parse(parts[3]);
            var dt1 = DateTime.Parse(parts[4], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var dt2 = DateTime.Parse(parts[5], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var st = int.Parse(parts[6]);
            var flag1 = bool.Parse(parts[7]);

            list.Add(new Appointment(id, pid, did, oid, dt1, dt2, st, flag1));
        }

        return list;
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
}
