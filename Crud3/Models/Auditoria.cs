using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crud3.Models
{
    [Table("Auditoria")]

    public class Auditoria
    {

        [Column("Id")]
        [Display(Name = "Codigo")]

        public int Id { get; set; }

        [Column("Alteração")]
        [Display(Name = "Alteração")]

        public string Alteração { get; set; }



    }
}