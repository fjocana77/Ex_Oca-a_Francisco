using System;
using System.Collections.Generic;
using System.Web.Http;
using Entities;
using BLL;

namespace APIRestFull.Controllers
{
    public class AsistentesController : ApiController
    {
        // Crear asistente
        [HttpPost]
        [Route("api/Asistentes")]
        public IHttpActionResult CreateAsistente(Asistentes asistente)
        {
            if (asistente == null)
                return BadRequest("El asistente no puede ser nulo.");

            var _asistentesLogic = new AsistentesLogic();

            try
            {
                var createdAsistente = _asistentesLogic.Create(asistente);
                return Created($"api/Asistentes/{createdAsistente.AsistenteID}", createdAsistente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el asistente: {ex.Message}");
            }
        }

        // Eliminar asistente
        [HttpDelete]
        [Route("api/Asistentes/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var _asistentesLogic = new AsistentesLogic();
            var result = _asistentesLogic.Delete(id);

            if (result)
                return Ok("Asistente eliminado correctamente.");
            else
                return NotFound();
        }

        // Obtener todos los asistentes
        [HttpGet]
        [Route("api/Asistentes")]
        public IHttpActionResult GetAll()
        {
            var _asistentesLogic = new AsistentesLogic();
            try
            {
                var asistentes = _asistentesLogic.RetrieveAll();
                if (asistentes == null || asistentes.Count == 0)
                    return NotFound();

                return Ok(asistentes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Obtener asistente por ID
        [HttpGet]
        [Route("api/Asistentes/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var _asistentesLogic = new AsistentesLogic();
            try
            {
                var asistente = _asistentesLogic.RetrieveById(id);
                if (asistente == null)
                    return NotFound();

                // Crear un objeto anónimo para devolver solo las propiedades necesarias
                var response = new
                {
                    asistente.AsistenteID,
                    asistente.EventoID,
                    asistente.Nombre,
                    asistente.Email,
                    asistente.FechaRegistro
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Actualizar asistente
        [HttpPut]
        [Route("api/Asistentes/{id:int}")]
        public IHttpActionResult UpdateAsistente(int id, Asistentes asistente)
        {
            if (asistente == null)
                return BadRequest("El asistente no puede ser nulo.");

            if (id != asistente.AsistenteID)
                return BadRequest("El ID del asistente no coincide con el ID de la URL.");

            var _asistentesLogic = new AsistentesLogic();

            try
            {
                var updated = _asistentesLogic.Update(asistente);
                if (updated)
                    return Ok("Asistente actualizado correctamente.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el asistente: {ex.Message}");
            }
        }
    }
}
