using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Delivery.ViewModels
{
    public abstract class BaseViewModel : BindableBase, IDialogAware
    {
        public virtual string Title { get; }
        protected virtual IDialogService DialogService { get; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        protected virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}
