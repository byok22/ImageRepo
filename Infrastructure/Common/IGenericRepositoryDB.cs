using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public interface IGenericRepositoryDB<T> where T : class
    {
        //get all data from database
        List<T> GetAll();
        //get data by id from database
        T GetById(int id);
        //insert data to database
        void Insert(T obj);
        //update data to database
        void Update(T obj);
        //delete data from database
        void Delete(int id);

        //get data from store procedure
        DataTable GetDataSP(string spName, SqlParameter[] param);

        //save data to database
        void SaveData(string sql);

        //get datatable from database
        DataTable GetDataTable(string sql);
    }
}
