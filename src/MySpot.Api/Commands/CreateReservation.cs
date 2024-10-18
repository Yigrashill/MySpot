namespace MySpot.Api.Commands;

// Stosojuemy Rekord jako rozkaz użytkownika
// Rekordy są stosowane jako Propery { get; init;}, to znaczy są inicjalizowane podczas stworzenia i pobieranie dowolnie.

public record CreateReservation(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, string LicensePlate, DateTime Date);