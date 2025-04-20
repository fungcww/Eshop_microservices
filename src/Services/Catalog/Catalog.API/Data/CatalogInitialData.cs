using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync())
                {
                return;
                }

            // Marten UPSERT will cater for existing records
            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Category = new List<string> { "Category 1" },
                    Description = "Description 1",
                    ImageFile = "ImageFile 1",
                    Price = 10.0m
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Category = new List<string> { "Category 2" },
                    Description = "Description 2",
                    ImageFile = "ImageFile 2",
                    Price = 20.0m
                }
            };
        }
    }
}
