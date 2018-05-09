using Autenticacao.Controllers.Comum;
using Autenticacao.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autenticacao.Controllers
{
    public class HomeController : BaseController
    {
        [CustomAuthentication]
        public ActionResult Index()
        {
            string nome = User.Nome;
            return View();
        }
    }
}