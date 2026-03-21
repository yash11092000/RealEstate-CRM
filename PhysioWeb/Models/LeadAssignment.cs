using System.Data;

namespace PhysioWeb.Models
{
    public class LeadAssignment
    {
        public int LeadId { get; set; }

        public string LeadName { get; set; }

        public string LeadUniqueNo { get; set; }

        public string Contact { get; set; }

        public string Status { get; set; }

        public LeadAssignment()
        {

        }

        public LeadAssignment(IDataReader reader, int IsDataList = 0)
        {
            if (IsDataList == 0)
            {
                populateObject(this, reader);
            }
            else if (IsDataList == 1)
            {
                populateObjectForDataList(this, reader);
            }
        }

        private void populateObjectForDataList(LeadAssignment obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("NewLeadId")))
            {
                obj.LeadId = rdr.GetInt32(rdr.GetOrdinal("NewLeadId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadName")))
            {
                obj.LeadName = rdr.GetString(rdr.GetOrdinal("LeadName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadUniqueNo")))
            {
                obj.LeadUniqueNo = rdr.GetString(rdr.GetOrdinal("LeadUniqueNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactNo")))
            {
                obj.Contact = rdr.GetString(rdr.GetOrdinal("ContactNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadStatus")))
            {
                obj.Status = rdr.GetString(rdr.GetOrdinal("LeadStatus"));
            }
        }

        private void populateObject(LeadAssignment obj, IDataReader rdr)
        {
            throw new NotImplementedException();
           
        }
    }
}
