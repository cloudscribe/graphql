# Experiments with cloudscribe and GraphQL.NET and Blazor

The primary purpose of this repository is as a learning excercise for me to learn GraphQL as an alternative to REST style APIs, and to learn Blazor, as an alternative to javascript for single page application (SPA) style development. cloudscribe is a set of libraries providing a content engine and blog as well as multi-tenant management for sites, users, roles, and more, but it is primarily implemented as an MVC style application. There has been some interest in the past for an API version of cloudscribe, and this repository is place to experiment to see what would that look like if we implemented cloudscribe features as a GraphQL API endpoint, and consumed the APIs from a Blazor client.

The sourceDev.WebApp project in this solution is a cloudscribe MVC app, that also hosts the GraphQL endpoint and provides authentication to the sourceDev.BlazorApp using IdentityServer4. So the sourceDev.BlazorApp is the main SPA application and it authenticates against the sourceDev.WebApp project and consumes a GraphQL API from there as well. The GraphQL end point could be a separate standalone API project but for convenience and easier integration testing I've just included it in the main MVC app.

The cloudscribe.Extensions.Blazor.Oidc project is a Blazor library that does js interop around the oidc-client.js to make it easy to authenticate against openid connect ie using IdentityServer4 in this case. Available as a [NuGet package](https://www.nuget.org/packages/cloudscribe.Extensions.Blazor.Oidc/)

The solution uses NoDb file system storage, so it is pre-populated with data and anyone who downloads this repo and tries it out will have the same existing data.

Learn more about [cloudscribe](https://www.cloudscribe.com/) (a set of .netstandard libraries that can be assembled into ASP.NET Core web applications providing multi-tenant site management and a really user friendly and powerful content engine), [GraphQL for .NET](https://github.com/graphql-dotnet/graphql-dotnet) (an alternative for traditional REST APIs created by Facebook, ported to .NET by @joemcbride et al), and [Blazor ](https://blazor.net/) (.NET in the web browser).

## Try It Out

1. Just run the sourceDev.WebApp and sourceDev.BlazorApp in the browser, ie right click each project and choose view in browser.
2. Click the Sign In button in the Blazor app and it will open a popup login window from the MVC app. The login page shows the admin credentials to login with.
3. Once you login an administration menu will appear. 

I've tried to make the Blazor Client mirror the MVC Site. Though only a few cloudscribe features are implemented so far, they use the same layout and urls as in the MVC app. The home page of the Blazor client gets it content from GraphQL services on top of cloudscribe SimpleContent, so the same home page content is in both the Blazor app and the MVC app. The Blog in the Blazor client gets the post list from a graphql query built on top of cloudscribe SimpleContent. The Site List page under Administrartion gets the list of sites from GraphQL query on top of cloudscribe Core and the Company Info edit page uses both GraphQL queries and mutation on top of cloudscribe Core services. 

## Technologies, Techniques, and Concepts Illustrated in this solution

* Authenticating a Blazor Client with IdentityServer4
* Implementing graphql on top of existing cloudscribe services using GraphQL for .NET
* A plugin system for adding query, mutation, and subscription from separate libraries into a Composite GraphQL Schema, Query, Mutation, and Subscription.
* Authorization of graphql operations and fields using ASP.NET Core Authorization policies
* Consuming graphql from Blazor HttpClient
* Implementing a GraphQL Mutation as a patch, sending only the changed values similar to JsonPatch

## Next Steps

What is done so far is just experimental proof of concept but trying to build a good architecture. It is possible to keep going building more and more cloudscribe features until the Blazor app has everything that the MVC app has, but that would be a lot of work. If you like what you see here and want to see it get completed or want to help please let me know. Blazor is still in very early stages and client side Blazor will likely remain experimental for some time yet. I would like to see some sign of commitment that client side Blazor will become a supported product before investing a lot more time, but I must say that I do really like the programming model and I hope it will become an official product. GraphQL for .NET is still relatively new but also I find the programming model to be quite compelling and I think it is ready for production use.

## Problems to solve in Blazor

* How to do things like pagination, date pickers, client side validation in Blazor
* What about wysiwyg editors for rich content etc in Blazor. 
* What about localization in Blazor
* What about dynamic menu items


## Have questions or want to discuss it?

[![Join the chat at https://gitter.im/joeaudette/cloudscribe](https://badges.gitter.im/joeaudette/cloudscribe.svg)](https://gitter.im/joeaudette/cloudscribe?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

