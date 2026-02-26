using Microsoft.AspNetCore.Components;
using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbHelper _dbHelper;

        public DashboardRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }
        public async Task<LeadDashboard> GetLeadDashboardData(string? userID, string? agencyId)
        {
            try
            {
                string[] parametersName = { "UserID", "AgencyId" };
                object[] Values = { userID, agencyId };

                string Sp = "FMR_GetLeadDashboardData";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);

                while (data.Read())
                {
                    var LeadData = new LeadDashboard(data);

                    if (data.NextResult()) {
                        while (data.Read()) {
                            LeadData.TotalLeads = Convert.ToInt32(data.GetValue(0));
                        }
                    }
                    return LeadData;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTableResult> LeadList(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "Lead","Contact", "PropertyType","Budget", "Status","Priority", "AssignedAgent","AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,
                    dataTablePara.sSearch_4,dataTablePara.sSearch_5,dataTablePara.sSearch_6,dataTablePara.AgencyId
                };


                var reader = await _dbHelper.GetDataReaderAsync("[FMR_LeadList]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<LeadDashboard>();

                while (reader.Read())
                {
                    list.Add(new LeadDashboard(reader, 1));
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<DataTableResult> AssignedUnassignLeadsList(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "Lead","Contact", "PropertyType","Budget", "Status","Priority", "AssignedAgent","AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,
                    dataTablePara.sSearch_4,dataTablePara.sSearch_5,dataTablePara.sSearch_6,dataTablePara.AgencyId
                };


                var reader = await _dbHelper.GetDataReaderAsync("[FMR_AssignedUnassignLeadsList]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<LeadDashboard>();

                while (reader.Read())
                {
                    list.Add(new LeadDashboard(reader, 1));
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<NewLead> GetDropDowndata(string UserID)
        {

            try
            {
                string[] parameterNames = { "UserID" };
                object[] parameterValues = { UserID };

                string Sp = "FMR_LeadAssignmentDropDown";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                var NewLeadDropDown = new NewLead();

                while (data.Read())
                {
                    NewLeadDropDown.UsersListForAssign.Add(new DropDownSource(data, true));
                }
               
                return NewLeadDropDown;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
