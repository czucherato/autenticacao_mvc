using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Autenticacao.Identidade
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(IIdentity identidade, string[] perfis, string nome, string sobrenome)
        {
            Identity = identidade;
            Perfis = perfis;
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public IIdentity Identity { get; private set; }
        public string[] Perfis { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public bool IsInRole(string perfil)
        {
            return Perfis.Any(p => p.Contains(perfil));
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Nome, Sobrenome);
        }
    }
}