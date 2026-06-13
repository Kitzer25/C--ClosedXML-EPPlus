using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.InternalNote.Commands;

public class AddInternalNoteCommand : IRequest<string>
{
    public Guid TicketId { get; set; }
    public Guid AuthorId { get; set; }
    public string Note { get; set; } = null!;
}
 
internal sealed class AddInternalNoteCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<AddInternalNoteCommand, string>
{
    public async Task<string> Handle(AddInternalNoteCommand request, CancellationToken ct)
    {
        if (await unitOfWork.TicketRepo.Exists(request.TicketId, ct))
            throw new KeyNotFoundException("El ticket no existe");
 
        var author = await unitOfWork.UserRepo.GetByIdAsync(request.AuthorId, ct)
                     ?? throw new KeyNotFoundException("El autor no existe");
 
        var note = new Domain.Entities.InternalNote
        {
            NoteId = Guid.NewGuid(),
            TicketId = request.TicketId,
            AuthorId = author.UserId,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow
        };
 
        await unitOfWork.InternalNoteRepo.AddAsync(note, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Nota interna generada exitosamente";
    }
}