namespace SchoolHubApi.Models.UserDto;

public record CreateUserRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string Pesel);