using System.Net;
using API.Configuration;
using Application.Companies.CreateCompany;
using Application.Companies.DeleteCompany;
using Application.Companies.DeleteCompany;
using Application.Companies.UpdateCompany;
using Domain.Companies;
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

    [Route("{companyId:guid}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteCompany(Guid companyId)
    {
        await _mediator.Send(new DeleteCompanyCommand(new CompanyId(companyId), GetUserId()));

        return NoContent();
    }

    [Route("{companyId:guid}")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] UpdateCompanyRequest request)
    {
        await _mediator.Send(
            new UpdateCompanyCommand(
                companyId,
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

        return NoContent();
    }

    // [Route("{companyId:guid}")]
    // [HttpGet]
    // [ProducesResponseType(typeof(CompanyDTO), (int)HttpStatusCode.OK)]
    // public async Task<IActionResult> GetCompanyById(Guid companyId)
    // {
    //     
    // }
    //
    // [Route("")]
    // [HttpGet]
    // [ProducesResponseType(typeof(CompanyDTO), (int)HttpStatusCode.OK)]
    // public async Task<IActionResult> GetAllCompanies()
    // {
    //     
    // }
}