using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StorageSlotsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StorageSlotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] string storageSlotId)
    {
        var command = new CreateStorageSlotCommand(storageSlotId);

        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }
}
