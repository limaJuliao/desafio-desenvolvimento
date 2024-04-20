using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WebApp_Desafio_FrontEnd.ViewModels
{
    [DataContract]
    public class ChamadoViewModel
    {
        private CultureInfo ptBR = new CultureInfo("pt-BR");

        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Assunto")]
        [DataMember(Name = "Assunto")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(maximumLength: 200, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.", MinimumLength = 10)]
        public string Assunto { get; set; }

        [Display(Name = "Solicitante")]
        [DataMember(Name = "Solicitante")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(maximumLength: 100, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Solicitante { get; set; }

        [Display(Name = "IdDepartamento")]
        [DataMember(Name = "IdDepartamento")]
        [Required(ErrorMessage = "Departamento é obrigatório.")]
        public int IdDepartamento { get; set; }

        [Display(Name = "Departamento")]
        [DataMember(Name = "Departamento")]
        public string Departamento { get; set; }

        [Display(Name = "Data Abertura")]
        [DataMember(Name = "DataAbertura")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataAbertura { get; set; }

        [DataMember(Name = "DataAberturaWrapper")]
        public string DataAberturaWrapper
        {
            get
            {
                return DataAbertura.ToString("d", ptBR);
            }
            set
            {
                DataAbertura = DateTime.Parse(value, ptBR);
            }
        }
    }
}
