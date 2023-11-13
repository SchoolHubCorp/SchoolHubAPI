namespace SchoolHubApi.Models.UserDto;

public record TeacherDto(
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password,
    string Pesel) : UserDto(Email, FirstName, LastName, PhoneNumber, Password, Pesel);