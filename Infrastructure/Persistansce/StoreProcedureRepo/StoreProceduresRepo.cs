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

        /// <summary>
        /// GetProcess
        /// </summary>
        /// <returns></returns>
        public List<UpGetProcessModel> GetProcess()
        {
             DataTable dt = dba.GetDataSP("up_GetProcess", null);
             List<UpGetProcessModel> list = dt.AsEnumerable().Select(m => new UpGetProcessModel(){
                    ProcessID = m.Field<int>("ProcessID"),
                    Process = m.Field<string>("Process"),                               
             }).ToList();
            return list;
        }

        /// <summary>
        /// GetServerPathFromProcessID
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public string GetServerPathFromProcessID(int processID)
        {                    
            SqlParameter parameter = new SqlParameter("@ProcessID", processID);
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = parameter;
            DataTable dt = dba.GetDataSP("up_GetFilePathByProcess", parameters);
            string serverPath = dt.Rows[0]["ServerPath"].ToString();
            return serverPath;
        }

        /// <summary>
        /// InsertAndGetUserActivity
        /// </summary>
        /// <param name="userActivity"></param>  
        public UserActivity InsertAndGetUserActivity(UserActivity userActivity)
        {
            SqlParameter parameter1 = new SqlParameter("@UserNT", userActivity.UserNT);
            SqlParameter parameter2 = new SqlParameter("@FkProcess", userActivity.FkProcess);
            SqlParameter parameter3 = new SqlParameter("@Activity", userActivity.Activity);
            SqlParameter parameter4 = new SqlParameter("@Terminal", userActivity.Terminal);            
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;            
            DataTable dt = dba.GetDataSP("up_InsertAR_UserActivity", parameters);
            UserActivity userActivityModel = dt.AsEnumerable().Select(m => new UserActivity(){                   
                    UserNT = m.Field<string>("UserNT"),
                    FkProcess = m.Field<int>("FkProcess"),
                    Activity = m.Field<string>("Activity"),
                    Terminal = m.Field<string>("Terminal"),
                    UpdatedAt = m.Field<DateTime>("UpdatedAt"),
             }).FirstOrDefault();
            return userActivityModel;
        }

        /// <summary>
        /// InsertAndGetImageRepositoryModel
        /// </summary>
        /// <param name="imageObj"></param>
        /// <returns></returns>
        public ImageRepositoryModel InsertAndGetImageRepositoryModel(ImageRepositoryModel imageObj)
        {
            SqlParameter parameter1 = new SqlParameter("@SerialNumber", imageObj.SerialNumber);
            SqlParameter parameter2 = new SqlParameter("@FKProcess", imageObj.FKProcess);
            SqlParameter parameter3 = new SqlParameter("@Path", imageObj.Path);
            SqlParameter parameter4 = new SqlParameter("@FileDateTime", imageObj.FileDateTime);
            SqlParameter parameter5 = new SqlParameter("@FileName", imageObj.FileName);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            DataTable dt = dba.GetDataSP("up_InsertAR_ImageRepository", parameters);
            ImageRepositoryModel imageRepositoryModel = dt.AsEnumerable().Select(m => new ImageRepositoryModel(){                   
                    SerialNumber = m.Field<string>("SerialNumber"),
                    FKProcess = m.Field<int>("FKProcess"),
                    Path = m.Field<string>("Path"),                  
                    UpdatedAt = m.Field<DateTime>("UpdatedAt")
             }).FirstOrDefault();
            return imageRepositoryModel;
        } 

    }
}
