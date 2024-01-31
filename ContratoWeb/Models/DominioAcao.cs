using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContratoWeb.Models
{
    public class DominioAcao
    {

        [DisplayName("CONTRATO")]
        [Required(ErrorMessage = "Preencha o Numero do contrato !")]
        public int NRO_CONTRATO { get; set; }  


        [DisplayName("ID ACAO")]
        public int ID_ACAO { get; set; } 


        [DisplayName("NUMERO LOJA")]
        public int NROEMPRESA { get; set; } 


        [DisplayName("NOME ACAO")]
        [Required(ErrorMessage = "Preencha o Nome da ação !")]
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string NOME_ACAO { get; set; }


        [DisplayName("LOJA")]
        public string NomeEmpresa { get; set; }


        [DisplayName("OBSERVAÇÃO")]
        [StringLength(45, ErrorMessage = "Campo de 0 a 45 caracteres")]
        public string OBSERVACAO { get; set; }


        [DisplayName("FORN. ACAO")]
        [Required(ErrorMessage = "Informe o fornecedor da ação !")]
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string NomeFonecedorAcao { get; set; }


        [Required(ErrorMessage = "Preencha o valor da ação !")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        [DisplayName("VALOR ACAO")]
        public decimal VALOR_ACAO { get; set; }


        [Required(ErrorMessage = "Preencha a data !")]
        [DisplayName("DATA ACAO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DTA_ACAO { get; set; }


        [DisplayName("NRO NF")]
        [StringLength(12, ErrorMessage = "Campo de 0 a 12 caracteres")]
        public string NRO_NF { get; set; }

        
        [DisplayName("ID CONTRATO")]
        public int ID_CONTRATO { get; set; } 


        [DisplayName("SALDO CONTRATO")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal SALDO_CONTRATO { get; set; } 


    }
}