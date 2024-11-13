using System;
using System.Globalization;
using System.Text;

namespace Win.Common
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never), System.ComponentModel.Browsable(false)]
    public static class StringExtensions
    {
        /// <summary>
        /// Pascal 规则,每个单词开头的字母大写(如 TestCounter)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPascal(this string str)
        {
            if (str.IndexOf("_", StringComparison.Ordinal) > -1)
            {
                StringBuilder sb = new StringBuilder();
                string[] s = str.Split('_');
                foreach (string s1 in s)
                {
                    sb.Append(s1.ToFirstUpper());
                }
                str = sb.ToString();
            }
            else
            {
                str = str.ToFirstUpper();
            }
            
            return str;
        }
    }
}