using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models
{
    public class UsuarioAcaoConstrutor
    {

        public static UsuarioACAOAplicacao usuAcaoApp()
        {
            return new UsuarioACAOAplicacao(new RepositoroAcaoAplicacaoADO());
        }

    }
}