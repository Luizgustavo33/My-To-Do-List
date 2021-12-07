using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crud3.Models
{
    [Table("ToDoList")]

    public class ToDoList
    {
        [Column("Id")]
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Column("Descrição")]
        [Display(Name = "Descrição")]
        [Required]
        public string Descrição { get; set; }

        [Column("Prioridade")]
        [Display(Name = "Prioridade")]
        [Required]

        public string Prioridade { get; set; }




    }
}