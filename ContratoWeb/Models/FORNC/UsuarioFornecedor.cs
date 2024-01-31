using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.FORNC
{
    public class UsuarioFornecedor
    {
        private readonly INfornecedor<DominioFornecedor> repositorio;


        public UsuarioFornecedor(INfornecedor<DominioFornecedor> rep)
        {
            repositorio = rep;

        }

        public List<DominioFornecedor> RetornaListaForcedores()
        {
            return repositorio.RetornaListaForcedores();
        }

    }
}