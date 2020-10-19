using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevelopment_Assignment2.Models;

namespace WebDevelopment_Assignment2.App_Data
{
    public interface IShoppingRepo
    {
        bool saveChanges();

        //Product
        void createProduct(Product product);
        IEnumerable<Product> getAllProducts();
        Product getProductById(int id);
        void removeProduct(Product product);
        void editProduct(Product product);

        //Cart
        void createCart(Cart cart);
        Cart getCartById(int id);

        //CartItem
        //AllCartItems of CartID
        void AddToCart(CartItem cartItem);
        IEnumerable<CartItem> getAllCartItems(int id);

        //Orders
        //AllOrders of CustomerID
        void createOrder(Order order);
        Order getOrderById(int id);
        //IEnumerable<Product> getAllOrders(int id);
        IEnumerable<Order> getAllOrders(string res);
    }
}
