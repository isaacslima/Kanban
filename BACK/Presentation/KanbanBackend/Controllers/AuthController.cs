using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Common.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;

namespace KanbanBackend.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        bool resultado = ValidarUsuario(loginRequest);

        if (resultado)
        {
            var tokenString = GerarTokenJWT();
            return Ok(new { token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }

    private string GerarTokenJWT()
    {
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var expiry = DateTime.Now.AddMinutes(120);
        var key = _config["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: issuer, audience: audience,
expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
    private bool ValidarUsuario(LoginRequest loginRequest)
    {
        if (loginRequest.login == "letscode" && loginRequest.senha == "lets@123")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

