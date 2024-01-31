using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.LOJAS
{
    public class UsuarioLoja
    {

        private readonly INLoja<DominioLoja> repositorio;

        public UsuarioLoja(INLoja<DominioLoja> rep)
        {
           repositorio = rep;

        }

        public List<DominioLoja> bllRetornaLojas()
        {
            return repositorio.bllRetornaLojas();
        }

    }

}