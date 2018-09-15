window.oidcJsFunctions = {

    // https://github.com/IdentityModel/oidc-client-js/wiki
    
    ensureUserManager: function (config) {
        if (window.UserManager === undefined) {
            window.UserManager = new Oidc.UserManager(config);
        }
        return window.UserManager;
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

    signOut: function () {
        return window.UserManager.signoutRedirect();
    },

    getUser: function () {
        return window.UserManager.getUser();
    },

    removeUser: function () {
        return window.UserManager.removeUser();
    }



};
