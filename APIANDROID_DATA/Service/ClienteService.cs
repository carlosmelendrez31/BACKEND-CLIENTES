using APIANDROID_DATA.Services;
using APIANDROID_DTO.CLIENTESDTO;
using APIANDROID_DTO.LoginDTO;
using APIANDROID_MODEL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using BC = BCrypt.Net.BCrypt;
using Dapper;

namespace APIANDROID_DATA
{
    public class ClienteService : IClienteService
    {
        private readonly DbConnection _db;
        private readonly IConfiguration _config;

        public ClienteService(DbConnection db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }



        public async Task<AuthResponseDto?> LoginAsync(Logindto dto)
        {
            using var con = _db.CreateConnection();

            var cliente = await con.QueryFirstOrDefaultAsync<ClienteModel>(
                "SELECT * FROM login_cliente(@Correo)",
                new { Correo = dto.Correo });

            if (cliente == null) return null;

            bool valido = BC.Verify(dto.Contrasena, cliente.Contrasena);
            if (!valido) return null;

            var token = GenerarToken(cliente);

            return new AuthResponseDto
            {
                Token = token,
                Correo = cliente.Correo
            };
        }

        private string GenerarToken(ClienteModel cliente)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, cliente.Correo),
            new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<IEnumerable<ClienteDto>> ObtenerClientesAsync()
        {
            using var con = _db.CreateConnection();
            return await con.QueryAsync<ClienteDto>("SELECT * FROM obtener_clientes()");
        }

        public async Task<ClienteDto?> ObtenerClientePorIdAsync(int id)
        {
            using var con = _db.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<ClienteDto>(
                "SELECT * FROM obtener_cliente_por_id(@Id)", new { Id = id });
        }

        public async Task<bool> CrearClienteAsync(CrearClienteDto dto)
        {
            using var con = _db.CreateConnection();
            var hash = BC.HashPassword(dto.Contrasena);
            var resultado = await con.QueryFirstOrDefaultAsync(
                "SELECT * FROM crear_cliente(@Correo, @Hash, @Nombre, @Edad, @FechaNacimiento)",
                new { dto.Correo, Hash = hash, dto.Nombre, dto.Edad, dto.FechaNacimiento });
            return resultado?.exito ?? false;
        }

        public async Task<bool> ActualizarClienteAsync(int id, Actualizardto dto)
        {
            using var con = _db.CreateConnection();
            return await con.ExecuteScalarAsync<bool>(
                "SELECT actualizar_cliente(@Id, @Nombre, @Edad, @FechaNacimiento)",
                new { Id = id, dto.Nombre, dto.Edad, dto.FechaNacimiento });
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            using var con = _db.CreateConnection();
            return await con.ExecuteScalarAsync<bool>(
                "SELECT eliminar_cliente(@Id)", new { Id = id });
        }
    }
}
