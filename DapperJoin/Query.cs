using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Assumption: join of same tables
// many to many
// one to one

namespace DapperJoin
{

    internal class GenericJoin<TParent, TChild> : Join
    {

        public GenericJoin()
        {
            ParentType = typeof(TParent);
            ChildType = typeof(TChild);
        }
    }

    internal class Join
    {
        public Type ParentType { get; protected set; }
        public Type ChildType { get; protected set; }

        public bool IsParent(Type ItemType)
        {
            return ParentType == ItemType;
        }
    }

    public class JoinRegistry
    {
        private static List<Join> Joins { get; set; }

        public static void Add<TParent, TChild>()
        {
            if(Joins == null)Joins = new List<Join>();

            Joins.Add(new GenericJoin<TParent, TChild>());
        }

        internal static Join Find(Type T1, Type T2)
        {
            if (Joins == null) Joins = new List<Join>();

            return Joins.Single(x=>(x.ParentType == T1 && x.ChildType == T2) || (x.ParentType == T2 && x.ChildType == T1));
        }
    }

    internal class SubQuery
    {
        public SubQuery(Type itemType, Join join, SubQuery joinedTo)
        {
            ItemType = itemType;
            Join = join;
            TableName = itemType.Name;
            TableAlias = itemType.Name.ToLower();
            JoinedTo = joinedTo;
        }

        public Type ItemType { get; set; }
        public string TableName { get; set; }
        public string TableAlias { get; set; }
        public Join Join { get; set; }
        public SubQuery JoinedTo { get; set; }

        public SubQuery GetParentQuery()
        {
            if (Join.ParentType == ItemType)
                return this;
            else
                return JoinedTo;
        }

        public SubQuery GetChildQuery()
        {
            if (Join.ChildType == ItemType)
                return this;
            else
                return JoinedTo;
        }

    }

    public class GenericQuery<TModel>
    {

        private List<SubQuery> SubQueries { get; set; }

        public GenericQuery()
        {
            SubQueries = new List<SubQuery>();
            SubQueries.Add(new SubQuery(
                typeof(TModel), null, null
                ));
        }

        public GenericQuery<TModel> Join<TFrom, TTo>()
        {
            SubQuery joinTo = SubQueries.Last(x => x.ItemType == typeof(TFrom));
            Join join = JoinRegistry.Find(joinTo.ItemType, typeof(TTo));

            SubQueries.Add(new SubQuery(typeof(TTo), join, joinTo));

            return this;
        }

        public string ToSql()
        {
            StringBuilder selectBuilder = new StringBuilder();
            string selectFormat = "\n{0}.*,";
            foreach (SubQuery subQuery in SubQueries)
            {
                selectBuilder.Append(string.Format(selectFormat, subQuery.TableAlias));
            }
            selectBuilder.Remove(selectBuilder.Length - 1, 1);

            StringBuilder fromBuilder = new StringBuilder();
            string joinFormat = "\nINNER JOIN {0} {1} ON {2}.{3} = {4}.{5}";
            string fromFormat = "\nFROM {0} {1}";

            foreach (SubQuery subQuery in SubQueries)
            {
                if (subQuery.Join == null)
                {
                    fromBuilder.Append(
                        string.Format(fromFormat, subQuery.TableName, subQuery.TableAlias)
                        );
                }
                else
                {
                    fromBuilder.Append(
                        string.Format(joinFormat,
                            subQuery.TableName
                            , subQuery.TableAlias
                            , subQuery.GetParentQuery().TableAlias
                            , "Id"
                            , subQuery.GetChildQuery().TableAlias
                            , subQuery.GetParentQuery().TableName + "Id"
                        )
                   );
                }
            } 

            return string.Format("SELECT {0} {1}", selectBuilder, fromBuilder);
        }
    }
}
