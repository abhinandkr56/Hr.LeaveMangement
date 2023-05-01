using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetAllLeaveTypesQuery());
        return Ok(leaveTypes);
    }

    [HttpGet("get-by-id")]
    public async Task<LeaveTypeDetailsDto> GetById(int id)
    {
        return await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
    }

    [HttpPost]
    // [ProducesResponseType(201)]
    // [ProducesResponseType(400)]
    public async Task<ActionResult> Create(LeaveTypeDetailsDto leaveType)
    {
        var response = await _mediator.Send(new CreateLeaveTypeCommand()
            { Name = leaveType.Name, DefaultDays = leaveType.DefaultDays });
        return CreatedAtAction(nameof(Get), new { id = response });
    }
    
    [HttpPut]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(LeaveTypeDetailsDto leaveType)
    {
        var response =  await _mediator.Send(new UpdateLeaveTypeCommand()
            { Id = leaveType.Id, Name = leaveType.Name, DefaultDays = leaveType.DefaultDays });
        return NoContent();
    }
    
    [HttpDelete]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
         await _mediator.Send(new DeleteLeaveTypeCommand(id));
         return NoContent();
    }
}