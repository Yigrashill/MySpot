
using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
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
        var reservations = _reservationsService.GetAllWeekly();

        if (reservations.Count() > 0)
        { 
            return Ok(reservations);
        }

        return NoContent();
    }

    [HttpGet("{id:Guid}")]
    public ActionResult<ReservationDTO> Get(Guid id)
    {
        var reservation =  _reservationsService.GetReservation(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = _reservationsService.Create(command with { ReservationId = Guid.NewGuid() });

        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new {id}, null);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, ChangeeservationLicensePlate command)
    {
        if(_reservationsService.Update(command with { ReservationId = id}))
        {
            return NoContent();
        }

        return NotFound();
    }


    [HttpDelete("{id:guid}")]
    public ActionResult<Reservation> Delete(Guid id)
    {
        if(_reservationsService.Delete(new DeleteReservation(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}
