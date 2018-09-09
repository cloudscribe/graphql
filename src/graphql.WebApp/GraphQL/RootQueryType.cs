using cloudscribe.Core.Models.GraphQL;
using cloudscribe.Core.Services.GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphql.WebApp.GraphQL
{
    public class RootQueryType : ObjectGraphType<object>
    {
        // there can be only one QueryType and one ISchema, so that has to be in the main web app to aggregate
        // types and queries from multiple sources into a single graphql endpoint
        // that is why RootQueryType and RootSchema belong to the app, everything else can come from libraries

        public RootQueryType(
            SiteService siteService
            )
        {
            Name = "Query";

            Field<ListGraphType<SiteInfoType>>(
                "sites",
                resolve: context => siteService.GetAllSites(context.CancellationToken)
            );

        }

    }
}
