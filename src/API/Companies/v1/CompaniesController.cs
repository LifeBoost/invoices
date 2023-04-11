using System.Net;
using API.Configuration;
using Application.Companies.CreateCompany;
using Application.Companies.DeleteCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Companies.v1;

[Route("api/v1/companies")]
[ApiController]
public class CompaniesController: AbstractController
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(CompanyDTO), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
    {
        var company = await _mediator.Send(
            new CreateCompanyCommand(
                GetUserId(),
                request.Name,
                request.IdentificationNumber,
                request.Email,
                request.PhoneNumber,
                request.IsVatPayer,
                request.VatRejectionReason,
                request.Street,
                request.City,
                request.ZipCode
            )
        );
        
        return Created(string.Empty, company);
    }

    [Route("{companyId:guid}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteCompany(Guid companyId)
    {
        await _mediator.Send(new DeleteCompanyCommand(companyId, GetUserId()));
        
        return NoContent();
    }
}