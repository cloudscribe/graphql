using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class UserManager
    {
        private UserManager()
        {

        }

        public static async Task<UserManager> GetUserManager()
        {
            var settings = await Settings.GetSettings();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.ensureUserManager", settings);

            return new UserManager();
        }


        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SignInRedirect()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signinRedirect");
        }

        /// <summary>
        /// Returns promise to process response from the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SignInRedirectCallback()
        {
             return await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinRedirectCallback"); 
        }

        /// <summary>
        /// Returns promise to trigger a request (via a popup window) to the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SignInPopup()
        {
            return await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinPopup");
        }

        /// <summary>
        /// Returns promise to notify the opening window of response from the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SignInPopupCallback()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signinPopupCallback"); 
        }

        /// <summary>
        /// Returns promise to load the User object for the currently authenticated user.
        /// </summary>
        /// <returns>If there is no authenticated user, return null. Else, return the current authenticated user</returns>
        public async Task<User> GetUser()
        {
            var user = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.getUser");
            //user.Name = await JSRuntime.Current.InvokeAsync<string>("oidcJsFunctions.getUserName");
            return user;
        }

        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the end session endpoint
        /// </summary>
        /// <returns></returns>
        public async Task SignOut()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signoutRedirect");
        }

        ///// <summary>
        ///// Returns promise to trigger a silent request (via an iframe) to the authorization endpoint. The result of the promise is the authenticated User.
        ///// </summary>
        ///// <returns></returns>
        //public async Task<User> SignInSilent()
        //{
        //    return await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signInSilent");
        //}

        /// <summary>
        /// Returns promise to remove from any storage the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task RemoveUser()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.removeUser");
        }

    }
}
