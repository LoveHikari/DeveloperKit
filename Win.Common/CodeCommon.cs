using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Win.Models;

/******************************************************************************************************************
 * 
 * 
 * 说　明： 类型转换(版本：Version1.0.0)
 * 作　者：YuXiaoWei
 * 日　期：2016/08/29
 * 修　改：
 * 参　考：http://www.csframework.com/archive/2/arc-2-20110317-1144.htm
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace Win.Common
{
    public class CodeCommon:System.CodeCommon
    {
        /// <summary>
        /// 获得主键或标识列列表
        /// </summary>
        /// <param name="fieldlist"></param>
        public static List<ColumnModel> GetPrimaryKeyList(List<ColumnModel> fieldlist)
        {
            List<ColumnModel> keyList = new List<ColumnModel>();
            foreach (ColumnModel field in fieldlist)
            {
                if (field.IsPrimaryKey || field.IsIdentity)
                {
                    keyList.Add(field);
                }
            }
            return keyList;
        }

        /// <summary>
        /// 主键列表中是否有标识列
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool IsHasIdentity(List<ColumnModel> keys)
        {
            bool flag = false;
            if (keys.Count > 0)
            {
                foreach (ColumnModel key in keys)
                {
                    if (key.IsIdentity)
                        flag = true;
                }
            }
            return flag;
        }
    }
}