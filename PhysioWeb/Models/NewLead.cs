using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PhysioWeb.Models
{
    // Assuming CommanProp provides properties like UniquId, CreatedBy, TotalCount, etc.
    public class NewLead : CommanProp
    {
        // --- Lead Information ---
        [Key]
        [Display(Name = "Lead ID")]
        public string LeadId { get; set; } = $"LEAD{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

        [Required(ErrorMessage = "Lead Source is required.")]
        [Display(Name = "Lead Source")]
        public string LeadSource { get; set; }

        [Required(ErrorMessage = "Lead Type is required.")]
        [Display(Name = "Lead Type")]
        public string LeadType { get; set; }

        [Required(ErrorMessage = "Lead Status is required.")]
        [Display(Name = "Lead Status")]
        public string LeadStatus { get; set; }

        // --- Personal / Contact Details ---
        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone Number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Alternate Phone Number.")]
        [Display(Name = "Alternate Number")]
        public string AlternateNumber { get; set; }

        [Required(ErrorMessage = "Preferred Contact Method is required.")]
        [Display(Name = "Preferred Contact Method")]
        public string PreferredContactMethod { get; set; }

        [Display(Name = "Follow-up Date")]
        [DataType(DataType.Date)]
        public DateTime? FollowUpDate { get; set; }

        // --- Property Requirements ---
        [Required(ErrorMessage = "Requirement Type is required.")]
        [Display(Name = "Requirement Type")]
        public string RequirementType { get; set; }

        [Required(ErrorMessage = "Property Type is required.")]
        [Display(Name = "Property Type")]
        public string PropertyType { get; set; }

        [Display(Name = "Budget Range (Min)")]
        public decimal? BudgetMin { get; set; }

        [Display(Name = "Budget Range (Max)")]
        public decimal? BudgetMax { get; set; }

        [Display(Name = "Location Preference")]
        public string LocationPreference { get; set; }

        [Display(Name = "Bedrooms (BHK)")]
        public string Bedrooms { get; set; }

        [Display(Name = "Furnishing Type")]
        public string FurnishingType { get; set; }

        [Display(Name = "Possession Timeframe")]
        public string PossessionTimeframe { get; set; }

        // --- Broker / Agent Assignment ---
        [Display(Name = "Assigned Agent")]
        public string AssignedAgent { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = "System"; // Auto-filled

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Auto-filled

        // --- Additional Fields ---
        [Display(Name = "Remarks / Notes")]
        public string Notes { get; set; }

        [Display(Name = "Lead Priority")]
        public string LeadPriority { get; set; }

        [Display(Name = "Lead Rating")]
        public string LeadRating { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Inactive Status")]
        public string InActiveText { get; set; }

        // --- Dropdown/Lookup Properties (For View Rendering) ---

        public List<DropDownSource> LeadTypeList { get; set; }


        public List<DropDownSource> LeadSourceList { get; set; }


        public List<DropDownSource> LeadStatusList { get; set; } 

        public List<DropDownSource> PreferredContactMethodList { get; set; }

        public List<DropDownSource> RequirementTypeList { get; set; }
        //same as LeadType


        public List<DropDownSource> PropertyTypeList { get; set; }

        public List<DropDownSource> BedroomsList { get; set; }

        public List<DropDownSource> FurnishingTypeList { get; set; }

        public List<DropDownSource> PossessionTimeframeList { get; set; }

        public List<DropDownSource> LeadPriorityList { get; set; }

        public List<DropDownSource> LeadRatingList { get; set; }

        public List<DropDownSource> AgentList { get; set; }
        // --- Constructors for Data Mapping ---

        public string PropertyInterestedIn { get; set; }
        public NewLead()
        {

            LeadRatingList = new List<DropDownSource>();
            AgentList = new List<DropDownSource>();
            LeadPriorityList = new List<DropDownSource>();
            PossessionTimeframeList = new List<DropDownSource>();
            FurnishingTypeList = new List<DropDownSource>();
            BedroomsList = new List<DropDownSource>();
            LeadSourceList = new List<DropDownSource>();
            LeadTypeList = new List<DropDownSource>();
            LeadStatusList = new List<DropDownSource>();
            PreferredContactMethodList = new List<DropDownSource>();
            RequirementTypeList = new List<DropDownSource>();
            PropertyTypeList = new List<DropDownSource>();

        }


        // Constructor for list view (flag = 0)
        public NewLead(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                // Mapping fields for a list/grid view
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = reader["UniquId"] != DBNull.Value ? Convert.ToInt32(reader["UniquId"]) : 0;
                LeadId = reader["LeadId"]?.ToString();
                FullName = reader["FullName"]?.ToString();
                Email = reader["Email"]?.ToString();
                PhoneNumber = reader["PhoneNumber"]?.ToString();
                LeadStatus = reader["LeadStatus"]?.ToString();
                LeadSource = reader["LeadSource"]?.ToString();
                AssignedAgent = reader["AssignedAgent"]?.ToString();
                CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.Now;
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
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquId")))
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquId"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadId")))
                obj.LeadId = rdr.GetString(rdr.GetOrdinal("LeadId"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadSource")))
                obj.LeadSource = rdr.GetString(rdr.GetOrdinal("LeadSource"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadType")))
                obj.LeadType = rdr.GetString(rdr.GetOrdinal("LeadType"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadStatus")))
                obj.LeadStatus = rdr.GetString(rdr.GetOrdinal("LeadStatus"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("FullName")))
                obj.FullName = rdr.GetString(rdr.GetOrdinal("FullName"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("Email")))
                obj.Email = rdr.GetString(rdr.GetOrdinal("Email"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")))
                obj.PhoneNumber = rdr.GetString(rdr.GetOrdinal("PhoneNumber"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("AlternateNumber")))
                obj.AlternateNumber = rdr.GetString(rdr.GetOrdinal("AlternateNumber"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("PreferredContactMethod")))
                obj.PreferredContactMethod = rdr.GetString(rdr.GetOrdinal("PreferredContactMethod"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("FollowUpDate")))
                obj.FollowUpDate = rdr.GetDateTime(rdr.GetOrdinal("FollowUpDate"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("RequirementType")))
                obj.RequirementType = rdr.GetString(rdr.GetOrdinal("RequirementType"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyType")))
                obj.PropertyType = rdr.GetString(rdr.GetOrdinal("PropertyType"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("BudgetMin")))
                obj.BudgetMin = rdr.GetDecimal(rdr.GetOrdinal("BudgetMin"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("BudgetMax")))
                obj.BudgetMax = rdr.GetDecimal(rdr.GetOrdinal("BudgetMax"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LocationPreference")))
                obj.LocationPreference = rdr.GetString(rdr.GetOrdinal("LocationPreference"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("Bedrooms")))
                obj.Bedrooms = rdr.GetString(rdr.GetOrdinal("Bedrooms"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("FurnishingType")))
                obj.FurnishingType = rdr.GetString(rdr.GetOrdinal("FurnishingType"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("PossessionTimeframe")))
                obj.PossessionTimeframe = rdr.GetString(rdr.GetOrdinal("PossessionTimeframe"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("AssignedAgent")))
                obj.AssignedAgent = rdr.GetString(rdr.GetOrdinal("AssignedAgent"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedBy")))
                obj.CreatedBy = rdr.GetString(rdr.GetOrdinal("CreatedBy"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedDate")))
                obj.CreatedDate = rdr.GetDateTime(rdr.GetOrdinal("CreatedDate"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("Notes")))
                obj.Notes = rdr.GetString(rdr.GetOrdinal("Notes"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadPriority")))
                obj.LeadPriority = rdr.GetString(rdr.GetOrdinal("LeadPriority"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("LeadRating")))
                obj.LeadRating = rdr.GetString(rdr.GetOrdinal("LeadRating"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
                obj.InActiveText = rdr.GetBoolean(rdr.GetOrdinal("IsActive")).ToString();
        }
    }

    // Helper class for dropdowns

}