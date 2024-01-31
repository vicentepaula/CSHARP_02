using ContratoWeb.contrato;
using System.Collections.Generic;

namespace ContratoWeb.Models
{
    public class UsuarioACAOAplicacao
    {
        private readonly IAcaoRepositorio<DominioAcao> repositorio;

        public UsuarioACAOAplicacao(IAcaoRepositorio<DominioAcao> irep)
        {
            repositorio = irep;
        }

        public DominioAcao ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }

        public void Salvar(DominioAcao acao)
        {
            repositorio.Salvar(acao);

        }

        public int InsereRetornarIDContrato(int contrato)
        {
            return repositorio.InsereRetornarIDContrato(contrato);
        }

        public DominioAcao ListarPorId_Contrato(int id_contrato)
        {
           return repositorio.ListarPorId_Contrato(id_contrato);
        }

        public IEnumerable<DominioAcao> ListaAcaoPorNROcontratoNroLoja(int nrocontrato)
        {
           return repositorio.ListaAcaoPorNROcontratoNroLoja(nrocontrato);
        }

        public List<DominioAcao> bllRetornaLojas()
        {
            return repositorio.bllRetornaLojas();
        }
    }
}