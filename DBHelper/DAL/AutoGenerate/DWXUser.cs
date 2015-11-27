using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DBModel;
using DBObjectQuery;
using System.Collections.Generic;

namespace DBDAL
{
    internal partial class DWXUser
	{
	    #region 插入实体操作部份
	    /// <summary>
        /// 插入
        /// </summary>
		/// <param name="cmd">Command对象</param>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
		internal static int Insert(SqlCommand cmd, WXUser wXUser)
		{
		    cmd.Parameters.Clear();
			cmd.CommandText = "insert into WX_User (OpenID,WeChatName,RemarkName,TrueName,Phone,QQ,CreateTime) values (@OpenID,@WeChatName,@RemarkName,@TrueName,@Phone,@QQ,@CreateTime);select @@identity";
			//从实体中取出值放入Command的参数列表
			cmd.Parameters.Add(new SqlParameter("@OpenID",wXUser.OpenID==null?(object)DBNull.Value:(object)wXUser.OpenID));
			cmd.Parameters.Add(new SqlParameter("@WeChatName",wXUser.WeChatName==null?(object)DBNull.Value:(object)wXUser.WeChatName));
			cmd.Parameters.Add(new SqlParameter("@RemarkName",wXUser.RemarkName==null?(object)DBNull.Value:(object)wXUser.RemarkName));
			cmd.Parameters.Add(new SqlParameter("@TrueName",wXUser.TrueName==null?(object)DBNull.Value:(object)wXUser.TrueName));
			cmd.Parameters.Add(new SqlParameter("@Phone",wXUser.Phone==null?(object)DBNull.Value:(object)wXUser.Phone));
			cmd.Parameters.Add(new SqlParameter("@QQ",wXUser.QQ==null?(object)DBNull.Value:(object)wXUser.QQ));
			cmd.Parameters.Add(new SqlParameter("@CreateTime",wXUser.CreateTime.HasValue?(object)wXUser.CreateTime.Value:(object)DBNull.Value));
			return Convert.ToInt32(cmd.ExecuteScalar());
		}
	    /// <summary>
        /// 不使用事务的插入方法
        /// </summary>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
	    internal static int Insert(WXUser wXUser)
		{
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
			{
				conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return Insert(cmd, wXUser);
                }
			}
		}
		
		/// <summary>
        /// 使用事务的插入方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
        internal static int Insert(Connection connection,WXUser wXUser)
        {
            return Insert(connection.Command, wXUser);
        }
		#endregion
		
		#region 删除实体操作
		
