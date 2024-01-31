using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContratoWeb.Models
{
    public class DominioContrato
    {
        
        [DisplayName("CONTRATO")]
        [Required(ErrorMessage = "Preencha o Numero do contrato !")]
        public int NRO_CONTRATO { get; set; } 


        [DisplayName("COD FORNECEDOR")]
        [Required(ErrorMessage = "Preencha o código do fornecedor !")]
        public int COD_FORNECEDOR { get; set; } 

                    
        [DisplayName("EMPRESA")]
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string NomeEmpresa { get; set; }


        [DisplayName("FORNECEDOR")]
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string NomeFonecedor { get; set; }


        [DisplayName("COMPRADOR")]
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string Comprador { get; set; }


        [DisplayName("NROEMPRESA")]
        [StringLength(12, ErrorMessage = "Campo de 1 a 12 caracteres")]
        public int NROEMPRESA { get; set; }

       
        [DisplayName("COD GESTOR")]
        [StringLength(12, ErrorMessage = "Campo de 1 a 12 caracteres")]
        public int COD_GESTOR { get; set; }

               
        [DisplayName("VALOR CONTRATO")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Preencha o Numero do contrato !")]
        public decimal VALOR_CONTRATO { get; set; }


        [DisplayName("NRO NF")]
        [StringLength(12, ErrorMessage = "Campo de 1 a 12 caracteres")]
        [Required(ErrorMessage = "Preencha o numero da Nota fiscal !")]
        public string NRO_NF { get; set; }


        [DisplayName("NOME ACAO")]
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string NOME_ACAO { get; set; }

              
        [DisplayName("SALDO CONTRATO")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal SALDO { get; set; }


        [DisplayName("ID ACAO")]
        public int ID_ACAO { get; set; }


        [DisplayName("DATA CONTRATO")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DTA_CONTRATO { get; set; }
                       
        
        [DisplayName("COMPRADOR")] // CAMPO REPETIDO, VERIFICAR A  EXCLUSÃO
        [StringLength(45, ErrorMessage = "Campo de 1 a 45 caracteres")]
        public string COMPRADOR { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtainicial { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtaFinal { get; set; }
             

        public Boolean checagem { get; set; }




        public string total_contrato { get; set; }//7
        public string total_saldo { get; set; }//7
        public bool saldoZerado { get; set; }

        

    }
}