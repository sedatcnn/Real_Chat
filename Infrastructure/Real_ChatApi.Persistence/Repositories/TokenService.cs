﻿using Microsoft.EntityFrameworkCore;
using Real_ChatApi.Application.Features.MedaitR.Results.UserResult;
using Real_ChatApi.Application.Tools;
using Real_ChatApi.Domain.Entites;
using Real_ChatApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Persistence.Repositories
{
    public class TokenService
    {
        private readonly ChatDbContext _context;

        public TokenService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<(string jwtToken, string refreshToken)> GenerateTokensAsync(User user)
        {
            // 1) JWT Token (senin mevcut GenerateToken kodun)
            var jwtToken = JwtTokenGenerateToken.GenerateToken(new GetCheckAppUserQueryResult
            {
                Id = user.Id,
                Mail = user.Email,
                Username = user.UserName
            }).Token;

            // 2) Refresh Token üret (random string)
            var refreshToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            // 3) Refresh Token veritabanına kaydet
            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7) // Örnek: 7 gün geçerli
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return (jwtToken, refreshToken);
        }

        public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var tokenEntity = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Token == refreshToken && !rt.IsRevoked);

            if (tokenEntity == null || tokenEntity.Expires < DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var tokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            if (tokenEntity != null)
            {
                tokenEntity.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }

}
