using System;
namespace RoleBasedAuthorizationAPI.DTOs;

public class RegisterDTOs
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string password { get; set; }
    public string Role { get; set; }
}
