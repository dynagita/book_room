using AutoMapper;
using BookRoom.Application.UseCases.RooUseCases;
using BookRoom.Domain.Contract.Constants;
using BookRoom.Domain.Contract.Responses;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Contract.UseCases.Rooms;
using BookRoom.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.Logging;

namespace BookRoom.Application.UseCases.RoomUseCases
{
    public class DeleteRoomUseCase : IDeleteRoomUseCase
    {
        private readonly IRoomRepository _repository;
        private readonly ILogger<CreateRoomUseCase> _logger;
        public DeleteRoomUseCase(IRoomRepository repository, ILogger<CreateRoomUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CommonResponse<bool>> HandleAsync(int request, CancellationToken cancellationToken)
        {
            try 
            {
                var room = await _repository.DeleteAsync(request, cancellationToken);
                return CommonResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(DeleteRoomUseCase), nameof(HandleAsync));
                return CommonResponse<bool>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
