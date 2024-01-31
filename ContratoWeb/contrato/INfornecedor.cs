using ContratoWeb.Models.FORNC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoWeb.contrato
{
    public interface INfornecedor<T> where T : class
    {
        List<DominioFornecedor> RetornaListaForcedores();
    }
}
