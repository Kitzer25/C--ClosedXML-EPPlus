using Domain.Ports.Repositories;
using MediatR;

namespace Application.UseCases.InternalNote.Commands;

public class DeleteInternalNoteCommand : IRequest<string>
{
    public Guid NoteId { get; set; }
}
 
internal sealed class DeleteInternalNoteCommandHandler(
    IUnitOfWork unitOfWork) :
    IRequestHandler<DeleteInternalNoteCommand, string>
{
    public async Task<string> Handle(DeleteInternalNoteCommand request, CancellationToken ct)
    {
        var note = await unitOfWork.InternalNoteRepo.GetByIdAsync(request.NoteId, ct)
                   ?? throw new KeyNotFoundException("La nota no existe");
 
        await unitOfWork.InternalNoteRepo.DeleteAsync(note, ct);
        await unitOfWork.SaveChangesAsync(ct);
 
        return "Nota interna eliminada exitosamente";
    }
}