namespace SchoolHubApi.Models.UserDto;

public record PupilDto(
    string Username, 
    string FirstName,
    string LastName, 
    string Email, 
    string PhoneNumber, 
    string Password, 
    string Pesel,
    string ClassCode) : UserDto(Username, FirstName, LastName, Email, PhoneNumber, Password, Pesel);