using System.Net;
using API.Configuration;
using Application.Contractors.CreateContractor;
using Application.Contractors.DeleteContractor;
using Application.Contractors.GetAllContractors;
using Application.Contractors.GetContractorById;
using Application.Contractors.UpdateContractor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Contractors.v1;

[Route("api/v1/contractors")]
[ApiController]
public class ContractorsController : AbstractController
{
    private readonly IMediator _mediator;

    public ContractorsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(ContractorDTO), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateContractor([FromBody] CreateContractorRequest request)
    {
        var contractor = await _mediator.Send(
            new CreateContractorCommand(
                GetUserId(),
                request.Name,
                request.IdentificationNumber,
                request.Street,
                request.ZipCode,
                request.City
            )
        );

        return Created(string.Empty, contractor);
    }

    [Route("{contractorId:guid}")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateContractor(Guid contractorId, [FromBody] UpdateContractorRequest request)
    {
        await _mediator.Send(new UpdateContractorCommand(
            contractorId,
            GetUserId(),
            request.Name,
            request.IdentificationNumber,
            request.Street,
            request.City,
            request.ZipCode
        ));

        return NoContent();
    }

    [Route("{contractorId:guid}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteContractor(Guid contractorId)
    {
        await _mediator.Send(new DeleteContractorCommand(contractorId, GetUserId()));

        return NoContent();
    }

    [Route("{contractorId:guid}")]
    [HttpGet]
    [ProducesResponseType(typeof(Application.Contractors.ContractorDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetContractorById(Guid contractorId)
    {
        var contractor = await _mediator.Send(new GetContractorByIdQuery(contractorId, GetUserId()));

        return Ok(contractor);
    }

    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(Application.Contractors.ContractorDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllContractors()
    {
        var contractors = await _mediator.Send(new GetAllContractorsQuery(GetUserId()));

        return Ok(contractors);
    }
}