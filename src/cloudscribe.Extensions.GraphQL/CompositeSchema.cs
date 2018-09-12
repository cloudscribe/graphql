using GraphQL;
using GraphQL.Types;
using System.Collections.Generic;
using System.Linq;

namespace cloudscribe.Extensions.GraphQL
{
    public class CompositeSchema : Schema
    {
        public CompositeSchema(
            IDependencyResolver resolver, 
            IEnumerable<IGraphMutationMarker> graphMutationMarkers,
            IEnumerable<IGraphSubscriptionMarker> graphSubscriptionMarkers
            ) : base(resolver)
        {
            Query = resolver.Resolve<CompositeQuery>();
            if(graphMutationMarkers.Any())
            {
                Mutation = resolver.Resolve<CompositeMutation>();
            }
            //var mutList = graphMutationMarkers.ToList();
            //if(mutList.Count > 0)
            //{
            //    Mutation = resolver.Resolve<CompositeMutation>();
            //}
            if(graphMutationMarkers.Any())
            {
                Subscription = resolver.Resolve<CompositeSubscription>();
            }
            
        }
    }
}
