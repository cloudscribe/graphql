# Experiments with cloudscribe and GraphQL.NET and Blazor

The primary purpose of this repository is as a learning excercise for me to learn GraphQL as an alternative to REST style APIs, and to learn Blazor, as an alternative to javascript for single page application (SPA) style development. cloudscribe is a set of libraries providing a content engine and blog as well as multi-tenant management for sites, users, roles, and more, but it is primarily implemented as an MVC style application. There has been some interest in the past for an API version of cloudscribe, and this repository is place to experiment to see what would that look like if we implemented cloudscribe features as a GraphQL API endpoint, and consumed the APIs from a Blazor client.

The sourceDev.WebApp project in this solution is a cloudscribe MVC app, that also hosts the GraphQL endpoint and provides authentication to the sourceDev.BlazorApp using IdentityServer4. So the sourceDev.BlazorApp is the main SPA application and it authenticates against the sourceDev.WebApp project and consumes a GraphQL API from there as well. The GraphQL end point could be a separate standalone API project but for convenience and easier integration testing I've just included it in the main MVC app.

The cloudscribe.Extensions.Blazor.oidc project is a Blazor library that does js interop around the oidc-client.js to make it easy to authenticated against openid connect ie using IdentityServer4 in this case.

The solution uses NoDb file system storage, so it is pre-populated with data and anyone who downloads this repo and tries it out will have the same existing data.

## Try It Out

1. Just run the sourceDev.WebApp and sourceDev.BlazorApp in the browser, ie right click each project and choose view in browser.
2. Click the Sign In button in the Blazor app and it will oopen a popup login window from the MVC app. The login page shows the admin credentials to login with.
3. The Site List page in the Blazor app is the first page I got working against the GraphQL endpoint, the site list is protected by an authorization policy so it only works if logged in as admin.

## Next Steps

I plan to flesh out a few more cloudscribe featues in GraphQL and Blazor including the blog. Longer term it could become a real alternative to the cloudscribe MVC components but for now Blazor is experimental and so is the code in this solution.

## Problems to solve

* How to do things like pagination, date pickers, client side validation in Blazor? 
* What about wysiwyg editors for rich content etc in Blazor. 
* What about localization in Blazor?


[![Join the chat at https://gitter.im/joeaudette/cloudscribe](https://badges.gitter.im/joeaudette/cloudscribe.svg)](https://gitter.im/joeaudette/cloudscribe?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

