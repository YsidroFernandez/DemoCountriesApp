using countriesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace countriesApp.ViewModels
{
    public class CountryDetailViewModel: BaseViewModel
    {

        #region Attributes

        private ObservableCollection<Borders> borders;
        private ObservableCollection<Currency> currencies;
        #endregion

        #region Properties
        public Country CountryDetail
        {
            get;
            set;
        }

        public ObservableCollection<Borders> Border
        {
            get
            {
                return this.borders;
            }
            set
            {
                SetValue(ref this.borders, value);
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get
            {
                return this.currencies;
            }
            set
            {
                SetValue(ref this.currencies, value);
            }
        }
        #endregion

        #region Constructors
        public CountryDetailViewModel(Country country)
        {
            this.CountryDetail = country;
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.CountryDetail.Currencies);
        }

        #endregion

        #region Methods

        private void LoadBorders()
        {
            this.Border = new ObservableCollection<Borders>();

            foreach( var border in this.CountryDetail.Borders)
            {
                var country = MainViewModel.GetInstance().countriesList.Where(l => l.Alpha3Code == border).FirstOrDefault();

                if( country != null)
                {
                    this.Border.Add(new Borders
                    {
                        Code = country.Alpha3Code,
                        Name = country.Name,
                    });
                }

            }

        }
        #endregion
    }
}
