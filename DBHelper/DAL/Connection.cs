using System;
using System.Data;
using System.Data.SqlClient;

namespace DBHelperDAL
{
    internal partial class Connection : IDisposable
    {
        private SqlConnection connection;
        private SqlTransaction transaction;
        private SqlCommand command;
		private static string connectionString;
        public static string ConnectionString
        {
            get
            {
                if (connectionString == null)
                {
                    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                }
                return connectionString;
            }
        }
		private bool disposed = false;

        public SqlCommand Command
        {
            get { return command; }
        }
        //打开连接并启用事务
        public void Open()
        {
		    connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();
            command = connection.CreateCommand();
			transaction = connection.BeginTransaction();
            command.Transaction = transaction;
        }
		//打开连接，并由参数指定是否启用事务
        public void Open(bool useTransaction)
        {
			connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();
            command = connection.CreateCommand();
            if (useTransaction)
            {
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
            }
        }
        //关闭连接
        public void Close()
        {
            connection.Dispose();
        }
		//回滚
        public void Rollback()
        {
            transaction.Rollback();
        }
        //提交
        public void Commit()
        {
            transaction.Commit();
        }
		#region IDisposable接口实现
		// 实现IDisposable接口
        public void Dispose()
        {
            Dispose(true);
            // 通知垃圾收集器将自己回收
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) 在两种场景下调用，
        // 如果disposing等于true,通过用户代码调用
        // 如果disposing等于false,运行时在调用finalizer时调用
        private void Dispose(bool disposing)
        {
            //如果Disposed等于true，表示已被调用过
            if(!this.disposed)
            {
                // 如果disposing等于true,释放所有托管和非托管资源
                if(disposing)
                {
					command.Dispose();
                	connection.Dispose();
                }
            }
            disposed = true;
        }
		#endregion
    }
}