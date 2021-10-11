using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class ClientDTO
    {      
        public int Id { get; set; }
        [Required(ErrorMessage = "Preenchimento obrigatório.")]
        [StringLength(150, ErrorMessage = "Limite de caracteres excedido.", MinimumLength = 2)]
        public string Name { get; set; }
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório.")]
        public string BirthDate { get; set; }
        public string CreateAt { get; set; }
        public decimal Income { get; set; }
    }
}