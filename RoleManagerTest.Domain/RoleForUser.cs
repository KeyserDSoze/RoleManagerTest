namespace RoleManagerTest.Domain
{
    public class RoleForUser
    {
        public string Id { get; set; }
        public List<RoleForService> Roles { get; set; }
    }
}