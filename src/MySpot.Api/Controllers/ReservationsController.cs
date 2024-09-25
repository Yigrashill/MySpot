
using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Model;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationsService _reservationsService = new();

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get()
    {
        var reservations = _reservationsService.GetAllReservations();

        if (reservations.Count > 0)
        { 
            return Ok(reservations);
        }

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation =  _reservationsService.GetReservation(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Reservation reservation)
    {
        var id = _reservationsService.Create(reservation);

        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new {id}, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        if(_reservationsService.Update(id, reservation))
        {
            return NoContent();
        }

        return NotFound();
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Reservation> Delete(int id)
    {
        if(_reservationsService.Delete(id))
        {
            return NoContent();
        }

        return NotFound();
    }
}
