using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.usuario
{
    public class UsuAplicacaoLogon
    {
        private readonly InLogon<DominioLogon> repositorio;

        public UsuAplicacaoLogon(InLogon<DominioLogon> rep)
        {
            repositorio = rep;
        
        }

        public DominioLogon capturaUsuario(string usu, string sen)
        {
            return repositorio.capturaUsuario(usu, sen);
        }

        public int cadastrarUsuario(string usuario, string senha)
        {
            return repositorio.cadastrarUsuario(usuario, senha);
        }

        public bool existeUsu(string user)
        {
            return repositorio.existeUsu(user);
        }

        public bool CompararUsuario(string user, string usubanco)
        {
            return repositorio.CompararUsuario(user, usubanco);
        }
               
    }
}