﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : BaseController<Wishlist,WishlistDto>
    {
        private readonly IWishlistService _wishlistService;
        private readonly WishlistMappingService _wishlistMappingService;
        public WishlistController(IWishlistService wishlistService, WishlistMappingService wishlistMappingService) : base(wishlistMappingService)
        {
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _wishlistMappingService = wishlistMappingService ?? throw new ArgumentNullException(nameof(wishlistMappingService));
        }
        #region GET
        // GET: api/Wishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetAllWishlist([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_wishlistService.GetAllAsync, pagination, "Wishlists");
        }
        // GET: api/Wishlist/{wishlistId}
        [HttpGet("{wishlistId}")]
        public async Task<ActionResult<WishlistDto>> GetWishlistById(int wishlistId)
        {
            return await GetEntityResponse(() => _wishlistService.GetByIdAsync(wishlistId), "Wishlist");
        }
        #endregion

        #region Add 

        // POST: api/Wishlist
        [HttpPost]
        public async Task<ActionResult<WishlistDto>> AddWishlist([FromBody] WishlistDto wishlistDTO)
        {
            return await AddEntityResponse(_wishlistService.AddAsync, wishlistDTO, "Wishlist", nameof(GetWishlistById));
        }

        #endregion

        #region Delete
        // DELETE: api/Wishlist/{wishlistId}
        [HttpDelete("{wishlistId}")]
        public async Task<IActionResult> DeleteWishlist(int wishlistId)
        {
            return await DeleteEntityResponse(_wishlistService.GetByIdAsync, _wishlistService.DeleteAsync, wishlistId);
        }
        #endregion

        #region Edit Put 
        // PUT: api/Wishlist/{wishlistId}
        [HttpPut("{wishlistId}")]
        public async Task<ActionResult> UpdateWishlist(int wishlistId, WishlistDto wishlistDto)
        {
            return await UpdateEntityResponse(_wishlistService.GetByIdAsync, _wishlistService.UpdateAsync, wishlistId, wishlistDto, "Wishlist");
        }
        #endregion
    }
}
