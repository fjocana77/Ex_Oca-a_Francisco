using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace SLC
{
    public interface IAsistentesService
    {
        // Crear un asistente
        Asistentes CreateAsistentes(Asistentes asistentes);

        // Eliminar un asistente por ID
        bool Delete(int id);

        // Obtener todos los asistente
        List<Asistentes> GetAll();

        // Obtener un asistente por ID
        Asistentes GetById(int id);

        // Actualizar un asistente
        bool UpdateProduct(Asistentes asistentes);
    }
}
