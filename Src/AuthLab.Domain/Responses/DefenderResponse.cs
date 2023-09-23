using AuthLab.Domain.Entities;

namespace AuthLab.Domain.Responses;
public record DefenderResponse
{
    public bool IsSuccess {  get; init; }
    public string? Message { get; init; }
    public UserInformation? UserInformation { get; set; }
}
