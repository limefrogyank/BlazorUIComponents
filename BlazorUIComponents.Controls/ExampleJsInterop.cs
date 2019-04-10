using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorUIComponents.Controls
{
    public class ExampleJsInterop
    {
        public static Task<string> Prompt(IJSRuntime jsRuntime, string message)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }
    }
}
