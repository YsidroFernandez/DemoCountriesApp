using countriesApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace countriesApp.ViewModels
{
    public class CountryDetailViewModel
    {
        #region Properties
        public Country CountryDetail
        {
            get;
            set;
        }
        #endregion
         
        #region Constructors
        public CountryDetailViewModel(Country country)
        {
            this.CountryDetail = country;
        } 
        #endregion
    }
}
