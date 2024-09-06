using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace haveaseat.DTOs;
public class NewUserLoginDTO
{
    [JsonConstructor] public NewUserLoginDTO() { }
    public NewUserLoginDTO(User user)
    {
        Email = user.Email;
        Password = user.Password;


    }
    public string Email { get; set; }
    public string Password { get; set; }


}
