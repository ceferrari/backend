using System;
using System.Threading.Tasks;
using Backend.Domain.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Services
{
    public interface IProductService
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(Guid id);
        Task<IActionResult> GetBySku(string sku);
        Task<IActionResult> Create(ProductViewModel product);
    }
}
