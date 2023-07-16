using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Application.Commons.Helpers;
using AutoMapper;
using Domain.Entities;

namespace Application.Products.Common;
public class ProductVM: IMapFrom<Product>
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal RegularPrice { get; set; }
    public decimal? SalePrice { get; set; }
    public bool IsOnSale { get; set; }
    public int Quantity { get; set; }
    // Including VAT
    public decimal VATAmount { get { return VATHelper.VATPercentage; } }
    public decimal IncludingVATAmount
    {
        get { return IsOnSale ?  (VATHelper.CalculateVAT(SalePrice ?? 0)) : (VATHelper.CalculateVAT(RegularPrice)); }
    }
    public decimal ExcludingVATAmount
    {
        get { return IsOnSale ? (SalePrice ?? 0) : RegularPrice; }
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductVM>();
        profile.CreateMap<ProductVM, Product>();
    }
}
