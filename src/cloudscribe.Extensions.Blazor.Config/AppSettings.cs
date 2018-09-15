using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Config
{
    public static class AppSettings
    {
        public static Task<string> Get(string dataSetKey)
        {
            return JSRuntime.Current.InvokeAsync<string>(
                "appSettingsJsFunctions.getConfigSetting",
                dataSetKey);
        }
    }
}
