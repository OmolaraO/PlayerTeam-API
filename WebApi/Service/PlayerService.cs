using WebApi.Contracts;
using WebApi.DTOs;
using WebApi.DTOs.Player;
using WebApi.DTOs.PlayerSkill;
using WebApi.Entities;
using WebApi.Enum;
using WebApi.Exceptions;
using WebApi.Service.Contracts;

namespace WebApi.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepositoryManager _repository;

        public PlayerService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<GenericResponse<PlayerDto>> CreatePlayerAsync(PlayerForCreationDto playerForCreationDto, bool trackChanges)
        {
            // validations

            if (playerForCreationDto.PlayerSkills.Count() <= 0) throw new PlayerSkillBadRequestException("Player skills not provided");


            if (playerForCreationDto.Position.Trim().ToLower() != PlayerPosition.defender.ToString().ToLower()
                && playerForCreationDto.Position.Trim().ToLower() != PlayerPosition.forward.ToString().ToLower()
                && playerForCreationDto.Position.Trim().ToLower() != PlayerPosition.midfielder.ToString().ToLower())
            {
                throw new PlayerPositionBadRequest($"Inavlid value for position: {playerForCreationDto.Position}");
            }

            playerForCreationDto.PlayerSkills.ToList().ForEach(x =>
            {
                if (x.Skill.ToLower() != PlayerSkills.speed.ToString().ToLower()
                    && x.Skill != PlayerSkills.stamina.ToString().ToLower()
                     && x.Skill != PlayerSkills.strength.ToString().ToLower()
                     && x.Skill != PlayerSkills.defense.ToString().ToLower()
                     && x.Skill != PlayerSkills.attack.ToString().ToLower())
                {
                    throw new PlayerSkillBadRequestException("Invalid Player skill value provided");
                }
            });

            var playerToBeSaved = new Player
            {
                Name = playerForCreationDto.Name,
                Position = playerForCreationDto.Position, 

            };

            var player = await _repository.Player.GetPlayerByNameAsync(playerForCreationDto.Name, trackChanges);

            if (player != null) throw new DuplicatePlayerException($"Player with name: {playerForCreationDto.Name} already exits");


            _repository.Player.CreatePlayer(playerToBeSaved);

            await _repository.SaveAsync();

            // get the last player that was added
            var players = await  _repository.Player.GetAllPlayersAsync(trackChanges);

            var lastPlayerAdded = players?.LastOrDefault();

            var playerSkillsToBeSaved = playerForCreationDto.PlayerSkills.Select(x => new PlayerSkill
            {
                Skill = x.Skill,
                Value = x.Value,
                PlayerId = lastPlayerAdded.Id,
            }).ToList();

            _repository.PlayerSkill.BulkCreatePlayerSkill(playerSkillsToBeSaved);
            await _repository.SaveAsync();

            var currentPlayerSkills = await _repository.PlayerSkill.GetPlayerSkills(lastPlayerAdded.Id, trackChanges);



            var playersSkillsDto = currentPlayerSkills.Select(x => new PlayerSkillDto
            {
                Skill = x.Skill,
                Value = x.Value,
                PlayerId = lastPlayerAdded.Id,
                Id = x.Id
            });

            var playerToBeReturned = new PlayerDto
            {
                Id = lastPlayerAdded.Id,
                Name = lastPlayerAdded.Name,
                Position = lastPlayerAdded.Position,
                PlayerSkills = playersSkillsDto

            };

            return new GenericResponse<PlayerDto>
            {
                Message = "Successfully created player",
                Data = playerToBeReturned
            };


        }

        public async Task<GenericResponse<PlayerDto>> DeletePlayerAsync(int playerId, bool trackChanges)
        {
            var player = await _repository.Player.GetPlayerByIdAsync(playerId, trackChanges);

            if (player is null) throw new PlayerNotFoundException("Player not found");

            _repository.Player.DeletePlayer(player);

            await _repository.SaveAsync();

            return new GenericResponse<PlayerDto>
            {
                Message = "Successfully deleted player",
            };

        }

        public async Task<GenericResponse<PlayerDto>> UpdatePlayerAsync(int playerId, PlayerForUpdateDto playerForUpdateDto, bool trackChanges)
        {
            if (!string.IsNullOrEmpty(playerForUpdateDto.Position) && playerForUpdateDto.Position.Trim().ToLower() != PlayerPosition.defender.ToString().ToLower()
                && playerForUpdateDto.Position.Trim().ToLower() != PlayerPosition.forward.ToString().ToLower()
                && playerForUpdateDto.Position.Trim().ToLower() != PlayerPosition.midfielder.ToString().ToLower())
            {
                throw new PlayerPositionBadRequest($"Invalid value for position: {playerForUpdateDto.Position}");
            }

            playerForUpdateDto.PlayerSkills.ToList().ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.Skill) && x.Skill.ToLower() != PlayerSkills.speed.ToString().ToLower()
                    && x.Skill != PlayerSkills.stamina.ToString().ToLower()
                     && x.Skill != PlayerSkills.strength.ToString().ToLower()
                     && x.Skill != PlayerSkills.defense.ToString().ToLower()
                     && x.Skill != PlayerSkills.attack.ToString().ToLower())
                {
                    throw new PlayerSkillBadRequestException("Invalid Player skill value provided");
                }
            });

            var player = await _repository.Player.GetPlayerByIdAsync(playerId, trackChanges) 
                        ?? throw new PlayerNotFoundException("Player not found");

            var playerDto = new PlayerDto
            {
                Name = player.Name,
                Id = player.Id,
                Position = player.Position,

            };

            var playerSkills = await _repository.PlayerSkill.GetPlayerSkills(playerId, trackChanges);

            if (playerSkills.Count() <= 0)
            {
                playerDto.PlayerSkills = Enumerable.Empty<PlayerSkillDto>();
                return new GenericResponse<PlayerDto>
                {
                    Message = "Player has no skills",
                    Data = playerDto
                };
            }

            // started updating for player

            player.Position = !string.IsNullOrEmpty(playerForUpdateDto.Position) ? playerForUpdateDto.Position : player.Position;
            player.Name = !string.IsNullOrEmpty(playerForUpdateDto.Name) ? playerForUpdateDto.Name : player.Name;

            //check if the skill about to be updated by the name

            var newPlayerSkills = new List<PlayerSkill>();

            foreach(var playerSkill in playerForUpdateDto.PlayerSkills)
            {
                var skill = await _repository.PlayerSkill.GetPlayerSkillAsync(playerSkill.Skill, trackChanges);
                if (skill != null)
                {
                    skill.Value = playerSkill.Value;
                    _repository.PlayerSkill.UpdatePlayerSkill(skill);
                    await _repository.SaveAsync();

                }
                else
                {
                    var playerSkillToBeSaved = new PlayerSkill
                    {
                        PlayerId = player.Id,
                        Skill = playerSkill.Skill,
                        Value = playerSkill.Value,

                    };

                    newPlayerSkills.Add(playerSkillToBeSaved);
                }
            }

           

            _repository.Player.UpdatePlayer(player);

            // add new skill to player
            _repository.PlayerSkill.BulkCreatePlayerSkill(newPlayerSkills);

            await _repository.SaveAsync();

            var updatedPlayer = await _repository.Player.GetPlayerByIdAsync(playerId, trackChanges);

            playerDto.Position = updatedPlayer.Position;
            playerDto.Name = updatedPlayer.Name;

            var updatedPlayerSkills = await _repository.PlayerSkill.GetPlayerSkills(playerId, trackChanges);

            var playerSkillsDto = updatedPlayerSkills.Select(x => new PlayerSkillDto
            {
                Skill = x.Skill,
                Value = x.Value,
                Id = x.Id,
                PlayerId = x.PlayerId
            });

            playerDto.PlayerSkills = playerSkillsDto;

            return new GenericResponse<PlayerDto>
            {
                Message = "SuccessFully updated user",
                Data = playerDto,
            };


                           
            }

        }
    }

