using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations; // Added for basic form validation

namespace PhysioWeb.Models
{
    // Assuming CommanProp provides properties like UniquId, CreatedBy, TotalCount, etc.
    public class NewLead : CommanProp
    {
        // --- Lead-Specific Properties ---

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone Number.")]
        public string PhoneNumber { get; set; }

        public string LeadSource { get; set; } // e.g., Zillow, Website, Referral

        public string Status { get; set; } // e.g., New, Contacted, Qualified

        public DateTime DateAdded { get; set; }

        public int AssignedAgentID { get; set; }
        public string AssignedAgentName { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }
        public string InActiveText { get; set; } // For displaying status in a list view


        // --- Dropdown/Lookup Properties (For View Rendering) ---
        // These will be populated by the controller before rendering the view.
        public List<DropDownSource> LeadSourceList { get; set; }
        public List<DropDownSource> StatusList { get; set; }
        public List<DropDownSource> AgentList { get; set; }


        // --- Constructors for Data Mapping ---

        public NewLead() { }

        // Constructor for list view (flag = 0)
        public NewLead(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                // Mapping fields for a list/grid view
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = reader["UniquId"] != DBNull.Value ? Convert.ToInt32(reader["UniquId"]) : 0;
                FirstName = reader["FirstName"]?.ToString();
                LastName = reader["LastName"]?.ToString();
                Email = reader["Email"]?.ToString();
                PhoneNumber = reader["PhoneNumber"]?.ToString();
                Status = reader["StatusName"]?.ToString();
                InActiveText = reader["IsActive"].ToString();
                CreatedBy = reader["CreatedBy"]?.ToString();
            }
            else if (flag == 1)
            {
                // Mapping all fields for a single detail/edit view
                populateObject(this, reader);
            }
        }

        private void populateObject(NewLead obj, IDataReader rdr)
        {
            // Utility method to safely read all columns
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
                obj.FirstName = rdr.GetString(rdr.GetOrdinal("FirstName"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
                obj.LastName = rdr.GetString(rdr.GetOrdinal("LastName"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("Email")))
                obj.Email = rdr.GetString(rdr.GetOrdinal("Email"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")))
                obj.PhoneNumber = rdr.GetString(rdr.GetOrdinal("PhoneNumber"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadSource")))
                obj.LeadSource = rdr.GetString(rdr.GetOrdinal("LeadSource"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("Status")))
                obj.Status = rdr.GetString(rdr.GetOrdinal("Status"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("AssignedAgentID")))
                obj.AssignedAgentID = rdr.GetInt32(rdr.GetOrdinal("AssignedAgentID"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("DateAdded")))
                obj.DateAdded = rdr.GetDateTime(rdr.GetOrdinal("DateAdded"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("Notes")))
                obj.Notes = rdr.GetString(rdr.GetOrdinal("Notes"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
        }
    }
}