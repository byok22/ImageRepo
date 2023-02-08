using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistansce;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Persistansce.AR_ImageRepo
{
   
    public class AR_ImageRepo : IGenericRepositoryDB<AR_ImageRepository>
    {
        private DbConnectA dba = new DbConnectA();
        public void Delete(int id)
        {
            string sql = "DELETE FROM AR_ImageRepository WHERE PKImage = " + id;
            dba.SaveData(sql);

        }

        public List<AR_ImageRepository> GetAll()
        {
         
            DataTable dt = dba.GetData("SELECT * FROM AR_ImageRepository");
            List<AR_ImageRepository> list = dt.AsEnumerable().Select(m => new AR_ImageRepository()
            {
                PKImage = m.Field<int>("PKImage"),
                FKProcess = m.Field<int>("PKImage"),
                Path = m.Field<string>("Path"),
                SerialNumber = m.Field<string>("SerialNumber"),
                FileDateTime = m.Field<DateTime>("FileDateTime"),
                UpdatedAt = m.Field<DateTime>("UpdatedAt"),
            }).ToList();

            return list;
        }

        public AR_ImageRepository GetById(int id)
        {
            DataTable dt = dba.GetData("SELECT * FROM AR_ImageRepository WHERE Id = " + id);
            AR_ImageRepository obj = new AR_ImageRepository();
            obj.PKImage = dt.Rows[0].Field<int>("PKImage");
            obj.FKProcess = dt.Rows[0].Field<int>("FKProcess");
            obj.Path = dt.Rows[0].Field<string>("Path");
            obj.SerialNumber = dt.Rows[0].Field<string>("SerialNumber");
            obj.FileDateTime = dt.Rows[0].Field<DateTime>("FileDateTime");
            obj.UpdatedAt = dt.Rows[0].Field<DateTime>("UpdatedAt");

            return obj;
        }

        public DataTable GetDataSP(string spName, SqlParameter[] param)
        {
            return dba.GetDataSP(spName, param);
        }

        public DataTable GetDataTable(string sql)
        {
           return dba.GetData(sql);
        }

        public void Insert(AR_ImageRepository obj)
        {
            string sql = "INSERT INTO AR_ImageRepository (FKProcess, Path, SerialNumber, FileDateTime, UpdatedAt) VALUES ('" + obj.FKProcess + "', '" + obj.Path + "', '" + obj.SerialNumber + "', '" + obj.FileDateTime + "', '" + obj.UpdatedAt + "')";
            dba.SaveData(sql);
        
        }

        public void SaveData(string sql)
        {
            dba.SaveData(sql);
        }

        public void Update(AR_ImageRepository obj)
        {
            string sql = "UPDATE AR_ImageRepository SET FKProcess = '" + obj.FKProcess + "', Path = '" + obj.Path + "', SerialNumber = '" + obj.SerialNumber + "', FileDateTime = '" + obj.FileDateTime + "', UpdatedAt = '" + obj.UpdatedAt + "' WHERE PKImage = " + obj.PKImage;
            dba.SaveData(sql);
        }
    }
}
