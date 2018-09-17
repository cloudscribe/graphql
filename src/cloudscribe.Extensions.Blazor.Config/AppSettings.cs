using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Config
{
    public static class AppSettings
    {
        public static Task<string> Get(string dataSetKey, string configElementId = "appSettings")
        {
            return JSRuntime.Current.InvokeAsync<string>("appSettingsJsFunctions.getConfigSetting", dataSetKey, configElementId);
        }
    }
}
