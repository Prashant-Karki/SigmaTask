using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SigmaTask.Data.Entities;
using SigmaTask.Model;
using SigmaTask.Repositories;
using SigmaTask.Services;

namespace SigmaTask.Test;

public class CandidateServiceTest
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ILogger<CandidateService> _logger;
    private readonly CandidateService _candidateService;
    public CandidateServiceTest()
    {
        _candidateRepository = Substitute.For<ICandidateRepository>();
        _logger = Substitute.For<ILogger<CandidateService>>();
        _candidateService = new CandidateService(_candidateRepository, _logger);
    }

    [Fact]
    public async Task UpsertCandidateAsync_Should_AddCandidateAndReturnAddApiResponse_IfNoRecordIsPresent()
    {
        //Arrange
        _candidateRepository.FindCandidateAsync(Arg.Any<Candidate>()).ReturnsNull();
        _candidateRepository.AddCandidateAsync(Arg.Any<Candidate>()).Returns(true);
        var candidatePayload = new Candidate();
        var expectedApiResponse = new ApiResponse<Candidate>
        {
            Message = "Candidate add successful.",
            Data = candidatePayload
        };

        //Act
        var actualApiResponse = await _candidateService.UpsertCandidateAsync(candidatePayload);

        //Assert
        await _candidateRepository.Received(1).AddCandidateAsync(candidatePayload);
        _logger
            .ReceivedWithAnyArgs(1)
            .LogInformation("Candidate add successful for: {Candidate}", candidatePayload);
        Assert.Equivalent(expectedApiResponse, actualApiResponse);
    }

    [Fact]
    public async Task UpsertCandidateAsync_Should_UpdateCandidateAndReturnUpdateApiResponse_IfRecordIsPresent()
    {
        //Arrange
        var foundCandidate = new Candidate();
        _candidateRepository.FindCandidateAsync(Arg.Any<Candidate>()).Returns(foundCandidate);
        _candidateRepository.UpdateCandidateAsync(Arg.Any<Candidate>()).Returns(true);
        var updateCandidate = new Candidate
        {
            FirstName = "Peter"
        };
        var expectedApiResponse = new ApiResponse<Candidate>
        {
            Message = "Candidate update successful.",
            Data = updateCandidate
        };


        //Act
        var actualApiResponse = await _candidateService.UpsertCandidateAsync(updateCandidate);

        //Assert
        await _candidateRepository.Received(1).UpdateCandidateAsync(updateCandidate);
        _logger
            .ReceivedWithAnyArgs(1)
            .LogInformation("Candidate update successful for: {Candidate}", updateCandidate);
        Assert.Equivalent(expectedApiResponse, actualApiResponse);
    }
}
