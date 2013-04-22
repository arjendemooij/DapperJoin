using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperJoin
{
    internal class ParentChildRelation<TParent, TChild> : IRelation
    {
        protected Table ParentTable { get; set; }
        protected Table ChildTable { get; set; }
        public bool ChildRequired { get; protected set; }

        public ParentChildRelation(bool childRequired)
        {
            ParentTable = new Table(typeof(TParent), null);
            ChildTable = new Table(typeof(TChild), null);
            ParentFieldName = "Id";
            ChildFieldName = ParentTable.TableName + "Id";
            ChildRequired = childRequired;
        }

        public bool Matches(Type t1, Type t2)
        {
            return (ParentTable.ItemType == t1 && ChildTable.ItemType == t2) || (ParentTable.ItemType == t2 && ChildTable.ItemType == t1);
        }

        public virtual SubQuery GetChildquery(SubQuery subQuery)
        {
            if (ParentTable.ItemType == ChildTable.ItemType)
                return subQuery;
            else if (ParentTable.ItemType == subQuery.Table.ItemType)
                return subQuery.JoinedTo;
            else
                return subQuery;
        }

        public virtual SubQuery GetParentQuery(SubQuery subQuery)
        {
            if (ParentTable.ItemType == subQuery.Table.ItemType)
                return subQuery;
            else
                return subQuery.JoinedTo;
        }

        public string ParentFieldName { get; set; }
        public string ChildFieldName { get; set; }
    }
}
