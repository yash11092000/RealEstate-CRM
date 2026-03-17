namespace PhysioWeb.Models
{
    public class CommanMaster:CommanProp
    {
        public List<DropDownSource> MasterDropDown { get; set; }
        
        public string MasterName { get; set; }

        public bool IsActive { get; set; }

        public CommanMaster() {
            MasterDropDown = new List<DropDownSource>();
        }
    }
}
