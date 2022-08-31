using System.ComponentModel.DataAnnotations;

namespace CRUD_Clientes
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime? Birthday { get; set; } 

        public int Age 
        {
            get => GetAge((DateTime)Birthday);
            
        }
        
        private int GetAge(DateTime date)
        {
            if (date.Month > DateTime.Now.Month)
                return (DateTime.Now.Year - date.Year);
            else
                return (DateTime.Now.Year - date.Year-1);

        }

    }

}
