using ContratoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoWeb.contrato
{
    public interface IAcaoRepositorio<T> where T : class
    {
        DominioAcao ListarPorId(int id);
        void Salvar(DominioAcao acao);
        int InsereRetornarIDContrato(int contrato);

        DominioAcao ListarPorId_Contrato(int id_contrato);
        IEnumerable<DominioAcao> ListaAcaoPorNROcontratoNroLoja(int nrocontrato);
        List<DominioAcao> bllRetornaLojas();

    }
}
