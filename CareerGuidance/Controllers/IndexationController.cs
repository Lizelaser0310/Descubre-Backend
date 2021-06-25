using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Algolia.Search.Clients;
using Algolia.Search.Models.Common;
using Algolia.Search.Models.Settings;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace CareerGuidance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexationController : ControllerBase
    {
        private readonly string _dbConnection;
        private readonly SearchClient _algolia;

        public IndexationController(SearchClient algolia, IConfiguration config)
        {
            _algolia = algolia;
            _dbConnection = config.GetConnectionString("connectionDB");
        }

        [HttpGet]
        public async Task<ActionResult> RefreshIndexes(string indexes)
        {
            if (string.IsNullOrEmpty(indexes))
            {
                return BadRequest("Indexes is mandatory");
            }

            var tasks = new Dictionary<string,Func<Task<MultiResponse>>>()
            {
            };

            var selectedTasks = new List<Task<MultiResponse>>();

            if (indexes == "*")
            {
                selectedTasks.AddRange(tasks.Select(item => item.Value()));
            }
            else
            {
                var entities = indexes.Split(",");
                if (entities.Length == 0)
                {
                    return BadRequest("At least one entity");
                }

                foreach (var index in entities)
                {
                    try
                    {
                        selectedTasks.Add(tasks[index]());
                    }
                    catch (KeyNotFoundException)
                    {
                        return BadRequest($"The index {index} was not found");
                    }
                }
            }

            var responses = await Task.WhenAll(selectedTasks);

            return Ok(responses);
        }

        public async Task<MultiResponse> RefreshIndex<T, R>(
            Func<DescubreContext, IQueryable<T>> callBack,
            string indexUid,
            Func<T, R> mutation
        ) where R : class
        {
            var index = _algolia.InitIndex(indexUid);

            var optionsBuilder = new DbContextOptionsBuilder<DescubreContext>();
            optionsBuilder.UseSqlServer(_dbConnection);
            await using var _db = new DescubreContext(optionsBuilder.Options);

            var entities = await callBack(_db).ToListAsync();
            var values = entities.Select(mutation);

            await index.SetSettingsAsync(new IndexSettings()
            {
                CustomRanking = new List<string> { "desc(objectID)" }
            });

            return await index.ReplaceAllObjectsAsync(values);
        }
    }
}