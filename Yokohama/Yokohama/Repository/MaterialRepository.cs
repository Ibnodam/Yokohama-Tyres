using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Yokohama.Models;

namespace Yokohama_Tyres.Repositories;

public class MaterialRepository
{
    private readonly YokohamaTyresDbContext _context;

    public MaterialRepository()
    {
        _context = new YokohamaTyresDbContext();
    }

    public List<Material> GetAllMaterials()
    {
        return _context.Materials
            .OrderBy(m => m.MaterialName)
            .ToList();
    }


    public Material? GetMaterialById(int id)
    {
        return _context.Materials
            .FirstOrDefault(m => m.MaterialId == id);
    }

    public void AddMaterial(Material material)
    {
        _context.Materials.Add(material);
        _context.SaveChanges();
    }


    public void UpdateMaterial(Material material)
    {
        _context.Materials.Update(material);
        _context.SaveChanges();
    }


    public bool DeleteMaterial(int id)
    {
        var material = _context.Materials
            .Include(m => m.ProductMaterials)
            .FirstOrDefault(m => m.MaterialId == id);

        if (material == null)
            return false;

   
        if (material.ProductMaterials.Any())
        {
            return false;
        }

        _context.Materials.Remove(material);
        _context.SaveChanges();
        return true;
    }
}