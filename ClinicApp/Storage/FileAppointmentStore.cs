
namespace ClinicApp.Storage;

// Almacenamiento en ARCHIVO BINARIO
public class FileAppointmentStore : IAppointmentStore
{
    private const string FilePath = "appointments.dat";

    public void Add(Appointment appointment)
    {
        using var stream = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
        using var writer = new BinaryWriter(stream);

        writer.Write(appointment.id.ToByteArray());
        writer.Write(appointment._patientId.ToByteArray());
        writer.Write(appointment._dentistId.ToByteArray());
        writer.Write(appointment._officeId.ToByteArray());
        writer.Write(appointment.dt1.ToBinary());
        writer.Write(appointment.dt2.ToBinary());
        writer.Write(appointment.st);
        writer.Write(appointment.flag1);
    }

    public IEnumerable<Appointment> GetAll()
    {
        var list = new List<Appointment>();
        if (!File.Exists(FilePath))
        {
            return list;
        }

        using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream);

        while (stream.Position < stream.Length)
        {
            var id = new Guid(reader.ReadBytes(16));
            var pid = new Guid(reader.ReadBytes(16));
            var did = new Guid(reader.ReadBytes(16));
            var oid = new Guid(reader.ReadBytes(16));
            var dt1 = DateTime.FromBinary(reader.ReadInt64());
            var dt2 = DateTime.FromBinary(reader.ReadInt64());
            var st = reader.ReadInt32();
            var flag1 = reader.ReadBoolean();

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
