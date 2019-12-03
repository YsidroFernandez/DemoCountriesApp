using countriesApp.Models;
using countriesApp.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace countriesApp.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;

        #endregion


        #region Attributes
        private bool isRefreshing;
        private string filter;
        private ObservableCollection<CountryItemViewModel> country;

        #endregion

        #region Properties

        public ObservableCollection<CountryItemViewModel> Country {
            get
            {
                return this.country;
            }
            set
            {
                SetValue(ref this.country, value);
            }
        }

        public bool IsRefreshing {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                SetValue(ref this.isRefreshing, value);
            }
        }

        public string Filter {
            get
            {
                return this.filter;
            }
            set
            {
                SetValue(ref this.filter, value);
                //para ejecutar la búsquedas cada vez que tipee una letra
                this.Search();
            }
        }
        #endregion

        #region Methods

        private async void LoadCountries()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
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
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "ERROR",
                    response.Message,
                    "Accept");
                return;
            }

            MainViewModel.GetInstance().countriesList = (List<Country>)response.Result;
            this.Country = new ObservableCollection<CountryItemViewModel>( this.ToItemCountryViewModel() );
            this.IsRefreshing = false;
        }

        private IEnumerable<CountryItemViewModel> ToItemCountryViewModel()
        {
            return MainViewModel.GetInstance().countriesList.Select(l => new CountryItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Country = new ObservableCollection<CountryItemViewModel>(this.ToItemCountryViewModel());
            }
            else
            {
                //Busca por nombre del paóis y los compara a minusculas
                this.Country = new ObservableCollection<CountryItemViewModel>(
                    this.ToItemCountryViewModel().Where(
                        c => c.Name.ToLower().Contains(this.Filter.ToLower()) || c.Capital.ToLower().Contains(this.Filter.ToLower())
                        ));
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

        #region Commands

        public ICommand RefreshCommand 
        {
            get
            {
                return new RelayCommand(this.LoadCountries);
            }  
        }

        public ICommand SearchCommand {
            get
            {
                return new RelayCommand(this.Search);
            }
        }
        #endregion
    }
}
