namespace SchoolHubApi.Models.UserDto;

public abstract record UserDto(
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password,
    string Pesel);