		/// <summary>
        /// 删除
        /// </summary>
		/// <param name="cmd">Command对象</param>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
		internal static int ExcuteDeleteCommand(SqlCommand cmd, WXUser wXUser)
        {
			cmd.Parameters.Clear();
            cmd.CommandText = "delete from WX_User where UserID=@UserID";
            //从实体中取出值放入Command的参数列表
		    cmd.Parameters.Add(new SqlParameter("@UserID", wXUser.UserID));
            return cmd.ExecuteNonQuery();
        }
		/// <summary>
        /// 不使用事务的删除方法
        /// </summary>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        internal static int Delete(WXUser wXUser)
        {
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteDeleteCommand(cmd, wXUser);
                }
            }
        }
		/// <summary>
        /// 使用事务的删除方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        internal static int Delete(Connection connection,WXUser wXUser)
        {
            return  ExcuteDeleteCommand(connection.Command, wXUser);
		}
		
		/// <summary>
        /// 执行删除命令
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        internal static int ExcuteDeleteCommand(SqlCommand cmd, string oql, ParameterList parameters)
        {
            //解析过滤部份Sql语句
            string filterString = SyntaxAnalyzer.ParseSql(oql, new WXUserMap());
            if (filterString != string.Empty)
            {
                filterString = " where " + filterString;
            }
            cmd.Parameters.Clear();
            cmd.CommandText = "delete from WX_User " + filterString;
            //添加参数
            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
            }
            return cmd.ExecuteNonQuery();
        }
		
		/// <summary>
        /// 不使用事务的删除方法
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        internal static int Delete(string oql, ParameterList parameters)
        {
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteDeleteCommand(cmd, oql, parameters);
                }
            }
        }
		
		/// <summary>
        /// 使用事务的删除方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        internal static int Delete(Connection connection, string oql, ParameterList parameters)
        {
            return ExcuteDeleteCommand(connection.Command, oql, parameters);
        }
		
		#endregion
		
		#region 更新实体操作
		
		/// <summary>
        /// 更新
        /// </summary>
		/// <param name="cmd">Command对象</param>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
		internal static int ExcuteUpdateCommand(SqlCommand cmd, WXUser wXUser)
		{
		    cmd.CommandText = "update WX_User set OpenID=@OpenID,WeChatName=@WeChatName,RemarkName=@RemarkName,TrueName=@TrueName,Phone=@Phone,QQ=@QQ,CreateTime=@CreateTime where UserID=@UserID";
			//从实体中取出值放入Command的参数列表
			cmd.Parameters.Add(new SqlParameter("@OpenID",wXUser.OpenID==null?(object)DBNull.Value:(object)wXUser.OpenID));
			cmd.Parameters.Add(new SqlParameter("@WeChatName",wXUser.WeChatName==null?(object)DBNull.Value:(object)wXUser.WeChatName));
			cmd.Parameters.Add(new SqlParameter("@RemarkName",wXUser.RemarkName==null?(object)DBNull.Value:(object)wXUser.RemarkName));
			cmd.Parameters.Add(new SqlParameter("@TrueName",wXUser.TrueName==null?(object)DBNull.Value:(object)wXUser.TrueName));
			cmd.Parameters.Add(new SqlParameter("@Phone",wXUser.Phone==null?(object)DBNull.Value:(object)wXUser.Phone));
			cmd.Parameters.Add(new SqlParameter("@QQ",wXUser.QQ==null?(object)DBNull.Value:(object)wXUser.QQ));
			cmd.Parameters.Add(new SqlParameter("@CreateTime",wXUser.CreateTime.HasValue?(object)wXUser.CreateTime.Value:(object)DBNull.Value));
			cmd.Parameters.Add(new SqlParameter("@UserID", wXUser.UserID));
            return cmd.ExecuteNonQuery();
		}
		
		/// <summary>
        /// 不使用事务的更新方法
        /// </summary>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
	    internal static int Update(WXUser wXUser)
		{
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
			{
				conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteUpdateCommand(cmd, wXUser);
                }
			}
		}
		/// <summary>
        /// 使用事务的更新方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="wXUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        internal static int Update(Connection connection,WXUser wXUser)
        {
            return ExcuteUpdateCommand(connection.Command, wXUser);
		}
		/// <summary>
        /// 执行更新命令
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        internal static int ExcuteUpdateCommand(SqlCommand cmd, string oql, ParameterList parameters)
        {
            //解析过滤部份Sql语句
            string updateString = SyntaxAnalyzer.ParseSql(oql, new WXUserMap());
            cmd.CommandText = "update WX_User set " + updateString;
			cmd.Parameters.Clear();
            //添加参数
            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
            }
            return cmd.ExecuteNonQuery();
        }
		
		/// <summary>
        /// 不使用事务的更新方法
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        internal static int Update(string oql, ParameterList parameters)
        {
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteUpdateCommand(cmd, oql, parameters);
                }
            }
        }
		
		/// <summary>
        /// 使用事务的更新方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        internal static int Update(Connection connection, string oql, ParameterList parameters)
        {
            return ExcuteUpdateCommand(connection.Command, oql, parameters);
        }
		#endregion
		
		#region 查询实体集合
		/// <summary>
        /// 执行Command获取对象列表
        /// </summary>
        /// <param name="cmd">Command对象</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象列表</returns>
        internal static List<WXUser> ExcuteSelectCommand(SqlCommand cmd,RecursiveType recursiveType,int recursiveDepth)
        {
            List<WXUser> wXUserList = new List<WXUser>();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    WXUser wXUser = DataReaderToEntity(dr);
                    wXUserList.Add(wXUser);
                }
            }
			return wXUserList;
        }
		/// <summary>
        /// 执行查询命令
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象集合</returns>
        internal static List<WXUser> ExcuteSelectCommand(SqlCommand cmd, string oql, ParameterList parameters,RecursiveType recursiveType,int recursiveDepth)
        {
            //解析过滤部份Sql语句
            string filterString = SyntaxAnalyzer.ParseSql(oql, new WXUserMap());
            if (filterString != string.Empty)
            {
				if(filterString.Trim().ToLower().IndexOf("order ")!=0)
                	filterString = " where " + filterString;
            }
            cmd.Parameters.Clear();
            cmd.CommandText = "select * from WX_User " + filterString;
            //添加参数
            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
            }
            return ExcuteSelectCommand(cmd, recursiveType, recursiveDepth);
        }
		
		/// <summary>
        /// 根据对象查询语句查询实体集合
        /// </summary>
        /// <returns>实体类对象集合</returns>
        internal static List<WXUser> Select()
        {
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from WX_User";
                    return ExcuteSelectCommand(cmd, RecursiveType.Parent, 1);
                }
            }
        }
		/// <summary>
        /// 根据对象查询语句查询实体集合
        /// </summary>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象集合</returns>
        internal static List<WXUser> Select(RecursiveType recursiveType, int recursiveDepth)
        {
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from WX_User";
                    return ExcuteSelectCommand(cmd, recursiveType, recursiveDepth);
                }
            }
        }
		
		/// <summary>
        /// 根据对象查询语句查询实体集合
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>实体类对象集合</returns>
        internal static List<WXUser> Select(string oql, ParameterList parameters)
        {
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteSelectCommand(cmd, oql, parameters, RecursiveType.Parent, 1);
                }
            }
        }
		
		/// <summary>
        /// 根据对象查询语句查询实体集合
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象集合</returns>
        internal static List<WXUser> Select(string oql, ParameterList parameters,RecursiveType recursiveType, int recursiveDepth)
        {
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteSelectCommand(cmd, oql, parameters, recursiveType, recursiveDepth);
                }
            }
        }
		
		/// <summary>
        /// 根据对象查询语句查询实体集合（启用事务）
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象集合</returns>
        internal static List<WXUser> Select(Connection connection, string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
        {
            return ExcuteSelectCommand(connection.Command, oql, parameters,recursiveType, recursiveDepth);
        }
		#endregion
		
		#region 查询单个实体
		
		/// <summary>
        /// 递归查询单个实体
        /// </summary>
        /// <param name="cmd">Command对象</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体对象</returns>
		internal static WXUser ExcuteSelectSingleCommand(SqlCommand cmd,RecursiveType recursiveType,int recursiveDepth)
		{
			WXUser wXUser=null;
			using (SqlDataReader dr = cmd.ExecuteReader())
            {
			    if(dr.Read())
				    wXUser = DataReaderToEntity(dr);
			}
			if(wXUser==null)
			    return wXUser;
            return wXUser;
		}
		/// <summary>
        /// 更据对象查询语句递归查询单个实体
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体对象</returns>
        internal static WXUser ExcuteSelectSingleCommand(SqlCommand cmd, string oql, ParameterList parameters,RecursiveType recursiveType,int recursiveDepth)
        {
            //解析过滤部份Sql语句
            string filterString = SyntaxAnalyzer.ParseSql(oql, new WXUserMap());
			if(filterString!=string.Empty)
			{
			    filterString=" where "+filterString;
			}
            cmd.CommandText = "select * from WX_User " + filterString;
			cmd.Parameters.Clear();
            //添加参数
            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, parameters[key]));
                }
            }
            return ExcuteSelectSingleCommand(cmd, recursiveType, recursiveDepth);
        }
				
		/// <summary>
        /// 更据对象查询语句递归查询单个实体
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体对象</returns>
        internal static WXUser SelectSingle(string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
        {
            using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteSelectSingleCommand(cmd, oql, parameters, recursiveType, recursiveDepth);
                }   
            } 
        }
		
		/// <summary>
        /// 更据对象查询语句查询单个实体
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>实体对象</returns>
        internal static WXUser SelectSingle(string oql, ParameterList parameters)
        {
            return SelectSingle(oql,parameters,RecursiveType.Parent,1);
        }
		
		/// <summary>
        /// 更据对象查询语句并启用事务查询单个实体
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>实体对象</returns>
        internal static WXUser SelectSingle(Connection connection, string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
        {
            return ExcuteSelectSingleCommand(connection.Command, oql, parameters, recursiveType, recursiveDepth);
        }
		
		/// <summary>
        /// 更据主键值递归查询单个实体
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="userID">主键值</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体对象</returns>
        internal static WXUser SelectSingle(SqlCommand cmd, int? userID,RecursiveType recursiveType,int recursiveDepth)
		{
		    cmd.Parameters.Clear();
			if(userID.HasValue)
			{
		    	cmd.CommandText = "select * from WX_User where UserID=@pk";
				cmd.Parameters.Add(new SqlParameter("@pk",userID.Value));
			}
			else
			{
			    cmd.CommandText = "select * from WX_User where UserID is null";
			}
			return ExcuteSelectSingleCommand(cmd, recursiveType, recursiveDepth);
		}
		
		/// <summary>
        /// 按主键字段查询特定实体
        /// </summary>
        /// <param name="userID">主键值</param>
        /// <returns>实体类对象</returns>
        internal static WXUser SelectSingle(int? userID)
        {
            using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return SelectSingle(cmd,userID,RecursiveType.Parent,1);
                }   
            } 
        }
		/// <summary>
        /// 按主键字段查询特定实体
        /// </summary>
        /// <param name="userID">主键值</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象</returns>
        internal static WXUser SelectSingle(int? userID, RecursiveType recursiveType, int recursiveDepth)
        {
            using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return SelectSingle(cmd,userID, recursiveType, recursiveDepth);
                }   
            } 
        }
		
		/// <summary>
        /// 使用事务并按主键字段查询特定实体
        /// </summary>
		/// <param name="connection">连接对象</param>
        /// <param name="userID">主键值</param>
        /// <returns>实体类对象</returns>
        internal static WXUser SelectSingle(Connection connection,int? userID, RecursiveType recursiveType, int recursiveDepth)
        {
			return SelectSingle(connection.Command, userID, recursiveType, recursiveDepth);
        }
		#endregion
		
				
		/// <summary>
        /// 从DataReader中取出值生成实体对象
        /// </summary>
        /// <param name="searcher">查询对象</param>
        /// <returns>过滤条件字符串</returns>
		private static WXUser DataReaderToEntity(SqlDataReader dr)
		{
		    WXUser entity = new WXUser ();
			if(dr["UserID"]!=System.DBNull.Value)
			{
			    entity.UserID=Convert.ToInt32(dr["UserID"]);
			}
			if(dr["OpenID"]!=System.DBNull.Value)
			{
			    entity.OpenID=dr["OpenID"].ToString();
			}
			if(dr["WeChatName"]!=System.DBNull.Value)
			{
			    entity.WeChatName=dr["WeChatName"].ToString();
			}
			if(dr["RemarkName"]!=System.DBNull.Value)
			{
			    entity.RemarkName=dr["RemarkName"].ToString();
			}
			if(dr["TrueName"]!=System.DBNull.Value)
			{
			    entity.TrueName=dr["TrueName"].ToString();
			}
			if(dr["Phone"]!=System.DBNull.Value)
			{
			    entity.Phone=dr["Phone"].ToString();
			}
			if(dr["QQ"]!=System.DBNull.Value)
			{
			    entity.QQ=dr["QQ"].ToString();
			}
			if(dr["CreateTime"]!=System.DBNull.Value)
			{
			    entity.CreateTime=Convert.ToDateTime(dr["CreateTime"]);
			}
			return entity;
		}
	}
}
