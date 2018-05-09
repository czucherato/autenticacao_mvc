using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Autenticacao.ViewModel
{
    public class LogarViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        //public string UrlRetorno { get; set; }

        public bool Lembrar { get; set; }
    }
}