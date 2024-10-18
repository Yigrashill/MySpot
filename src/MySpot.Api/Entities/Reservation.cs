using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public class Reservation
{
    public Guid Id { get; }
    public Guid ParkingSpotId { get; }
    public string EmployeeName { get; private set; }
    public string ParkingSpotName { get; private set; }
    public string LicenscePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(Guid id, Guid parkingSpotId, string employeeName, string licenscePlate, DateTime date)
    {
        Id = id;
        ParkingSpotId = parkingSpotId;
        EmployeeName = employeeName;
        ChangeLicencePlate(licenscePlate);
        Date = date;
    }

    public void ChangeLicencePlate(string licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate))
        {
            throw new EmptyLicensePlateException();
        }

        LicenscePlate = licensePlate;
    }
}