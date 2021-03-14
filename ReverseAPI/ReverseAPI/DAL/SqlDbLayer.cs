using Microsoft.EntityFrameworkCore;
using ReverseAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        #region Clients
        public async Task<List<Client>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();

            return clients;
        }

        public async Task<Client> GetClient(int? id)
        {
            var clients = await _context.Clients.ToListAsync();

            return clients.Find(s => s.ClientId == id);
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

        #endregion

        #region Suppliers
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

        #endregion

        #region Products

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product newProduct)
        {
            try
            {
                _context.Products.Add(newProduct);

                await _context.SaveChangesAsync();
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot add new product, {e.Message}");
            }

            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product productToUpdate)
        {
            try
            {
                _context.Products.Update(productToUpdate);
                await _context.SaveChangesAsync();
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot update product with id: {productToUpdate.ProductId}, {e.Message}");
            }

            return productToUpdate;
        }

        public async Task<Product> DeleteProduct(int? id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null) return null;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot delete product with id: {id}, {e.Message}");
            }

            return null;
        }

        #endregion

        #region Purchase
        public async Task<IEnumerable<Purchase>> Get()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task<Purchase> GetPurchase(int? id)
        {
            try
            {
                var purchase = await _context.Purchases.FindAsync(id);

                if (purchase == null) return null;

                return purchase;
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot find purchase with given id {id}, {e.Message}");
            }
        }

        public async Task<Purchase> AddPurchase(Purchase newPurchase)
        {
            try
            {
                _context.Purchases.Add(newPurchase);

                await _context.SaveChangesAsync();

                return newPurchase;
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot add purchase with id: {newPurchase.PurchaseId}, {e.Message}");
            }
        }

        public async Task<Purchase> UpdatePurchase(Purchase purchaseToUpdate)
        {
            try
            {
                _context.Purchases.Update(purchaseToUpdate);

                await _context.SaveChangesAsync();

                return purchaseToUpdate;
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot update purchase with given id {purchaseToUpdate.PurchaseId}, {e.Message}");
            }
        }

        public async Task<Purchase> DeletePurchase(int? id)
        {
            try
            {
                var purchase = _context.Purchases.Find(id);

                if (purchase == null) return null;

                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();

                return purchase;
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot delete purchase with given id {id}, {e.Message}");
            }
        }
        #endregion
    }
}
