window.oidcJsFunctions = {

    // https://github.com/IdentityModel/oidc-client-js/wiki
    
    ensureUserManager: function (config) {
        if (window.UserManager === undefined) {
            //console.log(config);

            window.UserManager = new Oidc.UserManager(config);
        }
        //return window.UserManager;
        return null;
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

    //signinSilent: function () {
    //    return window.UserManager.signinSilent();
    //},

    signoutRedirect: function () {
        return window.UserManager.signoutRedirect();
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
