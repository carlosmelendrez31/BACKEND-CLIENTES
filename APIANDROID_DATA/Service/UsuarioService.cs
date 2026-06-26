using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DATA.Service
{
    using APIANDROID_DATA.Interface;
    using APIANDROID_DTO.CLIENTESDTO;
    using APIANDROID_DTO.Crearusuariodto;
    using APIANDROID_DTO.LoginDTO;
    using APIANDROID_MODEL;
    // Services/UsuarioService.cs
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BC = BCrypt.Net.BCrypt;

    public class UsuarioService : IUsuarioService
    {
        private readonly DbConnection _db;
        private readonly IConfiguration _config;

        public UsuarioService(DbConnection db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<bool> CrearUsuarioAsync(CrearUsuarioDto dto)
        {
            using var con = _db.CreateConnection();
            var hash = BC.HashPassword(dto.Contrasena);

            var resultado = await con.QueryFirstOrDefaultAsync(
                "SELECT * FROM crear_usuario(@Correo, @Hash)",
                new { Correo = dto.Correo, Hash = hash });

            return resultado?.exito ?? false;
        }

        public async Task<AuthResponseDto?> LoginAsync(Logindto dto)
        {
            using var con = _db.CreateConnection();

            var usuario = await con.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM login_usuario(@Correo)",
                new { Correo = dto.Correo });

            if (usuario == null) return null;

            bool valido = BC.Verify(dto.Contrasena, usuario.Contrasena);
            if (!valido) return null;

            var token = GenerarToken(usuario);
            return new AuthResponseDto { Token = token, Correo = usuario.Correo };
        }

        private string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
