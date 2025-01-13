using FamilySchedule.Migrations;
using FamilySchedule.Models;
using FamilySchedule.Models.Context;
using FamilySchedule.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace FamilySchedule.Controllers
{
    public class EventoController : Controller
    {
        public List<EventoUsuario> EventoUsuario { get; }
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            
            _context = context;
        }

        //metodo que retorna la lista de eventos
        public async Task<IActionResult> Index()
        {
            
          return View();
        }

        //Metodos Get y post para crear un evento
        [HttpGet]
        public IActionResult Crear()
        {

            return PartialView("_Crear");
        }

        //se crea el evento y se agrega el id de todos los miembros de la familia en el evento
        //con el fin de que a cada familiar le salga el evento

        [HttpPost]
        public async Task<IActionResult> CrearE (EventoModel evento)
        {       

            if (ModelState.IsValid)
            {
                var correoUsuario = HttpContext.Session.GetString("Correo");

                //buscamos a todos los usuarios del grupo familiar del administrador 
                var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(u => u.Admin2 == correoUsuario);

                if(usuarioBD == null)
                {
                    return Json(new { success = false, message = "Usuario no encontrado" });
                }

               
                //se guarda el evento primero para que la base de datos le asigne un id al evento y luego se pueda guardar en eventoUsuario
                evento.Creador = correoUsuario;
                _context.Add(evento);
                await _context.SaveChangesAsync();

                //Instancia de EventoUsuario para agregar el evento y el usuario a la BD

                var eventoUsuario = new EventoUsuario
                {
                    EventoId = evento.Id,
                    UsuarioId = usuarioBD.Id
                };

                //se crea la notificacion
                
                //foreach (var usuario in Usuario)
                //{
                //    var notificacionEvento = new NotificacionesModel
                //    {

                //        Tipo = 3,
                //        Mensaje = usuarioBD.Admin2 + " a creado un nuevo evento",
                //        Fecha = DateTime.Now,
                //        UsuarioCorreo = "string-null",
                //        Admin = correoUsuario,


                //    };
                //}
              

                //_context.Add(notificacionEvento);
                //_context.Add(eventoUsuario);
                //await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Acción exitosa" });
            }

            return Json(new { success = false, message = "Datos inválidos." });
        }

        //metodo que valida que el evento que se edite exista mediante el id
        public async Task<IActionResult> Editar(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var eventInfoFound = await _context.Eventos.FindAsync(id);

            if(eventInfoFound == null)
            {
                return NotFound();
            }

            return View(eventInfoFound);
        }

        //metodo que edita
        [HttpPost]
        public async Task<IActionResult> Editar(EventoModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoEncontrado = await _context.Eventos.FindAsync(evento.Id);
                if(eventoEncontrado != null)
                {
                    eventoEncontrado.Titulo = evento.Titulo;
                    eventoEncontrado.Creador = evento.Creador;
                    eventoEncontrado.Fecha = evento.Fecha;
                    eventoEncontrado.Descripcion = evento.Descripcion;

                    _context.Update(eventoEncontrado);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Evento Actualizado exitosamente";
                    return RedirectToAction("Index");
                    
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> Eliminar(int id)
        {

            if (ModelState.IsValid)
            {
                var eventoEncontrado = await _context.Eventos.FindAsync(id);

                if (eventoEncontrado == null || id == null)
                {
                    return NotFound();
                }             
                
                _context.Eventos.Remove(eventoEncontrado);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Evento Eliminado exitosamente";

                return RedirectToAction("Index");

            }
            
            return NotFound();
        }

        public async Task<IActionResult> GetEventos()
        {
            var correoUsuario = HttpContext.Session.GetString("Correo");

            //obtener el grupo familiar del usuario
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correoUsuario);          
            var gpDelUsuario = usuario.Admin2;

            var eventos = await _context.Eventos
                .Where(e=> e.EventoUsuarios.Any(eu=> eu.UsuarioId == usuario.Id))
                .Select(e => new {
                    id = e.Id,
                    title = e.Titulo,
                    start = e.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"), // Formato ISO 8601
                    description = e.Descripcion
                }).ToListAsync();

            return Json(eventos);
        }

    }
}
