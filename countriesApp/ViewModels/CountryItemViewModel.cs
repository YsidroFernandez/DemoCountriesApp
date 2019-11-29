using countriesApp.Models;
using countriesApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace countriesApp.ViewModels
{
    public class CountryItemViewModel: Country
    {

        #region Commands
        public ICommand SelectCountryCommand {
            get
            {
                return new RelayCommand(SelectCountry);
            }

        }
        #endregion

        #region Methods

        private async void SelectCountry()
        {
            MainViewModel.GetInstance().CountryDetail = new CountryDetailViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new CountryDetailPage());
        }
        #endregion
    }
}
