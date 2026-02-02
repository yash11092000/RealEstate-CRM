using System.Data;

namespace PhysioWeb.Models
{
    public class LeadDashboard
    {
        public int NewLeadsCount { get; set; }
        public int ContactedLeadCount { get; set; }
        public int IntrestedLeadCount { get; set; }

        public int NotIntrestedLeadCount { get; set; }

        public int ConvertedLeadCount { get; set; }

        public LeadDashboard()
        {

        }
        public LeadDashboard(IDataReader reader)
        {
            populateObject(this, reader);
        }

        private void populateObject(LeadDashboard obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("NewLeadsCount")))
            {
                obj.NewLeadsCount = rdr.GetInt32(rdr.GetOrdinal("NewLeadsCount"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactedLeadCount")))
            {
                obj.ContactedLeadCount = rdr.GetInt32(rdr.GetOrdinal("ContactedLeadCount"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IntrestedLeadCount")))
            {
                obj.IntrestedLeadCount = rdr.GetInt32(rdr.GetOrdinal("IntrestedLeadCount"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("NotIntrestedLeadCount")))
            {
                obj.NotIntrestedLeadCount = rdr.GetInt32(rdr.GetOrdinal("NotIntrestedLeadCount"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ConvertedLeadCount")))
            {
                obj.ConvertedLeadCount = rdr.GetInt32(rdr.GetOrdinal("ConvertedLeadCount"));
            }
        }
    }
}
