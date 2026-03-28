namespace PhysioWeb.Models
{
    public class UserAccess
    {
        public List<SidebarMenu> SideBarMenu { get; set; }

        public List<DropDownSource> UserRoles { get; set; }

        public int UserRoleId { get; set; }

        public UserAccess()
        {
            SideBarMenu = new List<SidebarMenu>();
            UserRoles = new List<DropDownSource>();
        }
    }
}
