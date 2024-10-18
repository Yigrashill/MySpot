namespace MySpot.Api.Exceptions
{
    public sealed class ParkingSpootAlredyExistxception : CustomException
    {
        public ParkingSpootAlredyExistxception(string name, DateTime date )
            : base($"Parking spot: {name} alredy exist at: {date:d}.")
        {
            Name = name;
        }

        public string Name { get; }
        public DateTime Date { get; }

    }
}
