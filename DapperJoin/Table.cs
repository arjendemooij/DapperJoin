using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperJoin
{
    public class Table
    {

        public Table(Type itemType, string tableName)
        {
            this.ItemType = itemType;
            TableName = tableName ?? itemType.Name;
        }
        public Type ItemType { get; set; }
        public string TableName { get; set; }
    }
}
