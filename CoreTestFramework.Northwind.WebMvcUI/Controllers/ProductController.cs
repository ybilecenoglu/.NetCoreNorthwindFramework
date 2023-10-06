﻿using AutoMapper;
using CoreTestFramework.Core.Common;
using CoreTestFramework.Northwind.Business.Abstract;
using CoreTestFramework.Northwind.Entities.Concrate;
using CoreTestFramework.Northwind.Entities.DTO;
using CoreTestFramework.Northwind.WebMvcUI.ViewModels;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using static CoreTestFramework.Northwind.WebMvcUI.Extension.QueryableExtension;
namespace CoreTestFramework.Northwind.WebMvcUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public IActionResult Index(ProductViewModel vm)
        {
            return View();
        }

        public async Task<IActionResult> PageData(IDataTablesRequest request)
        {
            Result result = new Result { Success = false, };
            Result<IQueryable<ProductDTO>> product_result = new Result<IQueryable<ProductDTO>>();
            DataTablesResponse response;
            try
            {
                if (request == null)
            {
                result.Message = "İstek elde edilemedi.";
                TempData["result"] = result;
                response = DataTablesResponse.Create(request, 0, 0, new List<Product>());
                return new DataTablesJsonResult(response,true);
            }

            var products =  await _productService.GetProductListAsync();
            var mapped_list_product = _mapper.Map<List<ProductDTO>>(products.Data);
            product_result.Data = mapped_list_product.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                var searchToUpperValue = request.Search.Value.Trim().ToUpper();
                var searchToLowerValue = request.Search.Value.Trim().ToLower();

                product_result.Data = product_result.Data.Where(p => p.product_name.ToUpper().Contains(searchToUpperValue) || p.product_name.ToLower().Contains(searchToLowerValue));
            }

            var dataPage = product_result.Data;
            var orderColumns = request.Columns.Where(c => c.IsSortable == true && c.Sort != null);
            var searchColumns = request.Columns.Where(c => c.IsSearchable);

            if (orderColumns.Count() == 0)
            {
                dataPage = dataPage.Take(request.Length);
            }
            else {
                if (request.Length > 0)
                {
                    dataPage = dataPage.OrderBy(orderColumns).Skip(request.Start).Take(request.Length);
                }
                else{
                    dataPage = dataPage.OrderBy(orderColumns);
                }
            }
            response = DataTablesResponse.Create(request, product_result.Data.Count(), product_result.Data.Count(), dataPage);
            return new DataTablesJsonResult(response, true);
            }
            catch (System.Exception ex)
            {
                string message = ex.Message;
                throw;
            }
            
        }
        public async Task<ActionResult> Delete(int id) {
            
        }
    }
}
