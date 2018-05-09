using Autenticacao.Identidade;
using Autenticacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Autenticacao.Controllers.Comum
{
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        protected void Autenticar(LogarViewModel parametros)
        {
            FormsAuthenticationTicket authTicket = GerarTicket(parametros);

            string encryptTicket = FormsAuthentication.Encrypt(authTicket);

            GerarCookie(encryptTicket);
        }

        protected void Sair()
        {
            HttpCookie cookie = Request.Cookies["MinhaConta"];

            FormsAuthenticationTicket authTicket = DecriptarTicket(cookie.Value);

            authTicket = AtualizarTicket(authTicket);

            string encryptTicket = FormsAuthentication.Encrypt(authTicket);

            AtualizarCookie(cookie.Name, encryptTicket);
        }

        private string EncriptarTicket(FormsAuthenticationTicket authTicket)
        {
            return FormsAuthentication.Encrypt(authTicket);
        }

        private FormsAuthenticationTicket DecriptarTicket(string encryptTicket)
        {
            return FormsAuthentication.Decrypt(encryptTicket);
        }

        private void GerarCookie(string encryptTicket)
        {
            HttpCookie cookie = new HttpCookie("MinhaConta", encryptTicket);
            Response.Cookies.Add(cookie);
        }

        private void AtualizarCookie(string valor, string encryptTicket)
        {
            Request.Cookies.Remove(valor);

            HttpCookie cookie = new HttpCookie("MinhaConta", encryptTicket);
            Response.Cookies.Add(cookie);
        }

        private void RemoverCookie(string coockie)
        {
            throw new NotImplementedException();
        }

        private FormsAuthenticationTicket GerarTicket(LogarViewModel parametros)
        {
            return new FormsAuthenticationTicket(
                1,
                parametros.Login,
                DateTime.Now,
                parametros.Lembrar ? DateTime.Now.AddDays(15) : DateTime.Now.AddHours(1),
                parametros.Lembrar,
                "MinhaConta");
        }

        private FormsAuthenticationTicket AtualizarTicket(FormsAuthenticationTicket ticket)
        {
            return new FormsAuthenticationTicket(
                1,
                ticket.Name,
                DateTime.Now,
                DateTime.Now.AddDays(-1),
                false,
                "MinhaConta");
        }
    }
}