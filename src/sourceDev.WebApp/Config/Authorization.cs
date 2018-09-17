using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Authorization
    {
        public static AuthorizationOptions SetupAuthorizationPolicies(this AuthorizationOptions options)
        {
            //https://docs.asp.net/en/latest/security/authorization/policies.html

            options.AddCloudscribeCoreDefaultPolicies();
            options.AddCloudscribeLoggingDefaultPolicy();

            options.AddCloudscribeCoreSimpleContentIntegrationDefaultPolicies();
            // this is what the above extension adds
            //options.AddPolicy(
            //    "BlogEditPolicy",
            //    authBuilder =>
            //    {
            //        //authBuilder.RequireClaim("blogId");
            //        authBuilder.RequireRole("Administrators");
            //    }
            // );

            //options.AddPolicy(
            //    "PageEditPolicy",
            //    authBuilder =>
            //    {
            //        authBuilder.RequireRole("Administrators");
            //    });

            options.AddPolicy(
                "FileManagerPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
                "FileManagerDeletePolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
                "IdentityServerAdminPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });

            // add other policies here 


            return options;
        }

    }
}
