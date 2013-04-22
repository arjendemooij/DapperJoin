using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperJoin
{
    public class TableAliasRegistry
    {
        private static List<string> TableAliases { get; set; }

        public static void Register(string tableAlias) {
            Init();
            if (TableAliases.Contains(tableAlias))
                throw new ArgumentException(string.Format("Tablealias already exists {0}", tableAlias));
            TableAliases.Add(tableAlias);
        }

        public static string Generate(string tableName)
        {
            Init();
            string alias;

            string defaultAlias = tableName.ToLower();

            if (!TableAliases.Contains(defaultAlias))
                alias=  defaultAlias;
            else
            {
                int index = 2;
                while (TableAliases.Contains(defaultAlias + index))
                {
                    index++;
                }

                alias = defaultAlias + index;
            }

            Register(alias);

            return alias;
        }

        private static void Init()
        {
            if (TableAliases == null) TableAliases = new List<string>();
        }
    }
}
