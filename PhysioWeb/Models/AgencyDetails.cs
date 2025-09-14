using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class AgencyDetails : CommanProp
    {
        public string AgencyName { get; set; }
        public bool IsAgencyRegistered { get; set; } = false;

        // ✅ Logo
        public IFormFile AgencyLogo { get; set; }
        public string AgencyLogoFilePath { get; set; }
        public string AgencyLogoFileName { get; set; }

        // ✅ Contact Info
        public string ContactPersonName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string WebsiteUrl { get; set; }

        // ✅ Address
        public string StreetAddress { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }

        // ✅ Registration Info
        public string ReraRegNo { get; set; }
        public string LicenseIssueDate { get; set; }
        public string LicenseExpiryDate { get; set; }
        public string PAN { get; set; }
        public string GST { get; set; }

        public bool IsActive { get; set; } = true;

        public string IsActiveText { get; set; }

        // ✅ Documents
        public IFormFile ReraCertificate { get; set; }
        public string ReraCertificateFilePath { get; set; }
        public string ReraCertificateFileName { get; set; }

        public IFormFile AgencyLicense { get; set; }
        public string AgencyLicenseFilePath { get; set; }
        public string AgencyLicenseFileName { get; set; }

        public IFormFile AddressProof { get; set; }
        public string AddressProofFilePath { get; set; }
        public string AddressProofFileName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string AgencyTheme { get; set; }

        // ✅ For Handling Uploads in Forms
        //public IFormFile AgencyLogoFile { get; set; }
        //public IFormFile ReraCertificateFile { get; set; }
        //public IFormFile AgencyLicenseFile { get; set; }
        //public IFormFile AddressProofFile { get; set; }

        public AgencyDetails()
        {

        }
        public AgencyDetails(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                populateList(this, reader);
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }

        private void populateObject(AgencyDetails obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("AgencyID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyName")))
            {
                obj.AgencyName = rdr.GetString(rdr.GetOrdinal("AgencyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsAgencyRegistered")))
            {
                obj.IsAgencyRegistered = rdr.GetBoolean(rdr.GetOrdinal("IsAgencyRegistered"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyLogo")))
            {
                obj.AgencyLogoFilePath = rdr.GetString(rdr.GetOrdinal("AgencyLogo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonName")))
            {
                obj.ContactPersonName = rdr.GetString(rdr.GetOrdinal("ContactPersonName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("EmailAddress")))
            {
                obj.EmailAddress = rdr.GetString(rdr.GetOrdinal("EmailAddress"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MobileNo")))
            {
                obj.MobileNo = rdr.GetString(rdr.GetOrdinal("MobileNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AlternateMobileNo")))
            {
                obj.AlternateMobileNo = rdr.GetString(rdr.GetOrdinal("AlternateMobileNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("WebsiteUrl")))
            {
                obj.WebsiteUrl = rdr.GetString(rdr.GetOrdinal("WebsiteUrl"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("StreetAddress")))
            {
                obj.StreetAddress = rdr.GetString(rdr.GetOrdinal("StreetAddress"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CityName")))
            {
                obj.CityName = rdr.GetString(rdr.GetOrdinal("CityName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("StateName")))
            {
                obj.StateName = rdr.GetString(rdr.GetOrdinal("StateName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Pincode")))
            {
                obj.Pincode = rdr.GetString(rdr.GetOrdinal("Pincode"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Country")))
            {
                obj.Country = rdr.GetString(rdr.GetOrdinal("Country"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ReraRegNo")))
            {
                obj.ReraRegNo = rdr.GetString(rdr.GetOrdinal("ReraRegNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LicenseIssueDate")))
            {
                obj.LicenseIssueDate = rdr.GetString(rdr.GetOrdinal("LicenseIssueDate"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LicenseExpiryDate")))
            {
                obj.LicenseExpiryDate = rdr.GetString(rdr.GetOrdinal("LicenseExpiryDate"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PAN")))
            {
                obj.PAN = rdr.GetString(rdr.GetOrdinal("PAN"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("GST")))
            {
                obj.GST = rdr.GetString(rdr.GetOrdinal("GST"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ReraCertificate")))
            {
                obj.ReraCertificateFilePath = rdr.GetString(rdr.GetOrdinal("ReraCertificate"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyLicense")))
            {
                obj.AgencyLicenseFilePath = rdr.GetString(rdr.GetOrdinal("AgencyLicense"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AddressProof")))
            {
                obj.AddressProofFilePath = rdr.GetString(rdr.GetOrdinal("AddressProof"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ThemeColor")))
            {
                obj.AgencyTheme = rdr.GetString(rdr.GetOrdinal("ThemeColor"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserName")))
            {
                obj.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Password")))
            {
                obj.Password = rdr.GetString(rdr.GetOrdinal("Password"));
            }
        }

        private void populateList(AgencyDetails obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyName")))
            {
                obj.AgencyName = rdr.GetString(rdr.GetOrdinal("AgencyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CityName")))
            {
                obj.CityName = rdr.GetString(rdr.GetOrdinal("CityName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("StatusText")))
            {
                obj.IsActiveText = rdr.GetString(rdr.GetOrdinal("StatusText"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedBy")))
            {
                obj.CreatedBy = rdr.GetString(rdr.GetOrdinal("CreatedBy"));
            }
        }
    }
}
