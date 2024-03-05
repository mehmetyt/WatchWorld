using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
	public class CatalogFilterSpecification:Specification<Product>
	{
        public CatalogFilterSpecification(int? categryId, int? brandId)
        {
            if(categryId.HasValue)
                Query.Where(x => x.CategoryId == categryId);
            if(brandId.HasValue)
                Query.Where(x => x.BrandId == brandId);
        }

        public CatalogFilterSpecification(int? categryId, int? brandId,int skip,int take):this(categryId,brandId)
        {
            Query.Skip(skip).Take(take);
        }
    }
}
