using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SigmaTask.Controllers;
using SigmaTask.Data.Entities;
using SigmaTask.Services;

namespace SigmaTask.Test;

public class CandidateControllerTest
{
    private readonly ICandidateService _candidateService;
    private readonly IValidator<Candidate> _candidateValidator;
    private readonly CandidateController _candidateController;
    public CandidateControllerTest()
    {
        _candidateValidator = Substitute.For<IValidator<Candidate>>();
        _candidateService = Substitute.For<ICandidateService>();
        _candidateController = new CandidateController(_candidateValidator, _candidateService);
    }

    [Fact]
    public async Task UpsertCandidate_ShouldReturnBadRequest_IfRequestPayloadIsInvalid()
    {
        //Arrange
        var invalidValidationResult = new ValidationResult
        {
            Errors = [new ValidationFailure("FirstName", "Firstname is required.")]
        };
        var requestCandidate = new Candidate();

        var validationResult = _candidateValidator.ValidateAsync(Arg.Any<Candidate>()).Returns(invalidValidationResult);

        //Act
        var actualResult = (await _candidateController.UpsertCandidate(requestCandidate)) as ObjectResult;

        //Assert
        Assert.Equivalent(400, actualResult?.StatusCode);
    }

    [Fact]
    public async Task UpsertCandidate_ShouldReturnOkResult_IfRequestPayloadIsValid()
    {
        //Arrange
        var validValidationResult = new ValidationResult();
        var requestCandidate = new Candidate();

        var validationResult = _candidateValidator.ValidateAsync(Arg.Any<Candidate>()).Returns(validValidationResult);

        //Act
        var actualResult = (await _candidateController.UpsertCandidate(requestCandidate)) as ObjectResult;

        //Assert
        Assert.Equivalent(200, actualResult?.StatusCode);
    }
}
