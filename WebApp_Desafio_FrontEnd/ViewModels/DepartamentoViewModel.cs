using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WebApp_Desafio_FrontEnd.ViewModels
{
    [DataContract]
    public class DepartamentoViewModel
    {
        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Descrição")]
        [DataMember(Name = "Descricao")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(maximumLength: 100, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Descricao { get; set; }

    }
}
