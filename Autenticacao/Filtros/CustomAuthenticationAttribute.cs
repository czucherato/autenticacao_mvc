using Autenticacao.Identidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace Autenticacao.Filtros
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private FormsAuthenticationTicket _ticket;

        public void OnAuthentication(AuthenticationContext authContext)
        {
            HttpCookie cookie = authContext.HttpContext.Request.Cookies["MinhaConta"];

            if (!Equals(cookie, null))
                _ticket = FormsAuthentication.Decrypt(cookie.Value);

            if (!Equals(_ticket, null) && !_ticket.Expired)
                authContext.Principal = new CustomPrincipal(new FormsIdentity(_ticket), new[] { "Cliente" }, "Carlos", "Zucherato");
            else
                authContext.Result = RedirectToLoginPage();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext authChallengeContext)
        {
            var user = authChallengeContext.HttpContext.User;

            //Se o acesso não for autorizado
            if (!user.Identity.IsAuthenticated)
                authChallengeContext.Result = RedirectToLoginPage();
        }

        private ActionResult RedirectToLoginPage()
        {
            return new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        @Controller = "MinhaConta",
                        @Action = "Logar",
                        @Area = ""
                    }));
        }
    }
}