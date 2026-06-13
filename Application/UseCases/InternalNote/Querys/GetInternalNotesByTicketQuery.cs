using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.InternalNote.Querys;

public class GetInternalNotesByTicketQuery : IRequest<IEnumerable<InternalNoteDto>>
{
    public Guid TicketId { get; set; }
}
 
internal sealed class GetInternalNotesByTicketQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetInternalNotesByTicketQuery, IEnumerable<InternalNoteDto>>
{
    public async Task<IEnumerable<InternalNoteDto>> Handle(
        GetInternalNotesByTicketQuery request, CancellationToken ct)
    {
        if (!await unitOfWork.TicketRepo.Exists(request.TicketId, ct))
            throw new KeyNotFoundException("El ticket no existe");
 
        var notes = await unitOfWork.InternalNoteRepo.GetByTicketIdAsync(request.TicketId, ct);
 
        return notes.Select(n => new InternalNoteDto(
            n.NoteId,
            n.TicketId,
            n.AuthorId,
            n.Author.Username,
            n.Note,
            n.CreatedAt));
    }
}