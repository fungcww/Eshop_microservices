namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, ILogger<IndexModel> logger)
        : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        //Change to initialize by primary constructor
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();
        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page loaded at {Time}", DateTime.UtcNow);
            var result = await catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }
    }
}
