﻿using Microsoft.EntityFrameworkCore;
using ReverseAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReverseAPI.DAL
{
    public class SqlDbLayer : IDbLayer
    {
        private readonly MainContext _context;
        
        public SqlDbLayer(MainContext context)
        {
            _context = context;
        }

        // Clients
        public async Task<List<Client>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();            

            return clients;
        }

        public async Task<Client> GetClient(int? id)
        {
            var clients = await _context.Clients.ToListAsync();

            return clients.Find(s => s.IdClient == id);
        }

        public async Task<Client> AddClient(Client newClient)
        {
            int g = 0;
            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            return newClient;
        }

        public async Task<Client> UpdateClient(Client clientToEdit)
        {
            _context.Entry(clientToEdit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            /*_context.Clients.Update(clientToEdit);
            await _context.SaveChangesAsync();*/

            return clientToEdit;
        }

        public async Task<Client> DeleteClient(int? id)
        {
            var clientToDelete = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(clientToDelete);
            await _context.SaveChangesAsync();

            _context.Remove(clientToDelete);

            return clientToDelete;
        }

        // Suppliers
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplier(int? id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<Supplier> AddSupplier(Supplier newSupplier)
        {
            await _context.Suppliers.AddAsync(newSupplier);

            await _context.SaveChangesAsync();

            return newSupplier;
        }

        public async Task<Supplier> UpdateSupplier(Supplier supplierToEdit)
        {
            _context.Update(supplierToEdit);
            await _context.SaveChangesAsync();

            return supplierToEdit;
        }

        public async Task<Supplier> DeleteSupplier(int? id)
        {
            var supplierToDelete = await _context.Suppliers.FindAsync(id);

            _context.Suppliers.Remove(supplierToDelete);

            await _context.SaveChangesAsync();

            return supplierToDelete;
        }

    }
}
