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
        public ImageRepositoryModel InsertAndGetImageRepositoryModel(ImageRepositoryModel imageObj)
        {
            SqlParameter parameter1 = new SqlParameter("@SerialNumber", imageObj.SerialNumber);
            SqlParameter parameter2 = new SqlParameter("@FKProcess", imageObj.FKProcess);
            SqlParameter parameter3 = new SqlParameter("@Path", imageObj.Path);
            SqlParameter parameter4 = new SqlParameter("@FileDateTime", imageObj.FileDateTime);
           
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            DataTable dt = dba.GetDataSP("up_InsertAR_ImageRepository", parameters);
            ImageRepositoryModel imageRepositoryModel = dt.AsEnumerable().Select(m => new ImageRepositoryModel(){                   
                    SerialNumber = m.Field<string>("SerialNumber"),
                    FKProcess = m.Field<int>("FKProcess"),
                    Path = m.Field<string>("Path"),
                    FileDateTime = m.Field<DateTime>("FileDateTime"),
                    UpdatedAt = m.Field<DateTime>("UpdatedAt"),
             }).FirstOrDefault();
            return imageRepositoryModel;
        } 

    }
}
