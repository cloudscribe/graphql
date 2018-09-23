using cloudscribe.Core.ApiModels;
using cloudscribe.Core.GraphQL.GraphTypes;
using cloudscribe.Core.GraphQL.Services;
using cloudscribe.Extensions.GraphQL;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using System.Collections.Generic;

namespace cloudscribe.Core.GraphQL
{
    public class CoreMutation : ObjectGraphType, IGraphMutationMarker
    {
        public CoreMutation(
            SiteService siteService
            )
        {
            Name = "CoreMutation";

            //FieldAsync<SiteType>(
            //    "updateSiteName",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" },
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "newName", Description = "The new name for the site" }
            //    ),
            //    resolve: async context =>
            //    {
            //        var id = context.GetArgument<Guid>("id");
            //        var newName = context.GetArgument<string>("newName");
            //        return await context.TryAsyncResolve(
            //            async c => await siteService.UpdateSiteName(id, newName)
            //        );

            //    }

            //).AuthorizeWith("AdminPolicy");

            //FieldAsync<SiteType>(
            //    "updateSite",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" },
            //        new QueryArgument<NonNullGraphType<SiteUpdateInputType>> { Name = "updateSite", Description = "an object to patch site properties" }
            //    ),
            //    resolve: async context =>
            //    {
            //        var id = context.GetArgument<Guid>("id");
            //        var patch = context.GetArgument<SiteUpdateModel>("updateSite");


            //        return await context.TryAsyncResolve(
            //            async c => await siteService.UpdateSite(id, patch)
            //        );

            //    }

            //).AuthorizeWith("AdminPolicy");

            FieldAsync<SiteType>(
                "updateSiteCompanyInfo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" },
                    new QueryArgument<NonNullGraphType<CompanyInfoUpdateType>> { Name = "updateSiteCompanyInfo", Description = "an object to patch site properties" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var patch = context.GetArgument<Dictionary<string, object>>("updateSiteCompanyInfo");
                    
                    return await context.TryAsyncResolve(
                        async c => await siteService.UpdateSite(id, patch)
                    );

                }

            )
            .AuthorizeWith("AdminPolicy")
            ;

        }
    }
}
