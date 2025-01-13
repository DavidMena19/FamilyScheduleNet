using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySchedule.Models
{
    public class GFamiliaresModel
    {
        [Key]
        public int Id { get; set; }
        public string CorreoAdmin {  get; set; }
        public DateTime FechaCreacion { get; set; }


        //relacion con los miembros

        public ICollection<usuarioGFModel> UsuariosFamiliar{ get; set; }
    }
}
