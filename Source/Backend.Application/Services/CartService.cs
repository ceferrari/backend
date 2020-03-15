using System;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Notifications;
using System.Net;
using System.Threading.Tasks;
using Backend.Domain.Models.CartModel.Commands;
using Backend.Domain.Models.CartModel.Queries;
using Backend.Domain.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Services
{
    public class CartService : AbstractService, ICartService
    {
        public CartService(IBus bus, INotificationHandler notificationHandler)
            : base(bus, notificationHandler)
        {

        }

        public async Task<IActionResult> GetAll()
        {
            var query = new CartGetAllQuery();
            var result = await Bus.RequestAsync(query);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new CartGetByIdQuery(id);
            var result = await Bus.RequestAsync(query);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var query = new CartGetByCustomerIdQuery(customerId);
            var result = await Bus.RequestAsync(query);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> Create(CartViewModel cart)
        {
            var command = new CartCreateCommand(cart);
            var result = await Bus.SubmitAsync(command);

            return Result(HttpStatusCode.Created, result);
        }

        public async Task<IActionResult> Update(Guid id, CartItemViewModel item)
        {
            var command = new CartUpdateCommand(id, item);
            var result = await Bus.SubmitAsync(command);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new CartDeleteCommand(id);
            var result = await Bus.SubmitAsync(command);

            return Result(HttpStatusCode.NoContent, result);
        }

        public async Task<IActionResult> DeleteItem(Guid id, Guid itemId)
        {
            var command = new CartDeleteItemCommand(id, itemId);
            var result = await Bus.SubmitAsync(command);

            return Result(HttpStatusCode.NoContent, result);
        }

        public async Task<IActionResult> Checkout(Guid id, string currencyCode, string xTeamControl)
        {
            var command = new CartCheckoutCommand(id, currencyCode, xTeamControl);
            var result = await Bus.SubmitAsync(command);

            return Result(HttpStatusCode.OK, result);
        }
    }
}
