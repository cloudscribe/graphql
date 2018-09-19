using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class OidcService
    {
        public OidcService()
        {

        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        private UserManager _userManager = null;
        private User _currentUser = null;

        public User CurrentUser
        {
            get { return _currentUser; }
        }

        private async Task EnsureUserManager()
        {
            if(_userManager == null)
            {
                _userManager = await UserManager.GetUserManager();
            }
        }

        public async Task Init()
        {
            await EnsureUserManager();
            _currentUser = await _userManager.GetUser();

        }

        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SignInRedirect()
        {
            await EnsureUserManager();

            await _userManager.SignInRedirect();
        }

        /// <summary>
        /// Returns promise to process response from the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SignInRedirectCallback()
        {
            await EnsureUserManager();

            _currentUser = await _userManager.SignInRedirectCallback();
            NotifyStateChanged();
            return _currentUser;
        }

        /// <summary>
        /// Returns promise to trigger a request (via a popup window) to the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SignInPopup()
        {
            await EnsureUserManager();

            _currentUser = await _userManager.SignInPopup();

            return _currentUser;
        }

        public async Task<User> SignInSilent()
        {
            await EnsureUserManager();

            _currentUser = await _userManager.SignInSilent();

            return _currentUser;
        }

        public async Task SignInSilentCallback()
        {
            await EnsureUserManager();

            await _userManager.SignInSilentCallback();
            //NotifyStateChanged();
        }

        /// <summary>
        /// Returns promise to notify the opening window of response from the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SignInPopupCallback()
        {
            await EnsureUserManager();

            await _userManager.SignInPopupCallback();
            NotifyStateChanged();
        }

        /// <summary>
        /// Returns promise to load the User object for the currently authenticated user.
        /// </summary>
        /// <returns>If there is no authenticated user, return null. Else, return the current authenticated user</returns>
        //public async Task<User> GetUser()
        //{
        //    await EnsureUserManager();

        //    return await _userManager.GetUser();
        //}

        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the end session endpoint
        /// </summary>
        /// <returns></returns>
        public async Task SignOut()
        {
            await EnsureUserManager();
            //await _userManager.RemoveUser();
            await _userManager.SignOut();
            NotifyStateChanged();
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
            await EnsureUserManager();

            await _userManager.RemoveUser();
            NotifyStateChanged();
        }

    }
}
