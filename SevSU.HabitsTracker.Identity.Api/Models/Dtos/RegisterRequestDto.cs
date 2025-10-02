namespace SevSU.HabitsTracker.Identity.Api.Models.Dtos;

public sealed record RegisterRequestDto(string Email, string Username, string Password);