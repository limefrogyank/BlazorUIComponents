using System;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.Service
{
    public interface IDialogService
    {
        Task HideModalAsync(Action hideModalAction);
        Task ShowModalAsync(Action showModalAction);
    }
}