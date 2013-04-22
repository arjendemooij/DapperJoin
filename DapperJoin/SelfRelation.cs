using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperJoin
{
    internal class SelfRelation<T> : ParentChildRelation<T, T>
    {
        public SelfRelation()
            : base(childRequired: true)
        {
            ChildFieldName = "Id";
        }

        public override SubQuery GetParentQuery(SubQuery subQuery)
        {
            return subQuery.JoinedTo;
        }

        public override SubQuery GetChildquery(SubQuery subQuery)
        {
            return subQuery;

        }
    }
}
