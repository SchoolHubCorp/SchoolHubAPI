namespace SchoolHubApi.Models.UserDto;

public abstract record UserDto(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string Pesel);