using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexaoVerde.Web.Models;

namespace ConexaoVerde.Web.Business.Interfaces
{
    public interface IClienteBussines
    {
        Task RegistrarCliente(ClienteModel clienteModel);
        
    }
}