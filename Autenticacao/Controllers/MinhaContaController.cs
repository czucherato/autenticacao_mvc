using Autenticacao.Controllers.Comum;
using Autenticacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Autenticacao.Controllers
{
    public class MinhaContaController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [ActionName("Logar")]
        public ActionResult LogarGet(LogarViewModel parametros)
        {
            return View("Logar", parametros);
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Logar")]
        public ActionResult LogarPost(LogarViewModel parametros)
        {
            if (ModelState.IsValid)
            {
                Autenticar(parametros);

                return RedirectToRoute(new { @Controller = "Home" });
            }

            return View(parametros);
        }

        [AllowAnonymous]
        public ActionResult Sair()
        {
            base.Sair();

            return RedirectToAction("Logar");

        }
    }
}