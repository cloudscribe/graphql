window.appSettingsJsFunctions = {
    
    getConfigSetting: function (dataSetKey) {
        var appElement = document.getElementById("appSettings");
        return appElement.dataset[dataSetKey];
    }
    
};
