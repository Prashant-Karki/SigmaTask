using SigmaTask.Data.Entities;
using SigmaTask.Model;

namespace SigmaTask.Services
{
    public interface ICandidateService
    {
        Task<ApiResponse<Candidate>> UpsertCandidateAsync(Candidate candidate);
    }
}