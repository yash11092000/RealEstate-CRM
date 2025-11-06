namespace PhysioWeb.Models
{
    public class SidebarMenu:CommanProp
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuType { get; set; }  // parent or child
        public int? ParentMenuId { get; set; }
        public string MenuIcon { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool OpenInNewTab { get; set; }

        public List<DropDownSource> ParentList { get; set; }
        public SidebarMenu()
        {
            ParentList = new List<DropDownSource>();
        }
    }
}
