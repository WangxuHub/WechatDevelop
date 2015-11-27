using System;
using System.Collections;
using System.Collections.Generic;

namespace DBObjectQuery
{
    internal class EntityMap
    {
        private static Dictionary<string, EntityInfo> entitiesMap = new Dictionary<string, EntityInfo>();
        static EntityMap()
        {
            entitiesMap.Add("wxuser",new EntityInfo("WX_User",new WXUserMap()));
		}
        public static EntityInfo GetEntityInfo(string entityName)
        {
            try
            {
                return entitiesMap[entityName.ToLower()];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("from 后有语法错误，" + entityName + "实体不存在");
            } 
        }
    }
	interface IMap
    {
        string this[string propertyName]
        {
            get;
        }
    }
    class EntityInfo
    {
        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private IMap propertyMap;
        public IMap PropertyMap
        {
            get { return propertyMap; }
            set { propertyMap = value; }
        }
        public EntityInfo(string name, IMap map)
        {
            this.tableName = name;
            this.propertyMap = map;
        }
    }
}