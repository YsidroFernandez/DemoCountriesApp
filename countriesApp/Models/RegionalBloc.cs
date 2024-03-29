﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace countriesApp.Models
{
    public class RegionalBloc
    {

        #region Attributes

        [JsonProperty(PropertyName = "acronym")]
        public string Acronym { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        #endregion
    }
}
