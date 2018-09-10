using GraphQL;
using GraphQL.Types;
using System.Collections.Generic;
using System.Linq;

namespace cloudscribe.Extensions.GraphQL
{
    public class CompositeSchema : Schema
    {
        public CompositeSchema(IDependencyResolver resolver, IEnumerable<IGraphMutationMarker> graphMutationMarkers) : base(resolver)
        {
            Query = resolver.Resolve<CompositeQuery>();
            var mutList = graphMutationMarkers.ToList();
            if(mutList.Count > 0)
            {
                Mutation = resolver.Resolve<CompositeMutation>();
            }
            
        }
    }
}
