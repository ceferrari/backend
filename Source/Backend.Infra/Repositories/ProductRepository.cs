using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Backend.Domain.Models.ProductModel;
using Backend.Domain.Models.ProductModel.Repositories;
using Backend.Shared.Utilities;

namespace Backend.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Table _table;

        public ProductRepository(AmazonDynamoDBClient dbClient)
        {
            _table = Table.LoadTable(dbClient, "products_id_sku");
        }

        public async Task<bool> ExistsById(string id)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();

            return querySet.Count > 0;
        }

        public async Task<bool> ExistsBySku(string sku)
        {
            var filter = new ScanFilter();
            filter.AddCondition("sku", ScanOperator.Equal, sku);

            var search = _table.Scan(filter);
            var scanSet = await search.GetNextSetAsync();

            return scanSet.Count > 0;
        }

        public async Task<Product> GetById(string id)
        {
            var filter = new QueryFilter();
            filter.AddCondition("id", QueryOperator.Equal, id);

            var search = _table.Query(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            return ProductFromDocument(document);
        }

        public async Task<Product> GetBySku(string sku)
        {
            var filter = new ScanFilter();
            filter.AddCondition("sku", ScanOperator.Equal, sku);

            var search = _table.Scan(filter);
            var querySet = await search.GetNextSetAsync();
            var document = querySet.First();

            return ProductFromDocument(document);
        }

        public async Task<List<Product>> GetAll()
        {
            var filter = new ScanFilter();
            var search = _table.Scan(filter);

            var carts = new List<Product>();
            while (!search.IsDone)
            {
                var querySet = await search.GetNextSetAsync();
                carts.AddRange(querySet.Select(ProductFromDocument));
            }

            return carts;
        }

        public async Task<Product> Add(Product product)
        {
            var document = DocumentFromProduct(product);
            await _table.PutItemAsync(document);

            return await GetById(product.Id.ToString());
        }

        public async Task<Product> Update(Product product)
        {
            var document = DocumentFromProduct(product);
            await _table.UpdateItemAsync(document);

            return await GetById(product.Id.ToString());
        }

        #region Private Methods

        private static Document DocumentFromProduct(Product product)
        {
            return new Document
            {
                ["id"] = product.Id,
                ["sku"] = product.Sku,
                ["name"] = product.Name,
                ["shortDescription"] = product.ShortDescription,
                ["longDescription"] = product.LongDescription,
                ["imageUrl"] = product.ImageUrl,
                ["price"] = JsonUtilities.Serialize(product.Price)
            };
        }

        private static Product ProductFromDocument(Document document)
        {
            return new Product(document["id"].AsGuid())
            {
                Sku = document["sku"].AsString(),
                Name = document["name"].AsString(),
                ShortDescription = document["shortDescription"].AsString(),
                LongDescription = document["longDescription"].AsString(),
                ImageUrl = document["imageUrl"].AsString(),
                Price = JsonUtilities.Deserialize<ProductPrice>(document["price"])
            };
        }

        #endregion
    }
}