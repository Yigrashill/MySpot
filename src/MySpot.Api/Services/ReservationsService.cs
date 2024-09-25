using MySpot.Api.Model;

namespace MySpot.Api.Services;

public class ReservationsService
{
    private static int _id = 1;

    private static readonly List<Reservation> _reservations = new();

    private static readonly List<string> _parkingSpots = new()
    {
        "P1", "P2", "P3", "P4", "P5",
    };

    public ICollection<Reservation> GetAllReservations() =>
        _reservations;

    public Reservation GetReservation(int id) =>
        _reservations.SingleOrDefault(x => x.Id == id);

    public int? Create(Reservation reservation)
    {
        if (_parkingSpots.All(x => x != reservation.ParkingSpotName))
        {
            return default;
        }

        reservation.Date = DateTime.UtcNow.AddDays(1).Date;

        var parkingAlredyExist = _reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName
            && x.Date == reservation.Date.Date);

        if (parkingAlredyExist)
        {
            return default;
        }

        reservation.Id = _id;
        _reservations.Add(reservation);
        _id++;
        return reservation.Id;
    }


    public bool Update(int id, Reservation reservation)
    {
        var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return false;
        }

        existingReservation.LicenscePlateName = reservation.LicenscePlateName;
        return true;
    }

    public bool Delete(int id)
    {
        var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return false;
        }

        _reservations.Remove(existingReservation);
        return true;
    }
}
