using Domain.Users;

namespace Domain.Contractors;

public interface IContractorRepository
{
    Task<Contractor> GetByIdAsync(ContractorId id, UserId userId);

    Task AddAsync(Contractor contractor);

    Task DeleteAsync(ContractorId id, UserId userId);

    Task SaveAsync(Contractor contractor);
}