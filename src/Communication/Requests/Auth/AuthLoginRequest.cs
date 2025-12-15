namespace Communication.Requests.Auth;

public sealed record AuthLoginRequest(string Provider, string Code);