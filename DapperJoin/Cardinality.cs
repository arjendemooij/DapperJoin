//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DapperJoin
//{
//    public class Cardinality
//    {
//        private Cardinality(CardinalityPart from, CardinalityPart to)
//        {
//            From = from;
//            To = to;
//        }

//        public static readonly Cardinality C1_N = new Cardinality(CardinalityPart.C1, CardinalityPart.CN);
//        public static readonly Cardinality C1_0N = new Cardinality(CardinalityPart.C1, CardinalityPart.C0N);
//        public static readonly Cardinality C1_1 = new Cardinality(CardinalityPart.C1, CardinalityPart.C1);
//        public static readonly Cardinality C1_01 = new Cardinality(CardinalityPart.C1, CardinalityPart.C01);
//        public static readonly Cardinality CN_N = new Cardinality(CardinalityPart.CN, CardinalityPart.CN);

//        public CardinalityPart From { get; set; }
//        public CardinalityPart To { get; set; }
//    }

//    public class CardinalityPart
//    {
//        public static readonly CardinalityPart C01 = new CardinalityPart(false);
//        public static readonly CardinalityPart C1 = new CardinalityPart(true);
//        public static readonly CardinalityPart C0N = new CardinalityPart(false);
//        public static readonly CardinalityPart CN = new CardinalityPart(true);

//        private CardinalityPart(bool required)
//        {
//            Required = required;
//        }

//        public bool Required { get; set; }
//    }
//}
