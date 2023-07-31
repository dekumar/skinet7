using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, 
        IGenericRepository<ProductType> productTypeRepo, IGenericRepository<ProductBrand> productBrandRepo, 
            IMapper mapper)
        {
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts() {
            var spec = new ProductWithTypesAndBrandsSpecification();
            var products =await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) {
            //using specifications
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product=await _productRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(product);
            // return new ProductToReturnDto{
            //     Id = product.Id,
            //     Name=product.Name,
            //     Price=product.Price,
            //     PictureUrl=product.PictureUrl,
            //     ProductBrand=product.ProductBrand.Name,
            //     ProductType=product.ProductType.Name,                
            // };
            //using DTO to return data
            
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() {
            //return Ok(await _repository.GetProductBrandsAsync());

            //using generic repo
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() {
            //return Ok(await _repository.GetProductTypesAsync());

            //using generic repo
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}