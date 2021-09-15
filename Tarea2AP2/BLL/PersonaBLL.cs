﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tarea2AP2.DAL;
using Tarea2AP2.Data;
using Tarea2AP2.Models;


namespace Tarea2AP2.BLL
{
    public class PersonaBLL
    {
        private Contexto contexto { get; set; }

        public PersonaBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> Guardar(Personas persona)
        {
            if (!await Existe(persona.PersonaId))
                return await Insertar(persona);
            else
                return await Modificar(persona);
        }

        public async Task<bool> Existe(int id)
        {
            bool ok = false;
            try
            {
                ok = await contexto.Personas.AnyAsync(p => p.PersonaId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Personas persona)
        {
            bool ok = false;

            try
            {
                await contexto.Personas.AddAsync(persona);
                ok = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Personas persona)
        {
            bool ok = false;

            try
            {
                contexto.Entry(persona).State = EntityState.Modified;
                ok = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Personas> Buscar(int id)
        {
            Personas persona;

            try
            {
                persona = await contexto.Personas.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }

            return persona;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;

            try
            {
                var registro = await contexto.Personas.FindAsync(id);
                if (registro != null)
                {
                    contexto.Personas.Remove(registro);
                    ok = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Personas>> GetPersonas()
        {
            List<Personas> lista = new List<Personas>();

            try
            {
                lista = await contexto.Personas.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }


            return lista;
        }

        public async Task<List<Personas>> GetPersonas(Expression<Func<Personas, bool>> criterio)
        {
            List<Personas> lista = new List<Personas>();

            try
            {
                lista = await contexto.Personas.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
