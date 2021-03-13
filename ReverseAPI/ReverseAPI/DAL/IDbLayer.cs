using ReverseAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReverseAPI.DAL
{
    interface IDbLayer
    {
        // Client
        public Task<IEnumerable<Client>> GetClients();
        public Task<Client> GetClient(int? id);
        public Task<Client> AddClient(Client newCLient);
        public Task<Client> UpdateClient(Client clientToEdit);
        public Task<Client> DeleteClient(int? id);

        // Supplier
        public Task<IEnumerable<Supplier>> GetSuppliers();
        public Task<Supplier> GetSupplier(int? id);
        public Task<Supplier> AddSupplier(Supplier newSupplier);
        public Task<Supplier> UpdateSupplier(Supplier supplierToEdit);
        public Task<Supplier> DeleteSupplier(int? id);
    }
}
