/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */

using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Libros : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Libros> _listaLibros = new List<Libros>();

        private string _titulo = string.Empty;
        private decimal _precioRentaDiaria;
        private int _copiasDisponibles;
        private bool _esNovedad;
        private string _rutaImagen = string.Empty;

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El título no puede estar vacío.");
                _titulo = value.Trim();
            }
        }

        public decimal PrecioRentaDiaria
        {
            get { return _precioRentaDiaria; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio de renta debe ser mayor a cero.");
                _precioRentaDiaria = value;
            }
        }

        public int CopiasDisponibles
        {
            get { return _copiasDisponibles; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Las copias no pueden ser negativas.");
                _copiasDisponibles = value;
            }
        }

        public bool EsNovedad
        {
            get { return _esNovedad; }
            set { _esNovedad = value; }
        }

        public string RutaImagen
        {
            get { return _rutaImagen; }
            set { _rutaImagen = string.IsNullOrWhiteSpace(value) ? "libro_default.png" : value.Trim(); }
        }

        public Libros() : base(1, DateTime.Now, true)
        {
            this.Titulo = "Sin Título";
            this.PrecioRentaDiaria = 20.0m;
            this.CopiasDisponibles = 1;
            this.EsNovedad = false;
            this.RutaImagen = "libro_default.png";
        }

        // El ID (heredado de EntidadBase) es el identificador único del libro.
        public Libros(int id, string titulo, decimal precioRentaDiaria, int copiasDisponibles, bool esNovedad, string rutaImagen, bool estado)
            : base(id, DateTime.Now, estado)
        {
            this.Titulo = titulo;
            this.PrecioRentaDiaria = precioRentaDiaria;
            this.CopiasDisponibles = copiasDisponibles;
            this.EsNovedad = esNovedad;
            this.RutaImagen = rutaImagen;
        }

        public decimal CalcularCostoRenta(int dias)
        {
            if (dias <= 0)
                throw new ArgumentException("Los días deben ser mayores a cero.");

            return dias * this.PrecioRentaDiaria;
        }

        public decimal CalcularCostoRenta(int dias, decimal porcentajeDescuento)
        {
            if (porcentajeDescuento < 0 || porcentajeDescuento > 100)
                throw new ArgumentOutOfRangeException(nameof(porcentajeDescuento), "El descuento debe ser entre 0 y 100.");

            decimal costoBase = CalcularCostoRenta(dias);
            decimal descuento = costoBase * (porcentajeDescuento / 100m);
            return costoBase - descuento;
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto is Libros libro)
                _listaLibros.Add(libro);
            else
                throw new ArgumentException("El objeto no es del tipo Libros.");
        }

        public object? ConsultarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            foreach (Libros libro in _listaLibros)
            {
                if (libro.Id == idBuscado)
                    return libro;
            }
            return null;
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto is Libros libroActualizado)
            {
                for (int i = 0; i < _listaLibros.Count; i++)
                {
                    if (_listaLibros[i].Id == libroActualizado.Id)
                    {
                        _listaLibros[i] = libroActualizado;
                        return;
                    }
                }
                throw new ArgumentException("No se encontró el libro a actualizar.");
            }
            else
            {
                throw new ArgumentException("El objeto no es del tipo Libros.");
            }
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = int.Parse(id);
            for (int i = 0; i < _listaLibros.Count; i++)
            {
                if (_listaLibros[i].Id == idBuscado)
                {
                    _listaLibros.RemoveAt(i);
                    return;
                }
            }
            throw new ArgumentException("No se encontró el libro a eliminar.");
        }

        public static List<Libros> ObtenerTodos()
        {
            return _listaLibros;
        }

        public override string ToString()
        {
            string novedadStr = EsNovedad ? " (Novedad)" : "";
            string estadoStr = EsActivo ? "Disponible" : "No disponible";
            return $"[ID: {Id}] {Titulo}{novedadStr} | Stock: {CopiasDisponibles} | Precio/Día: ${PrecioRentaDiaria:F2} | Estado: {estadoStr}";
        }
    }
}