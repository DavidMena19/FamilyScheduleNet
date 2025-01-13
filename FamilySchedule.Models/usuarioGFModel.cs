using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySchedule.Models
{
    public class usuarioGFModel
    {
        [Key]
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public GFamiliaresModel grupoFamiliar { get; set; }

        public int usuarioID { get; set; }
        public Usuario usuario { get; set; }

        public DateTime FechaUnion {  get; set; }
    }
}
