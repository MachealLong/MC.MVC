using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace MC.MVC.FrameWork.Sqllite
{
    public class SQLiteHelper
    {
        private string connStr = "Data Source={0};Pooling=true;FailIfMissing=false";
        private string path = string.Empty;
        private SQLiteConnection conn;

        private SQLiteHelper()
        {
        }

        public SQLiteHelper(string path)
        {
            this.path = path;
            this.conn = new SQLiteConnection(string.Format(this.connStr, path));
        }

        ~SQLiteHelper()
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void CreateDB()
        {
            try
            {
                SQLiteConnection.CreateFile(this.path);
            }
            catch
            {
                throw new Exception("创建数据库异常");
            }
        }

        public SQLiteConnection Open()
        {
            if (this.conn.State != ConnectionState.Open)
            {
                this.conn.Open();
            }
            else
            {
                this.conn.Close();
                this.conn.Open();
            }
            return this.conn;
        }

        public void Close()
        {
            if (this.conn.State != ConnectionState.Closed)
            {
                this.conn.Close();
            }
        }

        public void Dispose()
        {
            this.Close();
            SQLiteConnection.ClearPool(this.conn);
        }

        public void ExecuteNonQuery(string sql)
        {
            SQLiteTransaction myTran = null;
            try
            {
                this.conn.Open();
                myTran = this.conn.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(sql, this.conn);

                command.Transaction = myTran;
                command.ExecuteNonQuery();
                myTran.Commit();
            }
            catch
            {
                if (myTran != null) { myTran.Rollback(); }
                throw;
            }
            finally
            {
                this.conn.Close();
            }
        }

        public void ExecuteNonQuery(string sql, SQLiteParameter[] sqlParams)
        {
            SQLiteTransaction myTran = null;
            try
            {
                this.conn.Open();
                myTran = this.conn.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(sql, this.conn);
                if (sqlParams != null && sqlParams.Length > 0)
                {
                    command.Parameters.AddRange(sqlParams);
                }

                command.Transaction = myTran;
                command.ExecuteNonQuery();
                myTran.Commit();
            }
            catch
            {
                if (myTran != null) { myTran.Rollback(); }
                throw;
            }
            finally
            {
                this.conn.Close();
            }
        }

        public void ExecuteNonQueryBatch(string sql, SQLiteParameter[][] sqlParams)
        {
            SQLiteTransaction myTran = null;
            try
            {
                this.conn.Open();
                myTran = this.conn.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(sql, this.conn);
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    if (sqlParams[i] != null && sqlParams[i].Length > 0)
                    {
                        command.Parameters.AddRange(sqlParams[i]);
                    }

                    command.Transaction = myTran;
                    command.ExecuteNonQuery();
                }
                myTran.Commit();
            }
            catch
            {
                if (myTran != null) { myTran.Rollback(); }
                throw;
            }
            finally
            {
                this.conn.Close();
            }
        }

    }
}
