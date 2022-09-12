using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Beon.Infrastructure
{
  public class BasicAuthOptions
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public BasicAuthOptions(string userName, string password)
    {
      UserName = userName;
      Password = password;
    }
  }

  public class BasicAuthMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly string _username;
    private readonly string _password;

    public BasicAuthMiddleware(RequestDelegate next, IOptions<BasicAuthOptions> options)
    {
      _next = next;
      _username = options.Value.UserName;
      _password = options.Value.Password;
    }
    public async Task InvokeAsync(HttpContext context)
    {
      var request = context.Request;
      string? authHeader = request.Headers["Authorization"].FirstOrDefault();
      if (authHeader == null || !authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
      {
        context.Response.StatusCode = 401;
        context.Response.Headers.Add("WWW-Authenticate", "Basic");
        return;
      }
      var token = authHeader.Substring("Basic ".Length).Trim();
      var credentialstring = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
      var credentials = credentialstring.Split(':');
      if (credentials.Length != 2 || credentials[0] != _username || credentials[1] != _password)
      {
        context.Response.StatusCode = 401;
        context.Response.Headers.Add("WWW-Authenticate", "Basic");
        return;
      }

      await _next.Invoke(context);
    }
  }
}