namespace Shopping.Application.Dto.Users;

public class UserClaimsDto
{

    public UserClaimsDto()
    {
        
    }
    public UserClaimsDto(Guid Id, string Email, List<string> Role)
    {
        this.Id = Id;
        this.Email = Email;
        this.Role = Role;
    }
    public Guid Id{ get; set; }
    public string Email { get; set; }
    public List<string> Role { get; set; } = [];
};