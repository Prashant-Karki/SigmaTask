using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SigmaTask.Data.Entities;
using SigmaTask.Extensions;
using SigmaTask.Services;

namespace SigmaTask.Controllers
{
    /// <summary>
    /// Controller for candidate management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IValidator<Candidate> _candidateValidator;
        private readonly ICandidateService _candidateService;

        /// <summary>
        /// Injecting the dependencies on runtime
        /// </summary>
        /// <param name="candidateValidator">Validator for candidate payload.</param>
        /// <param name="candidateService">Business Logic for the candidate management.</param>
        public CandidateController(IValidator<Candidate> candidateValidator,
            ICandidateService candidateService)
        {
            _candidateValidator = candidateValidator;
            _candidateService = candidateService;
        }

        /// <summary>
        /// Performs upsert (insert or update) for a candidate profile
        /// </summary>
        /// <param name="candidate">Payload for post request for upsert</param>
        /// <returns></returns>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     Inserts if no record is present and updates otherwise.
        /// 
        ///     POST /api/Candidate (Insert)
        ///     {
        ///     "email": "test@yopmail.com",
        ///     "firstName": "InsertTest",
        ///     "lastName": "Person",
        ///     "phoneNumber": "+977-9807656478",
        ///     "timeInterval": 5,
        ///     "linkedinProfileUrl": "linkedin.com/1112131",
        ///     "githubProfileUrl": "github.com/1123455",
        ///     "comment": "test comment for insert task."
        ///     }
        ///     
        ///     POST /api/Candidate (Update)
        ///     {
        ///     "email": "test@yopmail.com",
        ///     "firstName": "UpdateTest",
        ///     "lastName": "Person",
        ///     "phoneNumber": "+977-9800098778",
        ///     "timeInterval": 6,
        ///     "linkedinProfileUrl": "linkedin.com/1112132",
        ///     "githubProfileUrl": "github.com/1123452",
        ///     "comment": "test comment for update task."
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Returns response with details of inserted or updated and value created or updated</response>
        /// <response code="400">Returns Bad Request with key value details </response>
        /// <response code="500">Database and internal server errors</response>

        [HttpPost]
        public async Task<IActionResult> UpsertCandidate(Candidate candidate)
        {
            ValidationResult validationResult = await _candidateValidator.ValidateAsync(candidate);

            //check if request is valid
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            //perform upsert
            var result = await _candidateService.UpsertCandidateAsync(candidate);

            return Ok(result);
        }
    }
}
