using MoneyManger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MoneyManger.ViewModels
{
    public class CopyPasteTransactionPageViewModel : BaseViewModel
    {
        public CopyPasteTransactionPageViewModel()
        {
            Entities = new ObservableRangeCollection<Entity>();
            
            CopyCommand = new AsyncCommand(() => CopyAsync());
        }

        async Task ExecuteLoadEntitiesCommand()
        {
            try
            {
                IsBusy = true;
                var peoples = await EntityDataStore.GetAllEntityAsync();
                Entities.Clear();
                Entities.AddRange(peoples);
            }
            catch (ApplicationException aex)
            {
                aex.ShowApplicationExceptionErrorDialogAndHideLoading();
            }
            catch (Exception ex)
            {
                ex.ShowExceptionErrorDialogAndHideLoading();
            }
            finally { IsBusy = false; }
        }

        async Task CopyAsync()
        {
            try
            {
                
            }
            catch (ApplicationException aex)
            {
                aex.ShowApplicationExceptionErrorDialogAndHideLoading();
            }
            catch (Exception ex)
            {
                ex.ShowExceptionErrorDialogAndHideLoading();
            }
        }

        #region Commands and Bindings

        public ObservableRangeCollection<Entity> Entities { get; }
        public AsyncCommand CopyCommand { get; }

        #endregion
    }
}
