using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.usuario
{
    public class DominioLogon
    {
        public int id_usuario { get; }

        [DisplayName("USUÁRIO")]
        [Required(ErrorMessage = "Preencha o Nome do Usuário !")]
        [StringLength(20, ErrorMessage = "Campo de 1 a 20 caracteres")]
        public string nome { get; set; }

        [DisplayName("SENHA" )]
        [Required(ErrorMessage = "Preencha a Senha do Usuário !")]
        [StringLength(9, ErrorMessage = "Campo de 1 a 20 caracteres")]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [DisplayName("CONFIRMAR")]
        [Required(ErrorMessage = "Preencha a Senha do Usuário !")]
        [StringLength(9, ErrorMessage = "Campo de 1 a 20 caracteres")]
        [DataType(DataType.Password)]
        public string confirmarSenha { get; set; }
    }
}