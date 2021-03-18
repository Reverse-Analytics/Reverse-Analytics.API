using Microsoft.EntityFrameworkCore;
using ReverseAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        #region Sale
        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetSale(int? id)
        {
            try
            {
                var purchase = await _context.Sales.FindAsync(id);

                if (purchase == null) return null;

                return purchase;
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot find purchase with given id {id}, {e.Message}");
            }
        }

        public async Task<Sale> AddSale(Sale newPurchase)
        {
            try
            {
                _context.Sales.Add(newPurchase);

                await _context.SaveChangesAsync();

                return newPurchase;
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot add purchase with id: {newPurchase.SaleId}, {e.Message}");
            }
        }

        public async Task<Sale> UpdateSale(Sale purchaseToUpdate)
        {
            try
            {
                _context.Sales.Update(purchaseToUpdate);

                await _context.SaveChangesAsync();

                return purchaseToUpdate;
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot update purchase with given id {purchaseToUpdate.SaleId}, {e.Message}");
            }
        }

        public async Task<Sale> DeleteSale(int? id)
        {
            try
            {
                var purchase = _context.Sales.Find(id);

                if (purchase == null) return null;

                _context.Sales.Remove(purchase);
                await _context.SaveChangesAsync();

                return purchase;
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot delete purchase with given id {id}, {e.Message}");
            }
        }
        #endregion

        #region Supply
        public async Task<IEnumerable<Supply>> GetSupplies()
        {
            try
            {
                return await _context.Supplies.ToListAsync();
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot retrieve supplies, {e.Message}");
            }
        }

        public async Task<Supply> GetSupply(int? id)
        {
            try
            {
                return await _context.Supplies.FindAsync(id);
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot find supply with given id {id}, {e.Message}");
            }
        }

        public async Task<Supply> AddSupply(Supply newSupply)
        {
            try
            {
                _context.Supplies.Add(newSupply);

                await _context.SaveChangesAsync();

                return newSupply;
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot add supply: {newSupply}, {e.Message}");
            }
        }

        public async Task<Supply> UpdateSupply(Supply supplyToUpdate)
        {
            try
            {
                _context.Supplies.Update(supplyToUpdate);
                await _context.SaveChangesAsync();

                return supplyToUpdate;
            }
            catch (DbException e)
            {
                throw new Exception($"Cannot update supply with id: {supplyToUpdate.SupplyId}, {e.Message}");
            }
        }

        public async Task<Supply> DeleteSupply(int? id)
        {
            try
            {
                var supply = _context.Supplies.Find(id);

                if (supply == null) return null;

                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();

                return supply;
            }
            catch(DbException e)
            {
                throw new Exception($"Cannot delete supply with id: {id}, {e.Message}");
            }
        }
        #endregion

        #region Payment
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            try
            {
                return await _context.Payments.ToListAsync();
            }
            catch(Exception e)
            {
                throw new Exception($"Cannot retrieve payments: {e.Message}");
            }
        }

        public async Task<Payment> GetPayment(int? id)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(id);

                int g = 0;

                return payment;
            }
            catch(Exception e)
            {
                throw new Exception($"Cannot retrieve payment with id: {id}, {e.Message}");
            }
        }

        public async Task<Payment> AddPayment(Payment newPayment)
        {
            try
            {
                _context.Payments.Add(newPayment);

                await _context.SaveChangesAsync();


                return newPayment;
            }
            catch(Exception e)
            {
                throw new Exception($"Cannot add new payment, {e.Message}");
            }
        }

        public async Task<Payment> UpdatePayment(Payment paymentToUpdate)
        {
            try
            {
                _context.Payments.Update(paymentToUpdate);

                await _context.SaveChangesAsync();

                return paymentToUpdate;
            }
            catch(Exception e)
            {
                throw new Exception($"Cannot update payment with id: {paymentToUpdate.PaymentId}, {e.Message}: \n {e.InnerException}");
            }
        }

        public async Task<Payment> DeletePayment(int? id)
        {
            try
            {
                var payment = _context.Payments.Find(id);

                if (payment == null) throw new Exception($"There is no payment with given id {id}");

                _context.Payments.Remove(payment);
                
                await _context.SaveChangesAsync();

                return payment;
            }
            catch(Exception e)
            {
                throw new Exception($"Cannot delete payment with id: {id}, {e.Message}");
            }
        }
        #endregion
    }
}
