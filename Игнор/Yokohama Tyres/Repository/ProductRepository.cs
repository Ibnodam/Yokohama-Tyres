using Microsoft.EntityFrameworkCore;
using Yokohama_Tyres.Models;
using System.Collections.Generic;
using System.Linq;

namespace Yokohama_Tyres.Repositories;

public class ProductRepository
{
    private readonly YokohamaTyresDbContext _context;

    public ProductRepository()
    {
        _context = new YokohamaTyresDbContext();
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products
            .Include(p => p.ProductType)
            .OrderBy(p => p.Name)
            .ToList();
    }

   
    public Product? GetProductById(int id)
    {
        return _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductMaterials)
                .ThenInclude(pm => pm.Material)
            .FirstOrDefault(p => p.ProductId == id);
    }


    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }


    public void DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}