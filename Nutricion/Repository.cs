using Microsoft.Win32.SafeHandles;
using Nutricion.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutricion
{
    public class Repository
    {
        /*
        private readonly string _path;
        public string StatusMessage { get; set; }
        private SQLiteConnection conn;
        private void Init() { 
            if(conn is not null)
            {
                return;
            }
            else
            {
                conn = new(_path);
            }
            conn.CreateTable<IMC>();
        }
        public Repository(string path)
        {
            _path = path;
        }
        public void AgregarRegistro(IMC imc)
        {
            try {
                Init();
                if (imc is null)
                {
                    throw new Exception("Error al guardar");
                }
                else {
                    IMC imcguardar = new IMC();
                    imcguardar = imc;
                    imcguardar.Fecha = DateTime.Now;

                    int resultado=conn.Insert(imcguardar);
                    StatusMessage = "Registro Guardado"+resultado;

                }
            }catch(Exception ex) {
                StatusMessage = "Error de:"+ex.Message;
            }
        }
        public List <IMC> ObtenerRegistros()
        {
            try
            {
                Init();
                return conn.Table<IMC>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "error al obtener datos" + ex.Message;
            }return new List<IMC>();
        }
    }
        */
        private readonly string _path;
        public string StatusMessage { get; set; }
        private SQLiteConnection conn;
        private void Init()
        {
            if (conn is not null)
            {
                return;
            }
            else
            {
                conn = new(_path);
            }
            conn.CreateTable<Impuesto>();
        }
        public Repository(string path)
        {
            _path = path;
        }
        public void AgregarRegistro(Impuesto imp)
        {
            try
            {
                Init();
                if (imp is null)
                {
                    throw new Exception("Error al guardar");
                }
                else
                {
                    Impuesto impguardar = new Impuesto();
                    impguardar = imp;
                    impguardar.Fecha = DateTime.Now;

                    int resultado = conn.Insert(impguardar);
                    StatusMessage = "Registro Guardado" + resultado;

                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Error de:" + ex.Message;
            }
        }
        public List<Impuesto> ObtenerRegistros()
        {
            try
            {
                Init();
                return conn.Table<Impuesto>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "error al obtener datos" + ex.Message;
            }
            return new List<Impuesto>();
        }
    }
}
