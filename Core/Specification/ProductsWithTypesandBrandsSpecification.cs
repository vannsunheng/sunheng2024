
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesandBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesandBrandsSpecification(ProductSpecParams specParams):
        base(
            x=>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.BrandId.HasValue || x.ProductBrandId==specParams.BrandId) && 
            (!specParams.TypeId.HasValue || x.ProductTypeId==specParams.TypeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.productBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(specParams.PageSize *(specParams.PageIndex-1), specParams.PageSize);
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name); break;

                }
            }
        }

        public ProductsWithTypesandBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.productBrand);
        }
    }
}