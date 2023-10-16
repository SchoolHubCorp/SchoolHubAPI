namespace SchoolHubApi.Models.UserDto;

public record ParentDto(
    string Username, 
    string FirstName,
    string LastName, 
    string Email, 
    string PhoneNumber, 
    string Password, 
    string Pesel,
    string ChildCode) : UserDto(Username, FirstName, LastName, Email, PhoneNumber, Password, Pesel);