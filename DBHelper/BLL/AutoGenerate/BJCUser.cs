using System;
using DBHelper.DAL;
using DBHelper.Model;
using System.Collections;
using System.Collections.Generic;

namespace DBHelper.BLL
{
    public partial class BJCUser
	{
	    #region 插入实体操作部份
	    /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>标识列值或影响的记录行数</returns>
	    public static Guid Insert(JCUser jCUser)
		{
			return DJCUser.Insert(jCUser);
		}
		#endregion
		
		#region 删除实体操作
		/// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
        public static int Delete(JCUser jCUser)
        {
            return DJCUser.Delete(jCUser);
        }
		/// <summary>
        /// 根据对象查询语句删除
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        public static int Delete(string oql, ParameterList parameters)
        {
            return DJCUser.Delete(oql,parameters);
        }
		#endregion
		
		#region 更新实体操作
				
		/// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="jCUser">实体类对象</param>
        /// <returns>影响的记录行数</returns>
	    public static int Update(JCUser jCUser)
		{
		    return DJCUser.Update(jCUser);
		}
		
		/// <summary>
        /// 根据对象查询语句更新实体
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响的记录行数</returns>
        public static int Update(string oql, ParameterList parameters)
        {
            return DJCUser.Update(oql,parameters);
        }
		#endregion
		
		#region 查询实体集合
		/// <summary>
        /// \查询实体集合
        /// </summary>
        /// <returns>实体类对象集合</returns>
        public static List<JCUser> Select()
        {
			return DJCUser.Select();
        }
		/// <summary>
        /// 递归查询实体集合
        /// </summary>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象集合</returns>
        public static List<JCUser> Select(RecursiveType recursiveType, int recursiveDepth)
        {
			return DJCUser.Select(recursiveType, recursiveDepth);
        }
		
		/// <summary>
        /// 根据对象查询语句查询实体集合
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>实体类对象集合</returns>
        public static List<JCUser> Select(string oql, ParameterList parameters)
        {
			return DJCUser.Select(oql, parameters);
        }
		
		/// <summary>
        /// 根据对象查询语句递归查询实体集合
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体类对象集合</returns>
        public static List<JCUser> Select(string oql, ParameterList parameters,RecursiveType recursiveType, int recursiveDepth)
        {
			return DJCUser.Select(oql, parameters, recursiveType, recursiveDepth);
        }
		#endregion
		
		#region 查询单个实体
		/// <summary>
        /// 更据对象查询语句查询单个实体
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>实体对象</returns>
        public static JCUser SelectSingle(string oql, ParameterList parameters)
        {
             return DJCUser.SelectSingle(oql, parameters);
        }
		/// <summary>
        /// 更据对象查询语句递归查询单个实体
        /// </summary>
        /// <param name="oql">对象查询语句</param>
        /// <param name="parameters">参数列表</param>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体对象</returns>
        public static JCUser SelectSingle(string oql, ParameterList parameters, RecursiveType recursiveType, int recursiveDepth)
        {
		    return DJCUser.SelectSingle(oql, parameters, recursiveType, recursiveDepth);
		}
		
		/// <summary>
        /// 按主键字段查询特定实体
        /// </summary>
        /// <param name="userID">主键值</param>
        /// <returns>实体类对象</returns>
        public static JCUser SelectSingle(string userID)
        {
            return DJCUser.SelectSingle(userID);
        }
		
		/// <summary>
        /// 更据主键递归查询单个实体
        /// </summary>
		/// <param name="recursiveType">递归类型</param>
        /// <param name="recursiveDepth">递归深度</param>
        /// <returns>实体对象</returns>
        public static JCUser SelectSingle(string userID, RecursiveType recursiveType, int recursiveDepth)
        {
		    return DJCUser.SelectSingle(userID, recursiveType, recursiveDepth);
		}
		#endregion
    }
}