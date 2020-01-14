using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Wboy.Infrastructure.Core.Util.Tree
{
    public static class QueryTree
    {
        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<T> TreeWhere<T>(this List<T> entityList, Predicate<T> condition, string primaryKey, string parentId = "ParentId") where T : class
        {
            List<T> locateList = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            //模糊查询表达式
            List<T> treeList = new List<T>();
            foreach (T entity in locateList)
            {
                //先把自己加入进来
                treeList.Add(entity);
                //向上查询
                string pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }
                    Predicate<T> upLambda = (Expression.Equal(parameter.Property(primaryKey), Expression.Constant(pId))).ToLambda<Predicate<T>>(parameter).Compile();
                    T upRecord = entityList.Find(upLambda);
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }

        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }

        public static Expression<T> ToLambda<T>(this Expression body, params ParameterExpression[] parameters)
        {
            return Expression.Lambda<T>(body, parameters);
        }
    }

    public static class TreeJson
    {
        /// <summary>
        /// 转换树Json
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="ParentId">父节点</param>
        /// <returns></returns>
        public static string TreeToJson(this List<TreeModel> list, string ParentId = "")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeModel> item = list.FindAll(t => t.parentId == ParentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    if (entity.text != null && !string.IsNullOrEmpty(entity.text))
                    {
                        strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    }
                    strJson.Append("\"value\":\"" + entity.value + "\",");
                    if (entity.Attribute != null && !string.IsNullOrEmpty(entity.Attribute))
                    {
                        strJson.Append("\"" + entity.Attribute + "\":\"" + entity.AttributeValue + "\",");
                    }
                    if (entity.AttributeA != null && !string.IsNullOrEmpty(entity.AttributeA))
                    {
                        strJson.Append("\"" + entity.AttributeA + "\":\"" + entity.AttributeValueA + "\",");
                    }
                    if (entity.title != null && !string.IsNullOrEmpty(entity.title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.img != null && !string.IsNullOrEmpty(entity.img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    if (entity.parentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.parentId + "\",");
                    }
                    if (entity.Level != null)
                    {
                        strJson.Append("\"Level\":" + entity.Level + ",");
                    }
                    strJson.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeToJson(list, entity.id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }

        public static string TreeViewToJson(this List<TreeModel> list, string ParentId = null)
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeModel> item = list.FindAll(t => t.parentId == ParentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    if (entity.text != null && !string.IsNullOrEmpty(entity.text))
                    {
                        strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    }

                    if (entity.hasChildren)
                    {
                        strJson.Append("\"nodes\":" + TreeViewToJson(list, entity.id) + ",");
                    }
                    strJson.Append("\"tags\":" + "[\"\"]");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
