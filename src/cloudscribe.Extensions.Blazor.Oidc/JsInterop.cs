using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class JsInterop
    {
        public static Task<string> Prompt(string message)
        {
            // Implemented in exampleJsInterop.js
            return JSRuntime.Current.InvokeAsync<string>(
                "oidcJsFunctions.showPrompt",
                message);
        }
    }
}
