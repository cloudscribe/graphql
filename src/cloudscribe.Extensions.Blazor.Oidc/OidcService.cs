using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class OidcService
    {
        public OidcService()
        {

        }
        
        #region Events

        public event Action OnChange;
        public event Action OnUserLoaded;
        public event Action OnUserUnloaded;
        public event Action OnAccessTokenExpiring;
        public event Action OnAccessTokenExpired;
        public event Action OnUserSignedOut;
        public event Action<string> OnSilentRenewError;

        private void NotifyStateChanged() => OnChange?.Invoke();
        private void NotifyUserLoaded() => OnUserLoaded?.Invoke();
        private void NotifyUserUnloaded() => OnUserUnloaded?.Invoke();
        private void NotifyAccessTokenExpiring() => OnAccessTokenExpiring?.Invoke();
        private void NotifyAccessTokenExpired() => OnAccessTokenExpired?.Invoke();
        private void NotifyUserSignedOut() => OnUserSignedOut?.Invoke();
        private void NotifySilentRenewError(string error) => OnSilentRenewError?.Invoke(error);


        [JSInvokable]
        public async Task JsUserLoadedCallback(User user)
        {
            _userRoles = await JSRuntime.Current.InvokeAsync<List<string>>("oidcJsFunctions.getRolesFromToken", user.Access_Token);              
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", user);
            NotifyUserLoaded();
        }

        [JSInvokable]
        public async Task JsUserUnLoadedCallback()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", "user unloaded");
            NotifyUserUnloaded();
        }

        [JSInvokable]
        public async Task JsAccessTokenExpiringCallback()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", "access token expiring");
            NotifyAccessTokenExpiring();
        }

        [JSInvokable]
        public async Task JsAccessTokenExpiredCallback()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", "access token expired");
            NotifyAccessTokenExpired();
        }

        [JSInvokable]
        public async Task JsUserSignedOut()
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", "user signed out");
            NotifyUserSignedOut();
        }

        [JSInvokable]
        public async Task JsSilentRenewError(string error)
        {
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", $"silent renew error {error}");
            NotifySilentRenewError(error);
        }

        #endregion

        private User _currentUser = null;

        public User CurrentUser
        {
            get { return _currentUser; }
        }

        private List<string> _userRoles = new List<string>();

        /// <summary>
        /// Security should only really be enforced on the server.
        /// This method is for convenience to show/hide things in the UI
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return _userRoles.Contains(role);
        }

        private async Task EnsureUserManager()
        {
            var settings = await Settings.GetSettings();
            
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.ensureUserManager", settings, new DotNetObjectRef(this));
        }

        private async Task EnsureUser()
        {
            if(_currentUser == null)
            {
                _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.getUser");
                //if (_currentUser != null)
                //{
                    //await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", _currentUser);
                    //var expires = _currentUser.GetExpirationTime();
                    //await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", expires.ToString("s"));
               // }

            }
            
        }

        public async Task Init()
        {
            await EnsureUserManager();
            await EnsureUser();
            
        }

        //when the token has expired and been renewed we need to reload the user from js
        public async Task ReloadUser()
        {
            _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.getUser");
        }

        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SigninRedirect()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signinRedirect");
        }

        /// <summary>
        /// Returns promise to process response from the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SigninRedirectCallback()
        {
            await EnsureUserManager();
            _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinRedirectCallback");
            NotifyStateChanged();
            return _currentUser;
        }

        /// <summary>
        /// Returns promise to trigger a silent request (via an iframe) to the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SigninSilent()
        {
            await EnsureUserManager();
            _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinSilent");
            NotifyStateChanged();

            return _currentUser;
        }

        /// <summary>
        /// Returns promise to notify the parent window of response from the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SigninSilentCallback()
        {
            await EnsureUserManager();
            _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinSilentCallback");
            NotifyStateChanged();
        }

        /// <summary>
        /// Returns promise to trigger a request (via a popup window) to the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SigninPopup()
        {
            await EnsureUserManager();
            _currentUser =  await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinPopup");
            NotifyStateChanged();
            return _currentUser;
        }

        

        /// <summary>
        /// Returns promise to notify the opening window of response from the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SigninPopupCallback()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signinPopupCallback");
            NotifyStateChanged();
        }

       
        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the end session endpoint
        /// </summary>
        /// <returns></returns>
        public async Task SignoutRedirect()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signoutRedirect");
            NotifyStateChanged();
        }

        /// <summary>
        /// Returns promise to process response from the end session endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SignoutRedirectCallback()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signoutRedirectCallback");
            NotifyStateChanged();
        }


        /// <summary>
        /// Returns promise to remove from any storage the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task RemoveUser()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.removeUser");
            NotifyStateChanged();
        }

        /// <summary>
        /// Removes stale state entries in storage for incomplete authorize requests.
        /// </summary>
        /// <returns></returns>
        public async Task ClearStaleState()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.clearStaleState");
            NotifyStateChanged();
        }

        /// <summary>
        /// Returns promise to query OP for user's current signin status. Returns object with session_state and subject identifier.
        /// </summary>
        /// <returns></returns>
        public async Task<object> QuerySessionStatus()
        {
            await EnsureUserManager();
            return await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.querySessionStatus");
            
        }

        //public async Task<List<string>> GetUserRoles()
        //{
        //    await Init();
        //    if(_currentUser != null)
        //    {
        //        return await JSRuntime.Current.InvokeAsync<List<string>>("oidcJsFunctions.getRolesFromToken", _currentUser.Access_Token);
        //    }
        //    return new List<string>();
        //}

    }
}
