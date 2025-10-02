using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DbHelper _dbHelper;

        public PropertyRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<PropertyMaster> GetPropertyDetails(int propertyId)
        {
            try
            {
                string[] parameterNames = { "PropertyId" };
                object[] parameterValues = { propertyId };


                string Sp = "FMR_GetPropertyDetailsById";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                PropertyMaster Property;
                while (data.Read())
                {
                    Property = new PropertyMaster(data, 0);
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {

                            Property.Images.Add(new DropDownSource(data, true));
                        }
                    }
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {

                            Property.Videos.Add(new DropDownSource(data, true));
                        }
                    }
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {
                            Property.AmenityList.Add(new DropDownSource(data, true));
                        }
                    }
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {
                            Property.PropertyDetails.Add(new PropertyDetails(data));
                        }
                    }
                    return Property;
                }


                return new PropertyMaster();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> SendRequest(string contactPersonName, string contactPersonEmail, string contactPersonPhone, string description, int propertyId)
        {
            try
            {
                string[] parametersName = { "ContactPerson", "Email", "PhoneNo", "Description", "PropertyId" };

                object[] Values = { contactPersonName, contactPersonEmail, contactPersonPhone, description, propertyId };


                string Sp = "FMR_BookDemo";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
    }
}
