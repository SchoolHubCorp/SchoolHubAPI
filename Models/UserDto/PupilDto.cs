namespace SchoolHubApi.Models.UserDto;

public record PupilDto(
    string Email, 
    string FirstName,
    string LastName, 
    string PhoneNumber, 
    string Password, 
    string Pesel,
    string ClassCode) : UserDto(Email, FirstName, LastName, PhoneNumber, Password, Pesel);