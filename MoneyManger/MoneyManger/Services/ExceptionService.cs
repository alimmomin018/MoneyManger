using Acr.UserDialogs;
using System;

namespace MoneyManger
{
    public static class ExceptionService
    {
        public static async void ShowExceptionErrorDialog(this Exception ex)
        {
            await UserDialogs.Instance.ConfirmAsync(ex.Message, Constants.ERROR_LABEL, Constants.OK_LABEL, Constants.CANCEL_LABEL);
        }

        public static async void ShowExceptionErrorDialogAndHideLoading(this Exception ex)
        {
            await UserDialogs.Instance.ConfirmAsync(ex.Message, Constants.ERROR_LABEL, Constants.OK_LABEL, Constants.CANCEL_LABEL);
            UserDialogs.Instance.HideLoading();
        }

        public static async void ShowApplicationExceptionErrorDialog(this ApplicationException aex)
        {
            await UserDialogs.Instance.ConfirmAsync(aex.Message, Constants.ERROR_LABEL, Constants.OK_LABEL, Constants.CANCEL_LABEL);
        }        
        
        public static async void ShowApplicationExceptionErrorDialogAndHideLoading(this ApplicationException aex)
        {
            await UserDialogs.Instance.ConfirmAsync(aex.Message, Constants.ERROR_LABEL, Constants.OK_LABEL, Constants.CANCEL_LABEL);
            UserDialogs.Instance.HideLoading();
        }
    }
}
