using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistansce;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Persistansce.StoreProcedureRepo
{
    public class StoreProceduresRepo
    { 
        private DbConnectA dba = new DbConnectA();
        //Get List of up_GetProcessModel from up_GetProcess stored procedure
        public List<UpGetProcessModel> GetProcess()
        {
             DataTable dt = dba.GetDataSP("up_GetProcess", null);
             List<UpGetProcessModel> list = dt.AsEnumerable().Select(m => new UpGetProcessModel(){
                    ProcessID = m.Field<int>("ProcessID"),
                    Process = m.Field<string>("Process"),                               
             }).ToList();
            return list;
        }
        public string GetServerPathFromProcessID(int processID)
        {                    
            SqlParameter parameter = new SqlParameter("@ProcessID", processID);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = parameter;
            DataTable dt = dba.GetDataSP("up_GetFilePathByProcess", parameters);
            string serverPath = dt.Rows[0]["ServerPath"].ToString();
            return serverPath;
        }

    }
}
