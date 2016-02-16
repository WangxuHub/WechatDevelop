using System;
using System.Collections;
using System.Collections.Generic;

namespace DBHelper.Model
{
	public class ParameterList:System.Collections.Generic.Dictionary<string,object>
    {
		public ParameterList()
        {
        }
        public ParameterList(string parameterName, object parameterValue)
        {
            this.Add(parameterName, parameterValue);
        }
		public ParameterList(string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
        {
            this.Add(parameterName1, parameterValue1);
			this.Add(parameterName2, parameterValue2);
        }
        public ParameterList(string parameterName1, object parameterValue1, string parameterName2, object parameterValue2,string parameterName3, object parameterValue3)
        {
            this.Add(parameterName1, parameterValue1);
			this.Add(parameterName2, parameterValue2);
			this.Add(parameterName3, parameterValue3);
        }
		public ParameterList(string parameterName1, object parameterValue1, string parameterName2, object parameterValue2,string parameterName3, object parameterValue3, string parameterName4, object parameterValue4)
        {
			this.Add(parameterName1, parameterValue1);
			this.Add(parameterName2, parameterValue2);
			this.Add(parameterName3, parameterValue3);
            this.Add(parameterName4, parameterValue4);
        }
        public ParameterList(string parameterName1, object parameterValue1, string parameterName2, object parameterValue2,string parameterName3, object parameterValue3, string parameterName4, object parameterValue4, string parameterName5, object parameterValue5)
        {
		    this.Add(parameterName1, parameterValue1);
			this.Add(parameterName2, parameterValue2);
			this.Add(parameterName3, parameterValue3);
            this.Add(parameterName4, parameterValue4);
            this.Add(parameterName5, parameterValue5);
        }
    }
	/// <summary>
    /// 递归查询的方式，Parent：查询实体的同时也查询实体的外键属性；Child：查询实体的同时也查询子实体集合；None只查询实体
    /// </summary>
	public enum RecursiveType {Parent, Child, None}
}