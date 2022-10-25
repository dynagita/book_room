using AutoMapper;
using BookRoom.Readness.Domain.Contract.Constants;
using BookRoom.Readness.Domain.Contract.Responses;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using BookRoom.Readness.Domain.Contract.UseCases.RomUseCases;
using BookRoom.Readness.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BookRoom.Readness.Application.UseCases.RoomUseCases
{
    public class ListRoomUseCase : IListRoomUseCase
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListRoomUseCase> _logger;

        public ListRoomUseCase(ILogger<ListRoomUseCase> logger, IMapper mapper, IRoomRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommonResponse<List<RoomResponse>>> HandleAsync(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repository.ListAllAsync(cancellationToken);

                return CommonResponse<List<RoomResponse>>.Ok(_mapper.Map<List<RoomResponse>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - {Method} - Exception Thrown", nameof(ListRoomUseCase), nameof(HandleAsync));
                return CommonResponse<List<RoomResponse>>.BadRequest(ErrorMessages.EXCEPTION_ERROR);
            }
        }
    }
}
