using System;
using System.Collections.Generic;
using System.Linq;
using DAL; // Referencia a tu capa de datos
using Entities; // Referencia a tus entidades

namespace BLL
{
    public class AsistentesLogic
    {
        public Asistentes Create(Asistentes asistente)
        {
            // Validar que el asistente no tenga propiedades nulas o vacías
            if (asistente == null)
                throw new ArgumentNullException(nameof(asistente), "El objeto asistente no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(asistente.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(asistente.Nombre));

            if (string.IsNullOrWhiteSpace(asistente.Email))
                throw new ArgumentException("El email no puede estar vacío.", nameof(asistente.Email));

            if (asistente.EventoID <= 0)
                throw new ArgumentException("El ID del evento debe ser mayor que cero.", nameof(asistente.EventoID));

            Asistentes _asistente = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Asistentes _result = repository.Retrieve<Asistentes>(a => a.Email == asistente.Email);

                if (_result == null)
                {
                    _asistente = repository.Create(asistente);
                }
                else
                {
                    throw new Exception("Ya existe un asistente con ese correo electrónico.");
                }
            }

            return _asistente; // Retorna el asistente creado.
        }

        public Asistentes RetrieveById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            Asistentes _asistente = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                _asistente = repository.Retrieve<Asistentes>(a => a.AsistenteID == id);
            }
            return _asistente;
        }

        public bool Update(Asistentes asistente)
        {
            // Validar que el asistente no tenga propiedades nulas o vacías
            if (asistente == null)
                throw new ArgumentNullException(nameof(asistente), "El objeto asistente no puede ser nulo.");

            if (asistente.AsistenteID <= 0)
                throw new ArgumentException("El ID del asistente debe ser mayor que cero.", nameof(asistente.AsistenteID));

            if (string.IsNullOrWhiteSpace(asistente.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(asistente.Nombre));

            if (string.IsNullOrWhiteSpace(asistente.Email))
                throw new ArgumentException("El email no puede estar vacío.", nameof(asistente.Email));

            if (asistente.EventoID <= 0)
                throw new ArgumentException("El ID del evento debe ser mayor que cero.", nameof(asistente.EventoID));

            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Busca el asistente original en la base de datos
                var existingAsistente = repository.Retrieve<Asistentes>(a => a.AsistenteID == asistente.AsistenteID);

                if (existingAsistente == null)
                {
                    throw new Exception("El asistente no existe.");
                }

                // Actualiza manualmente las propiedades necesarias
                existingAsistente.EventoID = asistente.EventoID;
                existingAsistente.Nombre = asistente.Nombre;
                existingAsistente.Email = asistente.Email;
                existingAsistente.FechaRegistro = asistente.FechaRegistro;

                // Guarda los cambios
                return repository.Update(existingAsistente);
            }
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            bool _delete = false;
            var _asistente = RetrieveById(id);
            if (_asistente != null)
            {
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    _delete = repository.Delete(_asistente);
                }
            }
            else
            {
                throw new Exception("El asistente no existe.");
            }
            return _delete;
        }

        public List<Asistentes> RetrieveAll()
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Usar una expresión lambda para devolver una lista de Asistentes
                var asistentes = repository.Filter<Asistentes>(a => a.AsistenteID > 0).ToList();
                return asistentes;
            }
        }
    }
}
