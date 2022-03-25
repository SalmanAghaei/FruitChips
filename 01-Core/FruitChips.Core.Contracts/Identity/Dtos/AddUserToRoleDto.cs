namespace FruitChips.Core.Contracts.Identity.Dtos
{
    public class AddUserToRoleDto
    {
        public Guid UserId { get; set; }    
        public string[] RoleNames { get; set; }
    }
}
