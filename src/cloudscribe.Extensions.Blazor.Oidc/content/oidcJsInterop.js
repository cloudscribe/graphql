

// https://github.com/IdentityModel/oidc-client-js/wiki

window.oidcJsFunctions = {
  showPrompt: function (message) {
    return prompt(message, 'Type anything here');
    },

    ensureUserManager: function (config) {
        if (window.UserManager === undefined) {
            window.UserManager = new Oidc.UserManager(config);
        }
        return window.UserManager;
    }



};
