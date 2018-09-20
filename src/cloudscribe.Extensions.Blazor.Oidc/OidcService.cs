using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class OidcService
    {
        public OidcService()
        {

        }

        //[JSInvokable]
        //public string SayHello() {
        //   return  "Hello,Joe!";
        //}

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        private User _currentUser = null;

        public User CurrentUser
        {
            get { return _currentUser; }
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
                //if(_currentUser != null)
                //{
                //    await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", _currentUser);
                //    var expires = _currentUser.GetExpirationTime();
                //    await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.logToConsole", expires.ToString("s"));
                //}
                
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
        public async Task SignInRedirect()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signinRedirect");
        }

        /// <summary>
        /// Returns promise to process response from the authorization endpoint. The result of the promise is the authenticated User.
        /// </summary>
        /// <returns></returns>
        public async Task<User> SignInRedirectCallback()
        {
            await EnsureUserManager();
            _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinRedirectCallback");
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
            _currentUser =  await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signinPopup");
            NotifyStateChanged();
            return _currentUser;
        }

        public async Task<User> SignInSilent()
        {
            await EnsureUserManager();
            _currentUser = await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signInSilent");
            NotifyStateChanged();

            return _currentUser;
        }

        public async Task SignInSilentCallback()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<User>("oidcJsFunctions.signInSilentCallback");
            NotifyStateChanged();
        }

        /// <summary>
        /// Returns promise to notify the opening window of response from the authorization endpoint.
        /// </summary>
        /// <returns></returns>
        public async Task SignInPopupCallback()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signinPopupCallback");
            NotifyStateChanged();
        }

       
        /// <summary>
        /// Returns promise to trigger a redirect of the current window to the end session endpoint
        /// </summary>
        /// <returns></returns>
        public async Task SignOut()
        {
            await EnsureUserManager();
            await JSRuntime.Current.InvokeAsync<object>("oidcJsFunctions.signoutRedirect");
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

    }
}
