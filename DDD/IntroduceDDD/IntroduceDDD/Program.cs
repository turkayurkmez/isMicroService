// See https://aka.ms/new-console-template for more information
using IntroduceDDD.Domain.Aggregates;
using IntroduceDDD.Domain.Entities;

Console.WriteLine("Hello, World!");
/*
 * Entity
 *     Product
 *     Category
 *     Customer
 *     Order
 *     Vendor
 * 
 * Aggregate: Varlıkları yönetebildiğiniz diğer varlıklar.
 *     Customer customer = new Customer();
 *     customer.CreateOrder(product, 5);
 *     
 *     customer.AddProductToBasket(p,5);
 *     hasta.RandevuAl();
 */

CustomerAggregate customer = new CustomerAggregate();
var product = new Product() { ProductName ="ProductName", UnitPrice=10 };
var quantity = 5;
customer.CreateOrder(product, quantity);
