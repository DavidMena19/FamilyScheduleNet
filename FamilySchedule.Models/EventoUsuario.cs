using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilySchedule.Models
{
    public class EventoUsuario
    {

        public int Id { get; set; } // Clave primaria de la tabla
        public int EventoId { get; set; } // Relación con la tabla de eventos
        public EventoModel Evento { get; set; }
        public int UsuarioId { get; set; } // Relación con la tabla de usuarios
        public Usuario Usuario { get; set; }
    }
}
