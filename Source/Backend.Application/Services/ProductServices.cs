using System;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Notifications;
using Backend.Domain.Models.ProductModel.Commands;
using Backend.Domain.Models.ProductModel.Queries;
using System.Net;
using System.Threading.Tasks;
using Backend.Domain.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Services
{
    public class ProductService : AbstractService, IProductService
    {
        public ProductService(IBus bus, INotificationHandler notificationHandler)
            : base(bus, notificationHandler)
        {

        }

        public async Task<IActionResult> GetAll()
        {
            var query = new ProductGetAllQuery();
            var result = await Bus.RequestAsync(query);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new ProductGetByIdQuery(id);
            var result = await Bus.RequestAsync(query);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> GetBySku(string sku)
        {
            var query = new ProductGetBySkuQuery(sku);
            var result = await Bus.RequestAsync(query);

            return Result(HttpStatusCode.OK, result);
        }

        public async Task<IActionResult> Create(ProductViewModel product)
        {
            var command = new ProductCreateCommand(product);
            var result = await Bus.SubmitAsync(command);

            return Result(HttpStatusCode.Created, result);
        }
    }
}
