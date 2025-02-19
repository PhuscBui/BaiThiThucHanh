﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaiThiThucHanh.Models;
using BaiThiThucHanh.Models.ProductModels;

namespace BaiThiThucHanh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham = (from sp in db.TDanhMucSps
                           select new Product
                           {
                               MaSp = sp.MaSp,
                               TenSp = sp.TenSp,
                               MaLoai = sp.MaLoai,
                               AnhDaiDien = sp.AnhDaiDien,
                               GiaLonNhat = sp.GiaLonNhat
                           }).ToList();
            return sanPham;
        }

        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetProductsByCategory(string maloai)
        {
            var sanPham = (from sp in db.TDanhMucSps
                           where sp.MaLoai == maloai
                           select new Product
                           {
                               MaSp = sp.MaSp,
                               TenSp = sp.TenSp,
                               MaLoai = sp.MaLoai,
                               AnhDaiDien = sp.AnhDaiDien,
                               GiaLonNhat = sp.GiaLonNhat
                           }).ToList();
            return sanPham;
        }

    }
}
