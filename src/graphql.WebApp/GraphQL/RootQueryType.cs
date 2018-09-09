using cloudscribe.Core.Models.GraphQL;
using cloudscribe.Core.Services;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphql.WebApp.GraphQL
{
    public class RootQueryType : ObjectGraphType<object>
    {

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
