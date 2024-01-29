using WebApi.DTOs;
using WebApi.DTOs.Player;

namespace WebApi.Service.Contracts
{
    public interface IPlayerService
    {
        Task<GenericResponse<PlayerDto>> CreatePlayerAsync(PlayerForCreationDto playerForCreationDto, bool trackChanges);
        Task<GenericResponse<PlayerDto>> UpdatePlayerAsync(int playerId ,PlayerForUpdateDto playerForUpdateDto, bool trackChanges);
        Task<GenericResponse<PlayerDto>> DeletePlayerAsync(int playerId, bool trackChanges);
    }
}
