﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace USNStarterKit.USNModels
{

    /// <summary>
    /// Summary description for LoginFormViewModel
    /// </summary>
    public class USNLoginFormViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}