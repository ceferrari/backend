using System;
using System.Threading.Tasks;
using Backend.Domain.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Services
{
    public interface ICartService
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(Guid id);
        Task<IActionResult> GetByCustomerId(string customerId);
        Task<IActionResult> Create(CartViewModel cart);
        Task<IActionResult> Update(Guid id, CartItemViewModel item);
        Task<IActionResult> Delete(Guid id);
        Task<IActionResult> DeleteItem(Guid id, Guid itemId);
        Task<IActionResult> Checkout(Guid id, string currencyCode, string xTeamControl);
    }
}
