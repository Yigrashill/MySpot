using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities
{

    // Pjęcie Encji - obiekt biznesowy - enkapulujący swoją spójność
    // Encja posiada tożsamość zazwyczaj może to być jakieś ID, ale też może to być np. email w systemie, co można nazwać kluczem naturalnym.

    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
        {
            Id = id;
            From = from;
            To = to;
            Name = name;
        }

        public void AddReservation(Reservation reservation) 
        {
            var isInvalidDate = reservation.Date.Date < From
                || reservation.Date.Date > To
                || reservation.Date.Date < DateTime.UtcNow.Date;

            if (isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date.Date);
            }

            var reservationDateExist = _reservations.Any(x => x.Date.Date == reservation.Date.Date);

            if (reservationDateExist)
            {
                throw new ParkingSpootAlredyExistxception(Name, reservation.Date.Date);
            }

            _reservations.Add(reservation);
        }

        public void RemoveReservation(Guid reservationId)
        {
            var reservation = _reservations.FirstOrDefault(x => x.Id == reservationId);
            _reservations.Remove(reservation);
        }
    }
}
