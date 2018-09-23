using cloudscribe.Core.GraphQL.GraphTypes;
using cloudscribe.Core.GraphQL.Services;
using cloudscribe.Core.Models;
using cloudscribe.Extensions.GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace cloudscribe.Core.GraphQL
{
    public class CoreSubscription : ObjectGraphType<object> , IGraphSubscriptionMarker
    {
        public CoreSubscription(
            SiteService siteService)
        {
            _siteService = siteService;

            this.Name = "CoreSubscription";
            this.Description = "Site update events can be pushed to the client in real time over web sockets.";

            this.AddField(
                new EventStreamFieldType()
                {
                    Name = "siteUpdated",
                    Arguments = new QueryArguments(
                        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" }
                        ),
                    Type = typeof(SiteType),
                    Resolver = new FuncFieldResolver<ISiteContext>(ResolveSite),
                    Subscriber = new EventStreamResolver<SiteSettings>(SubscribeById),
                });

            

        }

        private readonly SiteService _siteService;

        private IObservable<SiteSettings> SubscribeById(ResolveEventStreamContext context)
        {
            
            var id = context.GetArgument<Guid>("id");
            return _siteService
                            .WhenSiteUpdated
                            .Where(x => x.Id == id);
        }

        private ISiteContext ResolveSite(ResolveFieldContext context)
        {
            var site = context.Source as ISiteContext;

            return site;
        }

        //private async Task<IObservable<ISiteContext>> SubscribeByIdAsync(ResolveEventStreamContext context)
        //{
        //    var id = context.GetArgument<Guid>("id");

        //    var site = await _siteService.GetSite(id, context.CancellationToken);
        //    return site;
        //}


    }
}
