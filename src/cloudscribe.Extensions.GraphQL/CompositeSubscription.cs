using GraphQL.Types;
using System.Collections.Generic;

namespace cloudscribe.Extensions.GraphQL
{
    public class CompositeSubscription : ObjectGraphType
    {
        public CompositeSubscription(IEnumerable<IGraphSubscriptionMarker> graphSubscriptionMarkers)
        {
            Name = "CompositeSubscription";

            foreach (var marker in graphSubscriptionMarkers)
            {
                var q = marker as ObjectGraphType;
                foreach (var f in q.Fields)
                {
                    AddField(f);
                }
            }
        }
    }
}
