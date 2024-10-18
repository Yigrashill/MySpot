using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;

namespace MySpot.Api.Services;

public class ReservationsService
{
    private static readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new()
    {
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P2"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P3"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P4"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), "P5")
    };

    public IEnumerable<ReservationDTO> GetAllWeekly() 
        =>  _weeklyParkingSpots.SelectMany(w => w.Reservations)
        .Select(x => new ReservationDTO
        {
            Id = x.Id,
            ParkingSpotId = x.ParkingSpotId,
            EmployeeName = x.EmployeeName,
            Date = x.Date,
        });

    public ReservationDTO GetReservation(Guid id) 
        =>  GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public Guid? Create(CreateReservation command)
    {
        var weekParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);

        if (weekParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(
            command.ReservationId,
            command.ParkingSpotId,
            command.EmployeeName,
            command.LicensePlate,
            command.Date);

        weekParkingSpot.AddReservation(reservation);

        return reservation.Id;
    } 

    public bool Update(ChangeeservationLicensePlate command)
    {
        var weeklyParkingspot = GetWeeklyParkingSpot(command.ReservationId);
        if (weeklyParkingspot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingspot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.Date <= DateTime.UtcNow)
        {
            return false;
        }

        existingReservation.ChangeLicencePlate(command.LicensePlate);
        return true;
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpot(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        return true;
    }


    private WeeklyParkingSpot GetWeeklyParkingSpot(Guid reservationId)
    {
        return _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
    }
}
