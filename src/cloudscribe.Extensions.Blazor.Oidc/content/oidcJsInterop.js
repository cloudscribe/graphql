window.oidcJsFunctions = {

    // https://github.com/IdentityModel/oidc-client-js/wiki
    
    ensureUserManager: function (config, dotnetHelper) {
        if (window.UserManager === undefined) {
            console.log(config);

            //Oidc.Log.logger = console;
            //Oidc.Log.level = Oidc.Log.INFO; // Oidc.Log.NONE Oidc.Log.ERROR Oidc.Log.WARN Oidc.Log.INFO

            window.UserManager = new Oidc.UserManager(config);

            

            window.UserManager.events.addUserLoaded(function (user) {
                console.log("userLoaded");
                console.log(user);
               
            });

            window.UserManager.events.addUserUnloaded(function () {
                console.log("userUnloaded");
                

            });

            window.UserManager.events.addAccessTokenExpired(function () {
                console.log("accessTokenExpired");

            });

            window.UserManager.events.addAccessTokenExpiring(function () {
                console.log("token expiring...");
                //window.UserManager.startSilentRenew();
            });

            window.UserManager.events.addSilentRenewError(function () {
                console.log("silentRenewError");

            });
            window.UserManager.events.addUserSignedOut(function () {
                console.log("silentRenewError");

            });

            //dotnetHelper.invokeMethodAsync('SayHello')
            //    .then(r => console.log(r));


        }
        //return window.UserManager;
        return null;
    },

    logToConsole: function (someDotNetObject) {
        console.log(someDotNetObject);
    },

    signinRedirect: function () {
        return window.UserManager.signinRedirect();
    },

    signinRedirectCallback: function () {
        return window.UserManager.signinRedirectCallback();
    },

    signinPopup: function () {
        return window.UserManager.signinPopup();
    },

    signinPopupCallback: function () {
        return window.UserManager.signinPopupCallback();
    },

    signinSilent: function () {
        console.log("signinSilent");
        return window.UserManager.signinSilent();
    },

    signinSilentCallback: function () {
        console.log("signinSilentCallback");
        return window.UserManager.signinSilentCallback();
    },

    signoutRedirect: function () {
        return window.UserManager.signoutRedirect();
    },

    startSilentRenew : function () {
        return window.UserManager.startSilentRenew ();
    },

    //getUserName: function () {
    //    var user = getUser().then();
    //    //console.log(promise);
    //    return promise.profile.name;
    //},

    getUser: function () {
        var promise = window.UserManager.getUser();
        //console.log(promise);
        return promise;
    },

    removeUser: function () {
        return window.UserManager.removeUser();
    }



};
