using ReverseAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReverseAPI.DAL
{
    public interface IDbLayer
    {
        // Client
        public Task<List<Client>> GetClients();
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

        // Products
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int? id);
        public Task<Product> AddProduct(Product newProduct);
        public Task<Product> UpdateProduct(Product productToUpdate);
        public Task<Product> DeleteProduct(int? id);

        // Purchases
        public Task<IEnumerable<Sale>> GetSales();
        public Task<Sale> GetSale(int? id);
        public Task<Sale> AddSale(Sale newSale);
        public Task<Sale> UpdateSale(Sale saleToUpdate);
        public Task<Sale> DeleteSale(int? id);

        // Supplies
        public Task<IEnumerable<Supply>> GetSupplies();
        public Task<Supply> GetSupply(int? id);
        public Task<Supply> AddSupply(Supply newSupply);
        public Task<Supply> UpdateSupply(Supply supplyToUpdate);
        public Task<Supply> DeleteSupply(int? id);

        // Payments
        public Task<IEnumerable<Payment>> GetPayments();
        public Task<Payment> GetPayment(int? id);
        public Task<Payment> AddPayment(Payment newPayment);
        public Task<Payment> UpdatePayment(Payment paymentToUpdate);
        public Task<Payment> DeletePayment(int? id);
    }
}
