using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DBHelper.Model;
using DBHelper.ObjectQuery;
using System.Collections.Generic;

namespace DBHelper.DAL
{
    internal partial class DJCUser
	{
	    #region 插入实体操作部份
	    /// <summary>
        /// 插入
        /// </summary>
		/// <param name="cmd">Command对象</param>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
		internal static Guid Insert(SqlCommand cmd, JCUser jCUser)
		{
		    cmd.Parameters.Clear();
			cmd.CommandText = "insert into JC_User (UserName,PassWord,NickName,TrueName,Email,Phone,QQ,CreateTime,LastLoginTime,Birthday) output inserted.UserID values (@UserName,@PassWord,@NickName,@TrueName,@Email,@Phone,@QQ,@CreateTime,@LastLoginTime,@Birthday)";
			//从实体中取出值放入Command的参数列表
			cmd.Parameters.Add(new SqlParameter("@UserID",jCUser.UserID==null?(object)DBNull.Value:(object)jCUser.UserID));
			cmd.Parameters.Add(new SqlParameter("@UserName",jCUser.UserName==null?(object)DBNull.Value:(object)jCUser.UserName));
			cmd.Parameters.Add(new SqlParameter("@PassWord",jCUser.PassWord==null?(object)DBNull.Value:(object)jCUser.PassWord));
			cmd.Parameters.Add(new SqlParameter("@NickName",jCUser.NickName==null?(object)DBNull.Value:(object)jCUser.NickName));
			cmd.Parameters.Add(new SqlParameter("@TrueName",jCUser.TrueName==null?(object)DBNull.Value:(object)jCUser.TrueName));
			cmd.Parameters.Add(new SqlParameter("@Email",jCUser.Email==null?(object)DBNull.Value:(object)jCUser.Email));
			cmd.Parameters.Add(new SqlParameter("@Phone",jCUser.Phone==null?(object)DBNull.Value:(object)jCUser.Phone));
			cmd.Parameters.Add(new SqlParameter("@QQ",jCUser.QQ==null?(object)DBNull.Value:(object)jCUser.QQ));
			cmd.Parameters.Add(new SqlParameter("@CreateTime",jCUser.CreateTime.HasValue?(object)jCUser.CreateTime.Value:(object)DBNull.Value));
			cmd.Parameters.Add(new SqlParameter("@LastLoginTime",jCUser.LastLoginTime.HasValue?(object)jCUser.LastLoginTime.Value:(object)DBNull.Value));

			cmd.Parameters.Add(new SqlParameter("@Birthday",jCUser.Birthday.HasValue?(object)jCUser.Birthday.Value:(object)DBNull.Value));
            
			return Guid.Parse(cmd.ExecuteScalar().ToString());

		}
	    /// <summary>
        /// 不使用事务的插入方法
        /// </summary>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
	    internal static Guid Insert(JCUser jCUser)
		{
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
			{
				conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return Insert(cmd, jCUser);
                }
			}
		}
		
		/// <summary>
        /// 使用事务的插入方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
        internal static Guid Insert(Connection connection,JCUser jCUser)
        {
            return Insert(connection.Command, jCUser);
        }
		#endregion
		
		#region 删除实体操作
		
