using System;
using System.Collections.Generic;
using System.Text;
using Win.Models;

namespace Win.Common.Builder
{
    /// <summary>
    /// 可系列化的Model代码生成组件
    /// </summary>
    public class BuilderModel2
    {
        #region 字段
        private string _modelpath;  //实体类的命名空间
        private List<ColumnModel> _fieldlist;  //选择的字段集合
        private string _modelName;  //实体类名
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fieldlist">选择的字段集合</param>
        /// <param name="modelpath">实体类的命名空间</param>
        /// <param name="modelSuffix">实体类的后缀</param>
        public BuilderModel2(List<ColumnModel> fieldlist, string modelpath, string modelSuffix)
        {
            
            _fieldlist = fieldlist;
            _modelpath = modelpath;
            string tableName = _fieldlist[0].TableName;
            _modelName = tableName.ToPascal() + modelSuffix;
        }
        /// <summary>
        /// 实体类的命名空间
        /// </summary>
        public string Modelpath { get => _modelpath; set => _modelpath = value; }
        /// <summary>
        /// 选择的字段集合
        /// </summary>
        public List<ColumnModel> Fieldlist { get => _fieldlist; set => _fieldlist = value; }
        /// <summary>
        /// 实体类名
        /// </summary>
        public string ModelName { get => _modelName; set => _modelName = value; }


        /// <summary>
        /// 生成完整Model类
        /// </summary>
        public virtual string CreatModel()
        {
            StringBuilder strclass = new StringBuilder();
            strclass.AppendLine("using System;");
            strclass.AppendLine("using System.Runtime.Serialization;");
            strclass.AppendLine("namespace " + _modelpath);
            strclass.AppendLine("{");
            strclass.AppendSpaceLine(1, "/// <summary>");
            if (_fieldlist[0].TableDescription.Length > 0)
            {
                strclass.AppendSpaceLine(1, "/// " + _fieldlist[0].TableDescription.Replace("\r\n", "\r\n\t///"));
            }
            else
            {
                strclass.AppendSpaceLine(1, "/// " + _fieldlist[0].TableName + ":实体类(属性说明自动提取数据库字段的描述信息)");
            }
            strclass.AppendSpaceLine(1, "/// </summary>");
            strclass.AppendSpaceLine(1, "[Serializable]");
            strclass.AppendSpaceLine(1, "public partial class " + _modelName);
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendLine(CreatModelMethod());
            strclass.AppendSpaceLine(1, "}");
            strclass.AppendLine("}");
            strclass.AppendLine("");

            return strclass.ToString();
        }


        /// <summary>
        /// 生成实体类的属性
        /// </summary>
        /// <returns></returns>
        public virtual string CreatModelMethod()
        {
            int i = 1;
            StringBuilder strclass = new StringBuilder();
            StringBuilder strclass2 = new StringBuilder();
            strclass.AppendSpaceLine(2, "#region Model");
            foreach (ColumnModel field in _fieldlist)
            {
                string columnName = field.ColumnName;
                string columnTypedb = field.TypeName;
                bool isIdentity = field.IsIdentity;
                bool ispk = field.IsPrimaryKey;
                bool cisnull = field.IsCanNull;
                //string defValue=field.DefaultVal;
                string deText = field.Description;
                string columnType = Win.Common.CodeCommon.DbTypeToCS(columnTypedb);
                string isnull = "";
                if ((!isIdentity) && (!ispk) && (cisnull) && (columnType!="string"))
                {
                    isnull = "?";//代表可空类型
                }

                strclass2.AppendSpaceLine(2, "/// <summary>");
                strclass2.AppendSpaceLine(2, "/// " + deText);
                strclass2.AppendSpaceLine(2, "/// </summary>");
                strclass2.AppendSpaceLine(2, "public " + columnType + isnull + " " + columnName.ToFirstUpper() + " { get; set; }");//属性
                i++;
            }
            strclass.Append(strclass2);
            strclass.AppendSpaceLine(2, "#endregion Model");

            return strclass.ToString();
        }




    }
}
