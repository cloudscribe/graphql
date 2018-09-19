//using GraphQL;
//using GraphQL.Types;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace cloudscribe.Extensions.GraphQL.Types
//{
//    public class PagedListGraphType<T> : PagedListGraphType
//        where T : IGraphType
//    {
//        public PagedListGraphType()
//            : base(typeof(T))
//        {
//        }
//    }

//    public class PagedListGraphType : GraphType
//    {
//        public PagedListGraphType(IGraphType type)
//        {
//            ResolvedType = type;
//        }

//        protected PagedListGraphType(Type type)
//        {
//            Type = type;
//        }

//        public Type Type { get; private set; }

//        public IGraphType ResolvedType { get; set; }

//        public override string CollectTypes(TypeCollectionContext context)
//        {
//            var innerType = context.ResolveType(Type);
//            ResolvedType = innerType;
//            var name = innerType.CollectTypes(context);
//            context.AddType(name, innerType, context);
//            return "[{0}]".ToFormat(name);
//        }
//    }

//}
