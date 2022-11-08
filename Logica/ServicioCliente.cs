using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Datos;
using NPOI.SS.Formula.Functions;

namespace Logica
{
    public class ServicioCliente : Iservices<Cliente>
    {
        
        List<Cliente> clienteList;
        RepositorioCliente repositorioCliente = new RepositorioCliente();
        public ServicioCliente()
        {
            clienteList = new List<Cliente>();
            clienteList = repositorioCliente.GetAll();
           
        }
        public string Delete(int indice)
        {
            try
            {
                clienteList.RemoveAt(indice);
                repositorioCliente.Actualizar(clienteList, false);
                return "El cliente fue eliminado correctamente";

            }
            catch (Exception)
            {

                return "El cliente no fue eliminado correctamente";
            }
        }


        public string Edit(Cliente Cliente_new, int p)
        {
            Cliente cliente_actual = GetById(Cliente_new, p);
            try
            {
                if (cliente_actual == null)
                {
                    clienteList[p] = Cliente_new;
                    return repositorioCliente.Actualizar(clienteList, false);
                }
                else
                {
                    return "Ya existe un cliente con esta cedula";
                }
            }
            catch (Exception)
            {
                return "Cliente no editado";
            }
            
        }

        public List<Cliente> GetAll()
        {
            return repositorioCliente.GetAll();
        }

        public string Guardar(Cliente Cliente)
        {
            string Guardado = string.Empty;
            try
            {
                if (GetById(Cliente, -1) == null)
                {
                    Guardado = repositorioCliente.Guardar(Cliente);
                    return Guardado;
                }
                else
                {
                    return "Ya existe un cliente con esa cedula";
                }

            }
            catch (Exception)
            {
                return "Se ha prodciono un error, el contacto no fue guardado";

            }
        }
       

        public Cliente GetById(Cliente cliente, int p)
        {
            clienteList = GetAll();
            for (int i = 0; i < clienteList.Count; i++)
            {
                if (clienteList[i].Cedula == cliente.Cedula && i != p)
                {
                    return clienteList[i];
                }
            }
            return null;
        }
    }
}