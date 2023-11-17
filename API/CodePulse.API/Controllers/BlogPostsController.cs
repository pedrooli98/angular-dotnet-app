using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodePulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        public BlogPostsController(IBlogRepository repository,ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _repository = repository;
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDTO request){
            var blogPost = new BlogPost 
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory is not null)
                    blogPost.Categories.Add(existingCategory);
            }

            blogPost = await _repository.CreateAsync(blogPost);

            var response = new BlogPostsDTO {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(x => new CategoryDTO 
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _repository.GetAllAsync();

            var response = new List<BlogPostsDTO>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostsDTO
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisible = blogPost.IsVisible,
                    Categories = blogPost.Categories.Select(x => new CategoryDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _repository.GetByIdAsync(id);
            if (blogPost is null)
                return NotFound();
            
            var response = new BlogPostsDTO {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible
            };

            return Ok(response);
        }
    }
}