using System.ComponentModel.DataAnnotations;

namespace SigmaTask.Data.Entities;

public class Candidate
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public int? TimeInterval { get; set; }
    public string? LinkedinProfileUrl { get; set; }
    public string? GithubProfileUrl { get; set; }
    public string? Comment { get; set; }

}
