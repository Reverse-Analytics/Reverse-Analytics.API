using ReverseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReverseAPI.DAL
{
    public class SqlDbLayer : IDbLayer
    {
        public readonly MainContext _context;
        
        // Clients
        public async Task<IEnumerable<Client>> GetClients()
        {
            throw new NotImplementedException();
        }

        public async Task<Client> GetClient(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> AddClient(Client newClient)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> UpdateClient(Client clientToEdit)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> DeleteClient(int? id)
        {
            throw new NotImplementedException();
        }

        // Suppliers
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public async Task<Supplier> GetSupplier(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Supplier> AddSupplier(Supplier newSupplier)
        {
            throw new NotImplementedException();
        }

        public async Task<Supplier> UpdateSupplier(Supplier supplierToEdit)
        {
            throw new NotImplementedException();
        }

        public async Task<Supplier> DeleteSupplier(int? id)
        {
            throw new NotImplementedException();
        }

    }
}
