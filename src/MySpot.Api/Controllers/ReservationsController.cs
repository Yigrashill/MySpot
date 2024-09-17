
using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Model;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private static int _id = 1;

    private static readonly List<Reservation> _reservations = new();

    private static readonly List<string> _parkingSpots = new()
    {
        "P1", "P2", "P3", "P4", "P5",
    };

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get()
    {
        if (_reservations.Count > 0)
        {
            return Ok(_reservations);
        }
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _reservations.FirstOrDefault(x => x.Id == id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Reservation reservation)
    {

        if (_parkingSpots.All(x => x != reservation.ParkingSpotName))
        {
            return BadRequest();
        }

        reservation.Date = DateTime.UtcNow.AddDays(1).Date;

        var parkingAlredyExist = _reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName
            && x.Date == reservation.Date.Date);

        if (parkingAlredyExist)
        {
            return BadRequest();
        }

        reservation.Id = _id;
        _reservations.Add(reservation);
        _id++;

        return CreatedAtAction(nameof(Get), new {id = reservation.Id}, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }
        
        existingReservation.LicenscePlateName = reservation.LicenscePlateName;
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Reservation> Delete(int id)
    {
        var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        _reservations.Remove(existingReservation);
        return NoContent();
    }
}
