﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.FORNC
{
    public class UsuContrudorDominioFornecedor
    {

        public static UsuarioFornecedor FornecedorAplicacao()
        {
            return new UsuarioFornecedor(new RepositorioFornecedorAplicacaoADO());
        }

    }
}