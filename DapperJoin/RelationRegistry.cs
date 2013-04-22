using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperJoin
{
    public class RelationRegistry
    {
        private static List<IRelation> Relations { get; set; }

        public static void Add<TParent, TChild>()
        {
            if (Relations == null) Relations = new List<IRelation>();
            RequireSelfRelation<TParent>();
            RequireSelfRelation<TChild>();

            if (Find<TParent, TChild>() == null)
            {
                Relations.Add(new ParentChildRelation<TParent, TChild>(childRequired: true));
            }
        }

        private static IRelation Find<T1, T2>()
        {
            return Find(typeof(T1), typeof(T2));
        }

        private static void RequireSelfRelation<TModel>()
        {
            Type modeltype = typeof(TModel);
            if (Find(modeltype, modeltype) == null)
            {
                Relations.Add(new SelfRelation<TModel>());
            }

        }

        internal static IRelation Find(Type type1, Type type2)
        {
            return Relations.SingleOrDefault(r => r.Matches(type1, type2));
            }
    }
}
