namespace SchoolHubApi.Models.UserDto;

public record ParentDto(
    string Email, 
    string FirstName,
    string LastName, 
    string PhoneNumber, 
    string Password, 
    string Pesel,
    string ChildCode) : UserDto(Email, FirstName, LastName, PhoneNumber, Password, Pesel);