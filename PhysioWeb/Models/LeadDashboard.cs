using System.Data;

namespace PhysioWeb.Models
{
    public class LeadDashboard
    {
        public int TotalLeads { get; set; }
        public int NewLeadsCount { get; set; }
        public int ContactedLeadCount { get; set; }
        public int IntrestedLeadCount { get; set; }

        public int NotIntrestedLeadCount { get; set; }

        public int ConvertedLeadCount { get; set; }

        public string LeadName { get; set; }

        public string ContactPerMobNo { get; set; }

        public string PropertyName { get; set; }


        public decimal Budget { get; set; }

        public string AssignAgent { get; set; }

        public string Status { get; set; }
        public int Priority { get; set; }
        public int UniquId { get; set; }


        public LeadDashboard()
        {

        }
        public LeadDashboard(IDataReader reader, int IsDataList = 0)
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

        private void populateObjectForDataList(LeadDashboard obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadName")))
            {
                obj.LeadName = rdr.GetString(rdr.GetOrdinal("LeadName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPerMobNo")))
            {
                obj.ContactPerMobNo = rdr.GetString(rdr.GetOrdinal("ContactPerMobNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyName")))
            {
                obj.PropertyName = rdr.GetString(rdr.GetOrdinal("PropertyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Budget")))
            {
                obj.Budget = rdr.GetDecimal(rdr.GetOrdinal("Budget"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AssignAgent")))
            {
                obj.AssignAgent = rdr.GetString(rdr.GetOrdinal("AssignAgent"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadStatus")))
            {
                obj.Status = rdr.GetString(rdr.GetOrdinal("LeadStatus"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Priority")))
            {
                obj.Priority = rdr.GetInt32(rdr.GetOrdinal("Priority"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquId")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquId"));
            }
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
