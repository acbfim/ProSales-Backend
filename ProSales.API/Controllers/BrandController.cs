using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSales.Application;
using ProSales.Application.Contracts;
using ProSales.Domain.Dtos;

namespace ProSales.API.Controllers;

//[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,admin,administrador,Administrador")]
public class BrandController : ControllerBase
{
    private readonly IBrandService brandService;

    public BrandController(IBrandService brandService)
    {
        this.brandService = brandService;
    }


    /// <summary>
    /// Get Brand by ExternalID
    /// </summary>
    /// <param name= "ExternalId"></param>
    [HttpGet("by-external-id/{externalId}")]
    public async Task<IActionResult> getByExternalId(Guid externalId)
    {
        var ret = this.brandService.GetBrandByExternalId(externalId).Result;
        return StatusCode(ret.StatusCode, ret);
    }

    /// <summary>
    /// Get Brand by Name
    /// </summary>
    /// <param name= "Name"></param>
    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetBrandByName(string name)
    {
        var ret = this.brandService.GetBrandByName(name).Result;
        return StatusCode(ret.StatusCode, ret);
    }

    /// <summary>
    /// Get Brand by Query
    /// </summary>
    [HttpGet("by-query")]
    public async Task<IActionResult> GetAllBrandByQuery([FromQuery] BrandQuery query)
    {
        var ret = this.brandService.GetAllBrandByQuery(query).Result;
        return StatusCode(ret.StatusCode, ret);
    }

    /// <summary>
    /// Post create Brand
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> postCreate(CreateBrandDto brand)
    {
        var ret = this.brandService.CreateBrand(brand).Result;
        return StatusCode(ret.StatusCode, ret);
    }

    /// <summary>
    /// Put update Brand
    /// </summary>
    [HttpPut("update")]
    public async Task<IActionResult> putUpdate(BrandDto brand)
    {
        var ret = this.brandService.UpdateBrand(brand).Result;
        return StatusCode(ret.StatusCode, ret);
    }

    /// <summary>
    /// Put toogle alter status active Brand
    /// </summary>
    /// <param name= "externalId"></param>
    [HttpPut("toggleStatus/by-external-id/{externalId}")]
    public async Task<IActionResult> putToogleDesactivate(Guid externalId)
    {
        var ret = this.brandService.ToggleDesactivateBrand(externalId).Result;
        return StatusCode(ret.StatusCode, ret);
    }
}
