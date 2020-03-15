using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Backend.Domain.Models.CartModel;
using Backend.Domain.Models.CartModel.Repositories;
using Backend.Shared.Constants;
using Backend.Shared.Utilities;
using Newtonsoft.Json.Linq;

namespace Backend.Infra.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Table _table;

        public CartRepository(AmazonDynamoDBClient dbClient)
        {
            _table = Table.LoadTable(dbClient, "carts_id_cid");
        }

        public async Task<bool> ExistsById(string id)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();

            return querySet.Count > 0;
        }

        public async Task<bool> ExistsByCustomerId(string customerId)
        {
            var filter = new ScanFilter();
            filter.AddCondition("customerId", ScanOperator.Equal, customerId);
            filter.AddCondition("status", ScanOperator.Equal, DomainValues.Cart.Status.Pending);

            var search = _table.Scan(filter);
            var scanSet = await search.GetNextSetAsync();

            return scanSet.Count > 0;
        }

        public async Task<Cart> GetById(string id)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            return CartFromDocument(document);
        }

        public async Task<Cart> GetByCustomerId(string customerId)
        {
            var filter = new ScanFilter();
            filter.AddCondition("customerId", ScanOperator.Equal, customerId);
            filter.AddCondition("status", ScanOperator.Equal, DomainValues.Cart.Status.Pending);

            var search = _table.Scan(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            return CartFromDocument(document);
        }

        public async Task<List<Cart>> GetAll()
        {
            var filter = new ScanFilter();
            var search = _table.Scan(filter);

            var carts = new List<Cart>();
            while (!search.IsDone)
            {
                var querySet = await search.GetNextSetAsync();
                carts.AddRange(querySet.Select(CartFromDocument));
            }

            return carts;
        }

        public async Task<Cart> Add(Cart cart)
        {
            var document = DocumentFromCart(cart);
            await _table.PutItemAsync(document);

            return await GetById(cart.Id.ToString());
        }

        public async Task<Cart> Update(Cart cart)
        {
            var document = DocumentFromCart(cart);
            await _table.UpdateItemAsync(document);

            return await GetById(cart.Id.ToString());
        }

        public async Task<bool> Delete(string id)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            const string cancel = DomainValues.Cart.Status.Cancel;
            document["status"] = cancel;

            await _table.UpdateItemAsync(document);
            var result = await GetById(id);

            return cancel.Equals(result.Status);
        }

        public async Task<bool> DeleteItem(string id, string itemId)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            var items = JsonUtilities.Deserialize<List<CartItem>>(document["items"].AsString());
            var item = items.FirstOrDefault(x => x.Id.ToString() == itemId);
            items.Remove(item);

            await _table.UpdateItemAsync(document);
            var result = await GetById(id);

            return result.Items.All(x => x.Id.ToString() != itemId);
        }

        public async Task<bool> Checkout(string id)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            const string done = DomainValues.Cart.Status.Done;
            document["status"] = done;

            await _table.UpdateItemAsync(document);
            var result = await GetById(id);

            return done.Equals(result.Status);
        }

        #region Private Methods

        private Document DocumentFromCart(Cart cart)
        {
            return new Document
            {
                ["id"] = cart.Id,
                ["customerId"] = cart.CustomerId,
                ["status"] = cart.Status,
                ["items"] = JsonUtilities.Serialize(cart.Items)
            };
        }

        private Cart CartFromDocument(Document document)
        {
            return new Cart(document["id"].AsGuid())
            {
                CustomerId = document["customerId"].AsString(),
                Status = document["status"].AsString(),
                Items = JsonUtilities.Deserialize<List<dynamic>>(document["items"].AsString())
                    .Select(item => JObject.Parse(item.ToString()))
                    .Select(item => new CartItem(Guid.Parse(item.id.ToString()))
                    {
                        Price = long.Parse(item.price.ToString()),
                        Scale = long.Parse(item.scale.ToString()),
                        CurrencyCode = item.currencyCode.ToString()
                    }).ToList()
            };
        }

        #endregion
    }
}