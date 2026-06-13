using Application.UseCases.Category.Commands;
using Application.UseCases.Category.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace S13MauricioCV.ReportData.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllCategoriesQuery(), ct));
 
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetCategoryByIdQuery { CategoryId = id }, ct));
 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCategoryCommand command, CancellationToken ct)
    {
        var result = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetAll), new { }, result);
    }
 
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand command, CancellationToken ct)
    {
        command.CategoryId = id;
        return Ok(await mediator.Send(command, ct));
    }
 
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteCategoryCommand { CategoryId = id }, ct));
}