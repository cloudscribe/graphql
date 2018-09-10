using GraphQL.Types;
using System.Collections.Generic;

namespace cloudscribe.Extensions.GraphQL
{
    public class CompositeQuery : ObjectGraphType<object>
    {
        public CompositeQuery(IEnumerable<IGraphQueryMarker> graphQueryMarkers)
        {
            Name = "CompositeQuery";
            foreach(var marker in graphQueryMarkers)
            {
                var q = marker as ObjectGraphType<object>;
                foreach(var f in q.Fields)
                {
                    AddField(f);
                }
            }
        }
    }
}
