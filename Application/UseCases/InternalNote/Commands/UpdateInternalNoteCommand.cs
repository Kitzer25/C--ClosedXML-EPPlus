using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.InternalNote.Commands;

public class UpdateInternalNoteCommand : IRequest<string>
{
    public Guid NoteId { get; set; }
    public Guid AuthorId { get; set; }
    public string Note { get; set; } = null!;
}
 
internal sealed class UpdateInternalNoteCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateInternalNoteCommand, string>
{
    public async Task<string> Handle(UpdateInternalNoteCommand request, CancellationToken ct)
    {
        var note = await unitOfWork.InternalNoteRepo.GetByIdAsync(request.NoteId, ct)
                   ?? throw new KeyNotFoundException("La nota no existe");
 
        // Solo el autor original puede modificar su nota
        if (note.AuthorId != request.AuthorId)
            throw new ApplicationException("Solo el autor puede modificar la nota");
 
        note.Note = request.Note;
 
        await unitOfWork.InternalNoteRepo.UpdateAsync(request.AuthorId, note, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Nota interna actualizada exitosamente";
    }
}