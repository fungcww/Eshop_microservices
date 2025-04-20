namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    //input validation
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand> 
        //Implemented FluentValidation library, errors will be captured by MediatR 
    {
        //public CreateProductCommandValidator()
        //{
        //    RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        //    RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        //    RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        //    RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        //    //pipeline behaviors to redirectly error to common 
        //}
    }
    internal class CreateProductCommandHandler
        //(IDocumentSession session, IValidator<CreateProductCommand> validator)
        //(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            //save to database
            //return CreateProductResult result

            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            //if(errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            //logger.LogInformation("CreateProductCommandHandler.Handle called with {@command}", command);

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // TODO
            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);//cancellationToken need for error that requires cancellation
            //return result

            return new CreateProductResult(product.Id);
        }
    }
}
