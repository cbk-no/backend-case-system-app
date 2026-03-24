using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasesController : ControllerBase
{
    private readonly ICaseService _service;

    public CasesController(ICaseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CaseDto>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CaseDto>> GetById(Guid id, CancellationToken ct)
    {
        var task = await _service.GetByIdAsync(id, ct);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<CaseDto>> Create([FromBody] CreateCaseRequest request, CancellationToken ct)
    {
        var created = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CaseDto>> Update(Guid id, [FromBody] UpdateCaseRequest request, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();

    // Assign a case to a user
    [HttpPost("{taskId:guid}/assign")]
    public async Task<ActionResult<CaseDto>> Assign(Guid taskId, [FromBody] AssignCaseRequest request, CancellationToken ct)
    {
        var result = await _service.AssignAsync(taskId, request.UserId, ct);
        return result is null ? NotFound() : Ok(result);
    }

    // List all cases for a project
    [HttpGet("~/api/projects/{projectId:guid}/cases")]
    public async Task<ActionResult<IReadOnlyList<CaseDto>>> GetByProject(Guid projectId, CancellationToken ct)
    {
        var tasks = await _service.GetByProjectAsync(projectId, ct);
        return Ok(tasks);
    }
}
