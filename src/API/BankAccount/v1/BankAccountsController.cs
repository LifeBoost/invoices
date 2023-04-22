using System.Net;
using API.Configuration;
using Application.BankAccounts.CreateBankAccount;
using Application.BankAccounts.DeleteBankAccount;
using Application.BankAccounts.GetAllBankAccounts;
using Application.BankAccounts.GetBankAccountById;
using Application.BankAccounts.UpdateBankAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.BankAccount.v1;

[Route("api/v1/bank-accounts")]
[ApiController]
public class BankAccountsController : AbstractController
{
    private readonly IMediator _mediator;

    public BankAccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(BankAccountDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountRequest request)
    {
        var bankAccount = await _mediator.Send(
            new CreateBankAccountCommand(
                GetUserId(),
                request.CompanyId,
                request.Name,
                request.BankName,
                request.BankAccountNumber,
                request.Currency
                )
            );

        return Created(string.Empty, bankAccount);
    }

    [Route("{bankAccountId:guid}")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateBankAccount(Guid bankAccountId, [FromBody] UpdateBankAccountRequest request)
    {
        await _mediator.Send(
            new UpdateBankAccountCommand(
                bankAccountId,
                GetUserId(),
                request.Name,
                request.BankName,
                request.BankAccountNumber,
                request.Currency
                )
            );
        
        return NoContent();
    }

    [Route("{bankAccountId:guid}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteBankAccount(Guid bankAccountId)
    {
        await _mediator.Send(new DeleteBankAccountCommand(bankAccountId, GetUserId()));

        return NoContent();
    }

    [Route("{bankAccountId:guid}")]
    [HttpGet]
    [ProducesResponseType(typeof(Application.BankAccounts.BankAccountDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBankAccountById(Guid bankAccountId)
    {
        var bankAccount = await _mediator.Send(new GetBankAccountByIdQuery(bankAccountId, GetUserId()));

        return Ok(bankAccount);
    }

    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(Application.BankAccounts.BankAccountDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllBankAccounts()
    {
        var bankAccounts = await _mediator.Send(
            new GetAllBankAccountsQuery(GetUserId())
            );

        return Ok(bankAccounts);
    }
}