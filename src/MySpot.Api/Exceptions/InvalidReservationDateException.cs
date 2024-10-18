using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MySpot.Api.Exceptions
{
    public sealed class InvalidReservationDateException : CustomException
    {
        public InvalidReservationDateException(DateTime date)
            :base($"Reservation date: {date:d} is innvalid.")
        {
            Date = date;
        }

        public DateTime Date { get; }
    }
}
