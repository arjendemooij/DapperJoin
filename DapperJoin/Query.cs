using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperJoin
{

    public class Query<TModel>
    {

        private List<SubQuery> SubQueries { get; set; }

        public Query()
        {
            SubQueries = new List<SubQuery>();
            SubQueries.Add(new SubQuery(
                typeof(TModel), null, null, null, null));
        }

        public Query<TModel> Join<TFrom, TTo>()
        {
            SubQuery joinTo = SubQueries.Last(x => x.Table.ItemType == typeof(TFrom));
            IRelation join = RelationRegistry.Find(joinTo.Table.ItemType, typeof(TTo));

            SubQueries.Add(new SubQuery(typeof(TTo), join, joinTo, null, null));

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
            string joinFormat = "\n{0} {1} {2} ON {3}.{4} = {5}.{6}";
            string fromFormat = "\nFROM {0} {1}";

            foreach (SubQuery subQuery in SubQueries)
            {
                if (subQuery.Relation == null)
                {
                    fromBuilder.Append(
                        string.Format(fromFormat, subQuery.Table.TableName, subQuery.TableAlias)
                        );
                }
                else
                {
                    fromBuilder.Append(
                        string.Format(
                            joinFormat
                            , subQuery.Relation.ChildRequired ? "INNER JOIN" : "LEFT OUTER JOIN" 
                            , subQuery.Table.TableName
                            , subQuery.TableAlias
                            , subQuery.GetParentQuery().TableAlias
                            , subQuery.Relation.ParentFieldName
                            , subQuery.GetChildQuery().TableAlias
                            , subQuery.Relation.ChildFieldName
                        )
                   );
                }
            }

            return string.Format("SELECT {0} {1}", selectBuilder, fromBuilder);
        }
    }
}
