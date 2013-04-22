using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Assumption: join of same tables
// many to many
// one to one
// tabelnaam centraal instelbaar (dus niet op relation)


namespace DapperJoin
{

    internal class SubQuery
    {
        public SubQuery(Type itemType, IRelation relation, SubQuery joinedTo, string tableName, string tableAlias)
        {
            Table = new DapperJoin.Table(itemType, tableName);
            Relation = relation;

            if (tableAlias == null)
            {
                TableAlias = TableAliasRegistry.Generate(itemType.Name);
            }
            else
            {
                TableAliasRegistry.Register(tableAlias);
                TableAlias = tableAlias;

            }
            JoinedTo = joinedTo;
        }

        public Table Table { get; set; }
        public string TableAlias { get; set; }
        public IRelation Relation { get; set; }
        public SubQuery JoinedTo { get; set; }

        internal SubQuery GetParentQuery()
        {
            return Relation.GetParentQuery(this);
        }

        internal SubQuery GetChildQuery()
        {
            return Relation.GetChildquery(this);
        }
    }
}
