using GraphQL.Types;
using System.Collections.Generic;

namespace cloudscribe.Extensions.GraphQL
{
    public class CompositeMutation : ObjectGraphType
    {
        public CompositeMutation(IEnumerable<IGraphMutationMarker> graphMutationMarkers)
        {
            Name = "CompositeMutation";

            foreach (var marker in graphMutationMarkers)
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
