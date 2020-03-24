using Dapper.FluentMap.Dommel.Mapping;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository.Context
{
    public class SqlBuilder<TEntity, TMap>
        where TMap : IDommelEntityMap, new()
        where TEntity : class
    {
        private TMap mapping = new TMap();
        private string sqlString = string.Empty;
        private Dictionary<string, object> arguments = new Dictionary<string, object>();
        private IDommelEntityMap mappingInnerJoin;

        List<string> mappedColumns = new List<string>();
        List<string> mappedOrdered = new List<string>();

        private List<PropertyInfo> GetAttributesList<TClass>(TClass templateobject)
            where TClass : class
        {
            return templateobject.GetType().GetProperties()
                 .Where(p => p.GetValue(templateobject) != null && !p.GetValue(templateobject).ToString().Equals("0"))
                 .ToList();
        }

        private void ResetValues()
        {
            arguments = new Dictionary<string, object>();
            mapping = new TMap();
            sqlString = "";
            mappingInnerJoin = null;
            mappedColumns = new List<string>();
        }

        private void addStatementWithArgs<TClass>(string separator, TClass templateobject, List<PropertyInfo> attributesList)
        {
            attributesList.ForEach(attr =>
            {
                if (!sqlString.EndsWith("SET "))
                {
                    sqlString += $" {separator} ";
                }

                string fieldName = this.GetMappedAttribute(mapping, attr.Name);

                string argSequence = arguments.Count.ToString();

                arguments.Add(argSequence, attr.GetValue(templateobject));

                sqlString += $"{mapping.TableName}.{fieldName} = @{argSequence}";

            });
        }

        private void addAttributesToSelect(IDommelEntityMap currentMap)
        {
            foreach (IPropertyMap map in currentMap.PropertyMaps)
            {
                mappedColumns.Add($"{currentMap.TableName}.{map.ColumnName} as {map.PropertyInfo.Name}");
            }
        }

        private void addAttributesToSelect(IDommelEntityMap currentMap, Expression<Func<TEntity, object>>[] columns)
        {
            var columnsAttrName = columns.Select(func => this.GetAttributeName(func));

            foreach (IPropertyMap map in currentMap.PropertyMaps.Where(a => columnsAttrName.Contains(a.PropertyInfo.Name)))
            {
                mappedColumns.Add($"{currentMap.TableName}.{map.ColumnName} as {map.PropertyInfo.Name}");
            }
        }

        private void addAttributesToOrderBy(IDommelEntityMap currentMap, Expression<Func<TEntity, object>>[] columns)
        {
            var columnsAttrName = columns.Select(func => this.GetAttributeName(func));

            foreach (IPropertyMap map in currentMap.PropertyMaps.Where(a => columnsAttrName.Contains(a.PropertyInfo.Name)))
            {
                mappedOrdered.Add($"{currentMap.TableName}.{map.ColumnName}");
            }
        }

        private void addWhereToString(IDommelEntityMap currentMap, string prop, object value)
        {
            string mappedCollumn = currentMap.PropertyMaps.First(a => a.PropertyInfo.Name == prop).ColumnName;

            string valueSql = valueToSqlString(value);

            sqlString += $" WHERE {currentMap.TableName}.{mappedCollumn} = {valueSql} ";
        }

        private string valueToSqlString(object value)
        {
            string result = string.Empty;

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Boolean:
                    result = (bool)value ? "1" : "0";
                    break;
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    result = string.Format("{0}", value);
                    break;
                case TypeCode.DateTime:
                    result = string.Format("'{0}'", ((DateTime)value).ToString("yyyyMMdd"));
                    break;
                case TypeCode.Char:
                case TypeCode.String:
                    result = string.Format("'{0}'", value);
                    break;
            }

            return result;
        }

        private string GetAttributeName<T>(Expression<Func<T, object>> func)
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

        private string writeMapped(List<string> mapList)
        {
            string result = string.Empty;

            int i = 0;

            foreach (string mapCol in mapList)
            {

                if (i == mapList.Count - 1)
                {
                    result += mapCol;
                }
                else
                {
                    result += mapCol + ", ";
                }

                i++;
            }

            return result;

        }

        public SqlBuilder<TEntity, TMap> Select(params Expression<Func<TEntity, object>>[] columns)
        {
            this.ResetValues();

            sqlString = $" SELECT * FROM {mapping.TableName} ";

            if (columns == null)
            {
                this.addAttributesToSelect(mapping);
            }
            else
            {
                this.addAttributesToSelect(mapping, columns);
            }

            return this;
        }

        public SqlBuilder<TEntity, TMap> Distinct()
        {
            sqlString = sqlString.Replace(" SELECT ", " SELECT DISTINCT ");

            return this;
        }

        public SqlBuilder<TEntity, TMap> Top(int count = 1)
        {
            if (count > 0)
            {
                if (sqlString.Contains("DISTINCT"))
                {
                    sqlString = sqlString.Replace(" DISTINCT ", $" DISTINCT TOP {count} ");
                }
                else
                {
                    sqlString = sqlString.Replace(" SELECT ", $" SELECT TOP {count} ");
                }
            }

            return this;
        }

        public SqlBuilder<TEntity, TMap> Update()
        {
            this.ResetValues();

            sqlString = $" UPDATE {mapping.TableName} SET ";

            return this;
        }

        public SqlBuilder<TEntity, TMap> Set(TEntity templateobject)
        {
            List<PropertyInfo> attributesList = this.GetAttributesList(templateobject);

            this.addStatementWithArgs(",", templateobject, attributesList);

            return this;
        }

        public SqlBuilder<TEntity, TMap> Where(Expression<Func<TEntity, object>> func, object value)
        {
            string prop = this.GetAttributeName(func);

            this.addWhereToString(mapping, prop, value);

            return this;
        }

        public SqlBuilder<TEntity, TMap> And(TEntity templateobject)
        {
            List<PropertyInfo> attributesList = this.GetAttributesList(templateobject);

            this.addStatementWithArgs("AND", templateobject, attributesList);

            return this;
        }

        public SqlBuilder<TEntity, TMap> AndIsNull(Expression<Func<TEntity, object>> func)
        {
            string prop = this.GetAttributeName(func);

            sqlString += $" AND {mapping.TableName}.{prop} IS NULL";

            return this;
        }

        public SqlBuilder<TEntity, TMap> AndIsNotNull(Expression<Func<TEntity, object>> func)
        {
            string prop = this.GetAttributeName(func);

            sqlString += $" AND {mapping.TableName}.{prop} IS NOT NULL";

            return this;
        }

        public SqlBuilder<TEntity, TMap> Or(TEntity templateobject)
        {
            List<PropertyInfo> attributesList = this.GetAttributesList(templateobject);

            this.addStatementWithArgs("OR", templateobject, attributesList);

            return this;
        }

        public SqlBuilder<TEntity, TMap> Innerjoin<TJoinMap>()
            where TJoinMap : IDommelEntityMap, new()
        {
            TJoinMap joinMapping = new TJoinMap();

            mappingInnerJoin = joinMapping;

            sqlString += $" INNER JOIN {joinMapping.TableName} ";

            return this;
        }

        public SqlBuilder<TEntity, TMap> Leftjoin<TJoinMap>()
            where TJoinMap : IDommelEntityMap, new()
        {
            TJoinMap joinMapping = new TJoinMap();

            mappingInnerJoin = joinMapping;

            sqlString += $" LEFT JOIN {joinMapping.TableName} ";

            return this;
        }

        public SqlBuilder<TEntity, TMap> On<T1>(Expression<Func<TEntity, object>> func1, Expression<Func<T1, object>> func2)
        {
            string attrName1 = this.GetAttributeName(func1);
            string attrName2 = this.GetAttributeName(func2);

            string columnNameMapping = this.GetMappedAttribute(mapping, attrName1);
            string columnNameInnerJoin = this.GetMappedAttribute(mappingInnerJoin, attrName2);

            sqlString += $" ON {mapping.TableName}.{columnNameMapping} = {mappingInnerJoin.TableName}.{columnNameInnerJoin} ";

            return this;
        }

        public SqlBuilder<TEntity, TMap> OrderBy(params Expression<Func<TEntity, object>>[] columns)
        {
            if (columns != null)
                this.addAttributesToOrderBy(mapping, columns);

            return this;
        }

        public string GetQuery()
        {
            if (sqlString.Contains("*"))
            {
                string columns = writeMapped(mappedColumns);
                sqlString = sqlString.Replace("*", columns);
            }
            if (mappedOrdered.Any())
            {
                string columnsOrdered = writeMapped(mappedOrdered);
                sqlString += $" ORDER BY {columnsOrdered}";
            }

            return sqlString.Trim();
        }

        public string GetFormattedQuery()
        {
            string result = this.GetQuery();

            result = result.Replace("SELECT ", $"SELECT{Environment.NewLine}\t ")
                            .Replace(" SET ", $" {Environment.NewLine}SET{Environment.NewLine}\t ")
                            .Replace(" WHERE ", $" {Environment.NewLine}WHERE{Environment.NewLine}\t ")
                            .Replace(" FROM ", $" {Environment.NewLine}FROM ")
                            .Replace(" INNER ", $" {Environment.NewLine}INNER ")
                            .Replace(" LEFT ", $" {Environment.NewLine}LEFT ")
                            .Replace(",", $",{Environment.NewLine}\t")
                            .Replace(" AND ", $" {Environment.NewLine}\tAND ")
                            .Replace(" OR ", $" {Environment.NewLine}\tOR ")
                            .Replace(" ON ", $" {Environment.NewLine}\t\tON ")
                            .Replace(" ORDER BY ", $" {Environment.NewLine}ORDER BY{Environment.NewLine}\t ");

            return result.Trim();
        }

        public Dictionary<string, object> GetArgs()
        {
            return arguments;
        }
    }
}
