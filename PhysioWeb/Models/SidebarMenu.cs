using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class SidebarMenu : CommanProp
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuType { get; set; }  // parent or child
        public int? ParentMenuId { get; set; }
        public int? ChildId { get; set; }
        public string MenuIcon { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool OpenInNewTab { get; set; }
        public bool HasChild { get; set; }
        public bool IsParent { get; set; }

        public List<DropDownSource> ParentList { get; set; }
        public SidebarMenu()
        {
            ParentList = new List<DropDownSource>();
        }
        public SidebarMenu(IDataReader reader)
        {
            populateObject(this, reader);
        }

        private void populateObject(SidebarMenu obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("ParentId")))
            {
                obj.ParentMenuId = rdr.GetInt32(rdr.GetOrdinal("ParentId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ChildId")))
            {
                obj.ChildId = rdr.GetInt32(rdr.GetOrdinal("ChildId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuName")))
            {
                obj.MenuName = rdr.GetString(rdr.GetOrdinal("MenuName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuUrl")))
            {
                obj.MenuUrl = rdr.GetString(rdr.GetOrdinal("MenuUrl"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("HasChild")))
            {
                obj.HasChild = rdr.GetBoolean(rdr.GetOrdinal("HasChild"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsParent")))
            {
                obj.IsParent = rdr.GetBoolean(rdr.GetOrdinal("IsParent"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuIcon")))
            {
                obj.MenuIcon = rdr.GetString(rdr.GetOrdinal("MenuIcon"));
            }
            
        }
    }
}
