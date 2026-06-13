using Domain.DTO_s;
using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.InternalNote.Querys;

public class GetInternalNoteByIdQuery : IRequest<InternalNoteDto>
{
    public Guid NoteId { get; set; }
}
 
internal sealed class GetInternalNoteByIdQueryHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<GetInternalNoteByIdQuery, InternalNoteDto>
{
    public async Task<InternalNoteDto> Handle(GetInternalNoteByIdQuery request, CancellationToken ct)
    {
        var note = await unitOfWork.InternalNoteRepo.GetByIdAsync(request.NoteId, ct)
                   ?? throw new KeyNotFoundException("La nota no existe");
 
        return new InternalNoteDto(
            note.NoteId,
            note.TicketId,
            note.AuthorId,
            note.Author.Username,
            note.Note,
            note.CreatedAt);
    }
}