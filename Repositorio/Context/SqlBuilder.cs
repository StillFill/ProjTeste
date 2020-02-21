using Dapper.FluentMap.Dommel.Mapping;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using Dapper.FluentMap.Mapping;

namespace Repository.Context
{
    public class SqlBuilder<TEntity, TMap>
        where TMap : IDommelEntityMap, new()
        where TEntity : class
    {

        private SqlConnection conn;
        private TMap mapping = new TMap();
        public string sqlString = "";
        private Dictionary<string, object> arguments = new Dictionary<string, object>();
        private IDommelEntityMap mappingInnerJoin;

        List<string> mappedColumns = new List<string>();

        public SqlBuilder(SqlConnection conn)
        {
            this.conn = conn;
        }

        private List<PropertyInfo> GetAttributesList<TClass>(TClass templateobject)
            where TClass : class
        {
            return templateobject.GetType().GetProperties()
                 .Where(p => p.GetValue(templateobject) != null && !p.GetValue(templateobject).ToString().Equals("0"))
                 .ToList();
        }

        private void addAttributesToSelect(IDommelEntityMap currentMap)
        {
            foreach(IPropertyMap map in currentMap.PropertyMaps)
            {
                if (map.ColumnName != map.PropertyInfo.Name)
                {
                    mappedColumns.Add(map.ColumnName + " as " + map.PropertyInfo.Name);
                } else
                {
                    mappedColumns.Add(currentMap.TableName + "." + map.PropertyInfo.Name);
                }
            }
        }

        public SqlBuilder<TEntity, TMap> Select()
        {
            sqlString = "SELECT * From " + mapping.TableName;

            this.addAttributesToSelect(mapping);

            arguments = new Dictionary<string, object>();

            return this;
        }

        public SqlBuilder<TEntity, TMap> Update()
        {
            sqlString = "UPDATE " + mapping.TableName + " SET ";
            arguments = new Dictionary<string, object>();

            return this;
        }

        private void buildArgs<TClass>(string type, TClass templateobject, List<PropertyInfo> attributesList)
        {

            int listSize = attributesList.Count;

            attributesList.ForEach(x =>
            {

                if (!sqlString.EndsWith("SET "))
                {
                    sqlString += " " + type + " ";
                }

                string prop = this.GetMappedAttribute(mapping, x.Name);

                string argSequence = arguments.Count.ToString();

                arguments.Add(argSequence, x.GetValue(templateobject));

                sqlString +=  mapping.TableName + "." + prop + " = @" + argSequence;

            });
        }

        public SqlBuilder<TEntity, TMap> Set(TEntity templateobject)
        {

            List<PropertyInfo> attributesList = this.GetAttributesList(templateobject);

            this.buildArgs(",", templateobject, attributesList);

            return this;
        }

        private void SetEqualStatement(IDommelEntityMap currentMap, string prop, object value)
        {
            string mappedCollumn = mapping.PropertyMaps.First(a => a.PropertyInfo.Name == prop).ColumnName;

            if (value.GetType().Name != "Int32")
            {
                value = "'" + value + "'";
            }

            sqlString += " WHERE " + mapping.TableName + "." + mappedCollumn + " = " + value;
        }

        public SqlBuilder<TEntity, TMap> Where(Expression<Func<TEntity, object>> f1, object value)
        {
            string prop = this.GetAttributeName(f1);

            this.SetEqualStatement(mapping, prop, value);

            return this;
        }

        public SqlBuilder<TEntity, TMap> And(TEntity templateobject)
        {
            List<PropertyInfo> attributesList = this.GetAttributesList(templateobject);

            this.buildArgs("AND", templateobject, attributesList);

            return this;
        }

        public SqlBuilder<TEntity, TMap> AndIsNull(Expression<Func<TEntity, object>> f1)
        {

            string prop = this.GetAttributeName(f1);

            sqlString += " AND " + mapping.TableName + "." + prop + " IS NULL";

            return this;
        }

        public SqlBuilder<TEntity, TMap> AndIsNotNull(Expression<Func<TEntity, object>> f1)
        {

            string prop = this.GetAttributeName(f1);

            sqlString += " AND " + mapping.TableName + "." + prop + " IS NOT NULL";

            return this;
        }

        public SqlBuilder<TEntity, TMap> Or(TEntity templateobject)
        {
            List<PropertyInfo> attributesList = this.GetAttributesList(templateobject);

            this.buildArgs("OR", templateobject, attributesList);

            return this;
        }

        public SqlBuilder<TEntity, TMap> Innerjoin<TJoinMap>()
            where TJoinMap : IDommelEntityMap, new()
        {
            TJoinMap joinMapping = new TJoinMap();

            mappingInnerJoin = joinMapping;

            sqlString += " INNER JOIN " + joinMapping.TableName;

            this.addAttributesToSelect(joinMapping);

            return this;
        }

        public string GetAttributeName<T>(Expression<Func<T, object>> func)
        {
            MemberExpression body = func.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)func.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        private string GetMappedAttribute(IDommelEntityMap mapp, string prop)
        {
            return mapp.PropertyMaps.First(a => a.PropertyInfo.Name == prop).ColumnName;
        }

        public SqlBuilder<TEntity, TMap> On<T1>(Expression<Func<TEntity, object>> f1, Expression<Func<T1, object>> f2)
        {
            string nomeF1 = this.GetAttributeName(f1);

            string nomeF2 = this.GetAttributeName(f2);

            string column1 = this.GetMappedAttribute(mapping, nomeF1);
            string column2 = this.GetMappedAttribute(mappingInnerJoin, nomeF2);

            sqlString += " ON " + mapping.TableName + "." + column1 + " = " + mappingInnerJoin.TableName + "." + column2;

            return this;
        }

        public string GetQuery()
        {

            string attrs = "";

            int i = 0;

            foreach(string mapCol in mappedColumns)
            {

                if (i == mappedColumns.Count - 1)
                {
                    attrs += mapCol;
                } 
                else
                {
                    attrs += mapCol + ", ";
                }

                i++;
            }

            sqlString = sqlString.Replace("*", attrs);

            return sqlString;
        }

        public Dictionary<string, object> GetArgs()
        {
            return arguments;
        }
    }
}
