using Gizli.Infrastructure.Context;
using Gizli.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gizli.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class AlarmController : ApiController
    {
        private readonly ApplicationDbContext _db;
       
        public AlarmController(IMediator mediator, ApplicationDbContext db) : base(mediator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
          
            var logs = await _db.AlarmLogs
                .FromSqlInterpolated($"SELECT Id, Temperature, CreatedAt FROM AlarmLogs ORDER BY CreatedAt DESC")
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Ok(logs);
        }
        [HttpPost]
        public async Task<IActionResult> GetLast(int count, CancellationToken cancellationToken)
        {
            if (count <= 0) return BadRequest("count must be > 0");

            var logs = await _db.AlarmLogs
                .FromSqlInterpolated($"SELECT TOP({count}) Id, Temperature, CreatedAt FROM AlarmLogs ORDER BY CreatedAt DESC")
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Ok(logs);
        }
    }
}
