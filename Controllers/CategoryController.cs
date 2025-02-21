using InspirePO.DTOs;
using InspirePO.Models;
using InspirePO.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InspirePO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Category Data is required");
            }
            try
            {
                var category = new FormCategory()
                {
                    CategoryId = 0,
                    Category = categoryDTO.Category,
                };

                var result = await _categoryRepository.AddCategory(category);

                var categoryResponse = new CategoryDTO()
                {
                    CategoryId = result.CategoryId,
                    Category = result.Category
                };

                return CreatedAtAction(nameof(GetCategoryById), new { id = categoryResponse.CategoryId }, categoryResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategory()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();
                if (categories == null || !categories.Any())
                {
                    return NotFound("Category record not found");
                }
                var categoryDTOs = categories.Select(c => new CategoryDTO { CategoryId = c.CategoryId, Category = c.Category }).ToList();
                return Ok(categoryDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }

        }

        [HttpGet]
        [Route("{id:long}", Name = "GetCategoryById")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById([FromRoute] long id)
        {
            if (id <= 0)
            {
                return BadRequest("Valid Category Id is required.");
            }
            try
            {
                var result = await _categoryRepository.GetCategoryById(id);
                if (result == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }
                var categoryDTO = new CategoryDTO()
                {
                    CategoryId = result.CategoryId,
                    Category = result.Category
                };

                return Ok(categoryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> RemoveCategory([FromRoute] long id)
        {
            if (id <= 0)
            {
                return BadRequest("Valid Category Id is required.");
            }
            try
            {
                var isDeleted = await _categoryRepository.DeleteCategory(id);
                if (!isDeleted)
                {
                    return NotFound($"Category with ID {id} not found.");
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while deleting the category.",
                    details = ex.Message
                });
            }

        }

        [HttpPut]
        [Route("{id:long}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromRoute] long id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id <= 0)
            {
                return BadRequest("Valid Category Id is required.");
            }

            if (categoryDTO == null)
            {
                return BadRequest("Category data is required.");
            }

            try
            {
                var existingCategory = await _categoryRepository.GetCategoryById(id);
                if (existingCategory == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                var updatedCategory = new FormCategory
                {
                    CategoryId = id,
                    Category = categoryDTO.Category
                };

                var result = await _categoryRepository.UpdateCategory(id, updatedCategory);

                if (!result)
                {
                    return StatusCode(500, "Category update failed.");
                }

                return Ok(new { message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
