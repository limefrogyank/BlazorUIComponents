using System;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.Service
{
    public interface IDialogService
    {
        Task<string> ShowSingleInputModalAsync(string title, string description, string inputHeader);
    }
}