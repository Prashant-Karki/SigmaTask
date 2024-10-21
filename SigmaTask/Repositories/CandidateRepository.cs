using Microsoft.EntityFrameworkCore;
using SigmaTask.Data;
using SigmaTask.Data.Entities;

namespace SigmaTask.Repositories;

public class CandidateRepository : ICandidateRepository
{
    private readonly DataContext _dataContext;
    private readonly ILogger _logger;

    public CandidateRepository(DataContext dataContext,
        ILogger<CandidateRepository> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task<Candidate?> FindCandidateAsync(Candidate candidate)
    {
        try
        {
            return await _dataContext.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == candidate.Email);
        }
        catch
        {
            _logger.LogError("Error occured while finding candidate candidate by key: {Email}", candidate.Email);
            throw;
        }
    }

    public async Task<bool> AddCandidateAsync(Candidate candidate)
    {
        try
        {
            _dataContext.Candidates.Add(candidate);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            _logger.LogError("Error occured while adding candidate: {Candidate}", candidate);
            throw;
        }
    }

    public async Task<bool> UpdateCandidateAsync(Candidate candidate)
    {
        try
        {
            _dataContext.Candidates.Update(candidate);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            _logger.LogError("Error occured while updating candidate: {Candidate}", candidate);
            throw;
        }
    }
}
