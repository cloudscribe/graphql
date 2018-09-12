using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

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
