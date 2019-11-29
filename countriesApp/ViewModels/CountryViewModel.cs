using countriesApp.Models;
using countriesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace countriesApp.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;

        #endregion


        #region Attributes

        private ObservableCollection<Country> country;

        #endregion

        #region Properties

        public ObservableCollection<Country> Country {
            get
            {
                return this.country;
            }
            set
            {
                SetValue(ref this.country, value);
            }
        }
        #endregion

        #region Methods

        private async void LoadCountries()
        {

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Country>(
                "https://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Accept");
                return;

                var list = (List<Country>)response.Result;
                this.Country = new ObservableCollection<Country>(list);
            }
        }
        #endregion

        #region Constructors

        public CountryViewModel()
        {
            this.apiService = new ApiService();
            this.LoadCountries();
        }
        #endregion
    }
}
