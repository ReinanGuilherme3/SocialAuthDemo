namespace Web.Communication.Responses.Auth;

public sealed record AuthLoginResponse(string UserId, string Email, bool IsNewUser, string Token);