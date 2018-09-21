window.oidcJsFunctions = {

    logToConsole: function (someDotNetObject) {
        console.log(someDotNetObject);
    },

    // https://github.com/IdentityModel/oidc-client-js/wiki
    
    ensureUserManager: function (config, dotnetOidcService) {
        if (window.UserManager === undefined) {
            console.log(config);

            //Oidc.Log.logger = console;
            //Oidc.Log.level = Oidc.Log.INFO; // Oidc.Log.NONE Oidc.Log.ERROR Oidc.Log.WARN Oidc.Log.INFO

            window.UserManager = new Oidc.UserManager(config);
            
            window.UserManager.events.addUserLoaded(function (user) {
                //console.log("userLoaded");
                dotnetOidcService.invokeMethodAsync('JsUserLoadedCallback', user);
                //.then(console.log("dotnetinterop invoked"));
                //console.log("dotnetinterop invoked");
            });

            window.UserManager.events.addUserUnloaded(function () {
                //console.log("userUnloaded");
                dotnetOidcService.invokeMethodAsync('JsUserUnLoadedCallback');
                    //.then(r => console.log("dotnetinterop unload invoked"));
            });

            window.UserManager.events.addAccessTokenExpiring(function () {
                //console.log("accessTokenExpiring");
                dotnetOidcService.invokeMethodAsync('JsAccessTokenExpiringCallback');
            });

            window.UserManager.events.addAccessTokenExpired(function () {
                //console.log("accessTokenExpired");
                dotnetOidcService.invokeMethodAsync('JsAccessTokenExpiredCallback');
            });
            
            window.UserManager.events.addSilentRenewError(function (err) {
                //console.log("silentRenewError");
                //console.log(err);
                dotnetOidcService.invokeMethodAsync('JsSilentRenewError', err);
            });

            window.UserManager.events.addUserSignedOut(function () {
                //console.log("userSignedOut");
                dotnetOidcService.invokeMethodAsync('JsUserSignedOut');
            });
            
        }   
    },

    getUser: function () {
        return window.UserManager.getUser();
    },

    removeUser: function () {
        return window.UserManager.removeUser();
    },

    signinRedirect: function () {
        return window.UserManager.signinRedirect();
    },

    signinRedirectCallback: function () {
        return window.UserManager.signinRedirectCallback();
    },

    signinSilent: function () {
        console.log("signinSilent");
        return window.UserManager.signinSilent();
    },

    signinSilentCallback: function () {
        console.log("signinSilentCallback");
        return window.UserManager.signinSilentCallback();
    },

    signinPopup: function () {
        return window.UserManager.signinPopup();
    },

    signinPopupCallback: function () {
        return window.UserManager.signinPopupCallback();
    },
    
    signoutRedirect: function () {
        return window.UserManager.signoutRedirect();
    },

    signoutRedirectCallback: function () {
        return window.UserManager.signoutRedirectCallback();
    },

    startSilentRenew : function () {
        return window.UserManager.startSilentRenew ();
    },

    stopSilentRenew: function () {
        return window.UserManager.stopSilentRenew();
    },

    clearStaleState: function () {
        return window.UserManager.clearStaleState();
    },

    querySessionStatus: function () {
        return window.UserManager.querySessionStatus();
    }
    
};
