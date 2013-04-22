using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperJoin
{
    internal interface IRelation
    {
        bool Matches(Type t1, Type t2);
        
        SubQuery GetChildquery(SubQuery subQuery);
        SubQuery GetParentQuery(SubQuery subQuery);

        string ParentFieldName { get; }
        string ChildFieldName { get; }

        bool ChildRequired { get; }
    }
}
