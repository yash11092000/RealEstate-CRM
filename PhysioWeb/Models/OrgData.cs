using System.Data;

namespace PhysioWeb.Models
{
    public class OrgNode
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }

        public string Contact { get; set; }

        public string Designation { get; set; }
        public List<OrgNode> Children { get; set; } = new List<OrgNode>();
    }
    public class OrgData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
        public string Role { get; set; }

        public string Contact { get; set; }

        public string Designation { get; set; }
        public OrgData()
        {

        }

        public OrgData(IDataReader reader, int flag = 0)
        {
            populateObject(this, reader);
        }

        private void populateObject(OrgData obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("Id")))
            {
                obj.Id = rdr.GetInt32(rdr.GetOrdinal("Id"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Name")))
            {
                obj.Name = rdr.GetString(rdr.GetOrdinal("Name"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ParentId")))
            {
                obj.ParentId = rdr.GetInt32(rdr.GetOrdinal("ParentId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserRole")))
            {
                obj.Role = rdr.GetString(rdr.GetOrdinal("UserRole"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Contact")))
            {
                obj.Contact = rdr.GetString(rdr.GetOrdinal("Contact"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("DesignationName")))
            {
                obj.Designation = rdr.GetString(rdr.GetOrdinal("DesignationName"));
            }
        }
    }
}
