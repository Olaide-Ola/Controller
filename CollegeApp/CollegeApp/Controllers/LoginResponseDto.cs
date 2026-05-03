using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class LoginResponseDto //this will return whether the login is successful or not
{
    public string Username { get; set; }
    public string Token { get; set; }
}