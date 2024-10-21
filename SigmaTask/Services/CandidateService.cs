using SigmaTask.Data.Entities;
using SigmaTask.Model;
using SigmaTask.Repositories;

namespace SigmaTask.Services;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger _logger;

    public CandidateService(ICandidateRepository candidateRepository,
        ILogger<CandidateService> logger)
    {
        _candidateRepository = candidateRepository;
        _logger = logger;
    }

    public async Task<ApiResponse<Candidate>> UpsertCandidateAsync(Candidate candidate)
    {
        //find
        var dbCandidate = await _candidateRepository.FindCandidateAsync(candidate);

        if (dbCandidate is null)
        {
            //add
            await _candidateRepository.AddCandidateAsync(candidate);
            _logger.LogInformation("Candidate add successful for: {Candidate}", candidate);

            return new ApiResponse<Candidate>
            {
                Message = "Candidate add successful.",
                Data = candidate
            };
        }

        //update
        await _candidateRepository.UpdateCandidateAsync(candidate);
        _logger.LogInformation("Candidate update successful for: {Candidate}", candidate);

        return new ApiResponse<Candidate>
        {
            Message = "Candidate update successful.",
            Data = candidate
        };
    }
}
