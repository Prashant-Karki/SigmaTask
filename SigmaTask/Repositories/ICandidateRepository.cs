using SigmaTask.Data.Entities;

namespace SigmaTask.Repositories;

public interface ICandidateRepository
{
    Task<Candidate?> FindCandidateAsync(Candidate candidate);
    Task<bool> AddCandidateAsync(Candidate candidate);
    Task<bool> UpdateCandidateAsync(Candidate candidate);
}