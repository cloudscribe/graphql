window.appSettingsJsFunctions = {
    
    getConfigSetting: function (dataSetKey, configElementId) {
        var appElement = document.getElementById(configElementId);
        return appElement.dataset[dataSetKey];
    }
    
};
