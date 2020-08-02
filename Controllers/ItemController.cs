using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcCrudAPI.Models;
using ArcCrudAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        IItemRepository postRepository;
        public ItemController(IItemRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var items = await postRepository.GetArcItems();
                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        [HttpGet]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem(int? itemID)
        {
            if (itemID == null)
            {
                return BadRequest();
            }

            try
            {
                var arcItem = await postRepository.GetItem(itemID);

                if (arcItem == null)
                {
                    return NotFound();
                }

                return Ok(arcItem);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ArcItems model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await postRepository.AddItem(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeletePost(int? itemId)
        {
            int result = 0;

            if (itemId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await postRepository.DeleteItem(itemId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] ArcItems model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await postRepository.UpdateItem(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}
