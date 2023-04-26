using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GoodsIssuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsIssuesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateGoodsIssueCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    [Route("{id}/baskets")]
    public async Task<IActionResult> AddBasketsAsync(string id, [FromBody] List<AddGoodsIssuesEntryBasketViewModel> baskets)
    {
        var command = new AddBasketsToGoodsIssueCommand(id, baskets);

        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPatch]
    [Route("{id}/baskets")]
    public async Task<IActionResult> ConfirmExportingBasketsAsync(string id, [FromBody] List<string> basketIds)
    {
        var command = new ConfirmExportingBasketsCommand(id, basketIds);

        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }
}