		/// <summary>
        /// 删除
        /// </summary>
		/// <param name="cmd">Command对象</param>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
		internal static int ExcuteDeleteCommand(SqlCommand cmd, JCUser jCUser)
        {
			cmd.Parameters.Clear();
            cmd.CommandText = "delete from JC_User where UserID=@UserID";
            //从实体中取出值放入Command的参数列表
		    cmd.Parameters.Add(new SqlParameter("@UserID", jCUser.UserID));
            return cmd.ExecuteNonQuery();
        }
		/// <summary>
        /// 不使用事务的删除方法
        /// </summary>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        internal static int Delete(JCUser jCUser)
        {
            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteDeleteCommand(cmd, jCUser);
                }
            }
        }
		/// <summary>
        /// 使用事务的删除方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        internal static int Delete(Connection connection,JCUser jCUser)
        {
            return  ExcuteDeleteCommand(connection.Command, jCUser);
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
            string filterString = SyntaxAnalyzer.ParseSql(oql, new JCUserMap());
            if (filterString != string.Empty)
            {
                filterString = " where " + filterString;
            }
            cmd.Parameters.Clear();
            cmd.CommandText = "delete from JC_User " + filterString;
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
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
		internal static int ExcuteUpdateCommand(SqlCommand cmd, JCUser jCUser)
		{
		    cmd.CommandText = "update JC_User set UserID=@UserID,UserName=@UserName,PassWord=@PassWord,NickName=@NickName,TrueName=@TrueName,Email=@Email,Phone=@Phone,QQ=@QQ,CreateTime=@CreateTime,LastLoginTime=@LastLoginTime,Birthday=@Birthday where UserID=@UserID";
			//从实体中取出值放入Command的参数列表
			cmd.Parameters.Add(new SqlParameter("@UserName",jCUser.UserName==null?(object)DBNull.Value:(object)jCUser.UserName));
			cmd.Parameters.Add(new SqlParameter("@PassWord",jCUser.PassWord==null?(object)DBNull.Value:(object)jCUser.PassWord));
			cmd.Parameters.Add(new SqlParameter("@NickName",jCUser.NickName==null?(object)DBNull.Value:(object)jCUser.NickName));
			cmd.Parameters.Add(new SqlParameter("@TrueName",jCUser.TrueName==null?(object)DBNull.Value:(object)jCUser.TrueName));
			cmd.Parameters.Add(new SqlParameter("@Email",jCUser.Email==null?(object)DBNull.Value:(object)jCUser.Email));
			cmd.Parameters.Add(new SqlParameter("@Phone",jCUser.Phone==null?(object)DBNull.Value:(object)jCUser.Phone));
			cmd.Parameters.Add(new SqlParameter("@QQ",jCUser.QQ==null?(object)DBNull.Value:(object)jCUser.QQ));
			cmd.Parameters.Add(new SqlParameter("@CreateTime",jCUser.CreateTime.HasValue?(object)jCUser.CreateTime.Value:(object)DBNull.Value));
			cmd.Parameters.Add(new SqlParameter("@LastLoginTime",jCUser.LastLoginTime.HasValue?(object)jCUser.LastLoginTime.Value:(object)DBNull.Value));
			cmd.Parameters.Add(new SqlParameter("@Birthday",jCUser.Birthday.HasValue?(object)jCUser.Birthday.Value:(object)DBNull.Value));
			cmd.Parameters.Add(new SqlParameter("@UserID", jCUser.UserID));
            return cmd.ExecuteNonQuery();
		}
		
		/// <summary>
        /// 不使用事务的更新方法
        /// </summary>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
	    internal static int Update(JCUser jCUser)
		{
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
			{
				conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    return ExcuteUpdateCommand(cmd, jCUser);
                }
			}
		}
		/// <summary>
        /// 使用事务的更新方法
        /// </summary>
        /// <param name="connection">实现共享Connection的对象</param>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        internal static int Update(Connection connection,JCUser jCUser)
        {
            return ExcuteUpdateCommand(connection.Command, jCUser);
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
            string updateString = SyntaxAnalyzer.ParseSql(oql, new JCUserMap());
            cmd.CommandText = "update JC_User set " + updateString;
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
        internal static List<JCUser> ExcuteSelectCommand(SqlCommand cmd,RecursiveType recursiveType,int recursiveDepth)
        {
            List<JCUser> jCUserList = new List<JCUser>();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    JCUser jCUser = DataReaderToEntity(dr);
                    jCUserList.Add(jCUser);
                }
            }
			return jCUserList;
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
        internal static List<JCUser> ExcuteSelectCommand(SqlCommand cmd, string oql, ParameterList parameters,RecursiveType recursiveType,int recursiveDepth)
        {
            //解析过滤部份Sql语句
            string filterString = SyntaxAnalyzer.ParseSql(oql, new JCUserMap());
            if (filterString != string.Empty)
            {
				if(filterString.Trim().ToLower().IndexOf("order ")!=0)
                	filterString = " where " + filterString;
            }
            cmd.Parameters.Clear();
            cmd.CommandText = "select * from JC_User " + filterString;
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
        internal static List<JCUser> Select()
        {
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from JC_User";
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
        internal static List<JCUser> Select(RecursiveType recursiveType, int recursiveDepth)
        {
			using(SqlConnection conn=new SqlConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from JC_User";
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
        internal static List<JCUser> Select(string oql, ParameterList parameters)
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
        internal static List<JCUser> Select(string oql, ParameterList parameters,RecursiveType recursiveType, int recursiveDepth)
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
        internal static List<JCUser> Select(Connection connection, string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
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
		internal static JCUser ExcuteSelectSingleCommand(SqlCommand cmd,RecursiveType recursiveType,int recursiveDepth)
		{
			JCUser jCUser=null;
			using (SqlDataReader dr = cmd.ExecuteReader())
            {
			    if(dr.Read())
				    jCUser = DataReaderToEntity(dr);
			}
			if(jCUser==null)
			    return jCUser;
            return jCUser;
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
        internal static JCUser ExcuteSelectSingleCommand(SqlCommand cmd, string oql, ParameterList parameters,RecursiveType recursiveType,int recursiveDepth)
        {
            //解析过滤部份Sql语句
            string filterString = SyntaxAnalyzer.ParseSql(oql, new JCUserMap());
			if(filterString!=string.Empty)
			{
			    filterString=" where "+filterString;
			}
            cmd.CommandText = "select * from JC_User " + filterString;
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
        internal static JCUser SelectSingle(string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
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
        internal static JCUser SelectSingle(string oql, ParameterList parameters)
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
        internal static JCUser SelectSingle(Connection connection, string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
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
        internal static JCUser SelectSingle(SqlCommand cmd, string userID,RecursiveType recursiveType,int recursiveDepth)
		{
		    cmd.Parameters.Clear();
			if(userID==null)
			{
		    	cmd.CommandText = "select * from JC_User where UserID is null";
			}
			else
			{
			    cmd.CommandText = "select * from JC_User where UserID=@pk";
				cmd.Parameters.Add(new SqlParameter("@pk",userID));
			}
			return ExcuteSelectSingleCommand(cmd, recursiveType, recursiveDepth);
		}
		
		/// <summary>
        /// 按主键字段查询特定实体
        /// </summary>
        /// <param name="userID">主键值</param>
        /// <returns>实体类对象</returns>
        internal static JCUser SelectSingle(string userID)
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
        internal static JCUser SelectSingle(string userID, RecursiveType recursiveType, int recursiveDepth)
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
        internal static JCUser SelectSingle(Connection connection,string userID, RecursiveType recursiveType, int recursiveDepth)
        {
			return SelectSingle(connection.Command, userID, recursiveType, recursiveDepth);
        }
		#endregion
		
				
		/// <summary>
        /// 从DataReader中取出值生成实体对象
        /// </summary>
        /// <param name="searcher">查询对象</param>
        /// <returns>过滤条件字符串</returns>
		private static JCUser DataReaderToEntity(SqlDataReader dr)
		{
		    JCUser entity = new JCUser ();
			if(dr["UserID"]!=System.DBNull.Value)
			{
			    Guid tempGuid = new Guid();Guid.TryParse(dr["UserID"].ToString(),out tempGuid);entity.UserID = tempGuid;;
			}
			if(dr["UserName"]!=System.DBNull.Value)
			{
			    entity.UserName=dr["UserName"].ToString();
			}
			if(dr["PassWord"]!=System.DBNull.Value)
			{
			    entity.PassWord=dr["PassWord"].ToString();
			}
			if(dr["NickName"]!=System.DBNull.Value)
			{
			    entity.NickName=dr["NickName"].ToString();
			}
			if(dr["TrueName"]!=System.DBNull.Value)
			{
			    entity.TrueName=dr["TrueName"].ToString();
			}
			if(dr["Email"]!=System.DBNull.Value)
			{
			    entity.Email=dr["Email"].ToString();
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
			if(dr["LastLoginTime"]!=System.DBNull.Value)
			{
			    entity.LastLoginTime=Convert.ToDateTime(dr["LastLoginTime"]);
			}
			if(dr["Birthday"]!=System.DBNull.Value)
			{
			    entity.Birthday=Convert.ToDateTime(dr["Birthday"]);
			}
			return entity;
		}
	}
}
