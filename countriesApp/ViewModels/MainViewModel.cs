﻿
using countriesApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace countriesApp.ViewModels
{
    public class MainViewModel
    {
        #region Properties

        public List <Country> countriesList {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login {
            get;
            set;
        }
        public CountryViewModel Country
        {
            get;
            set;
        }

        public CountryDetailViewModel CountryDetail { 
            get; 
            set;
        }

        public CountryItemViewModel CountryItem
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
