using ContratoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoWeb.contrato
{
    public interface INLoja<T> where T : class
    {
        List<DominioLoja> bllRetornaLojas();

    }

}
