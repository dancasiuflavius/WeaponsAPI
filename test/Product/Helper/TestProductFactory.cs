using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponsAPI.Weapons.Model;

namespace test.Products.Helper;

public class TestProductFactory
{
    public static List<Weapon> CreateProducts(int count)
    {
        var products = new List<Weapon>();
        for(int i=1; i<=count;i++)
        {
            products.Add(CreateProduct(i));
        }
        return products;
    }
    public static Weapon CreateProduct(int id)
    {
        return new Weapon
        {
            Id = id,
            Name = $"Product {id}",
            price = 100 + 10 * id,
            Category = $"Category {id % 3 + 1}",
            
        };
    }
    public static List<Weapon> CreateProductsInCategory(string category, int count)
    {
        var products = new List<Weapon>();
        for(int i=1;i<=count;i++)
        {
            var product = CreateProduct(i);
            product.Category = category;
            products.Add(product);
        }
        return products;
    }
    public static List<Weapon> CreateProductsInPriceRange(double minPrice, double maxPrice, int count)
    {
        var products = new List<Weapon>();
        double priceIncrement = (maxPrice - minPrice) / count;
        for (int i = 0; i < count; i++)
        {
            var product = CreateProduct(i + 1);
            product.price = minPrice + priceIncrement * i;
            products.Add(product);
        }
        return products;
    }
    public static Weapon CreateProductWithNoCategory(int id)
    {
        var product = CreateProduct(id);
        product.Category = null;
        return product;
    }
}
