using Microsoft.EntityFrameworkCore; // <-- ToListAsync için gerekli
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Domain.Entites;
using Real_ChatApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Real_ChatApi.Persistence.Repositories
{
    public class WithRepository : IWithRepository
    {
        private readonly ChatDbContext _context;

        public WithRepository(ChatDbContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetUsersAsync()
        {
            // ToListAsync ile asenkron çalıştırıyoruz
            return _context.Users.ToListAsync();
        }

        public Task<List<Message>> GetMessagesAsync()
        {
            return _context.Messages.ToListAsync();
        }

        public Task<List<Group>> GetGroupsAsync()
        {
            return _context.Groups.ToListAsync();
        }
        public async Task<List<Message>> GetMessagesByGroupAsync(Guid groupId, string? searchText = null, int pageNumber = 1, int pageSize = 50)
        {
            var query = _context.Messages.AsQueryable();

            query = query.Where(m => m.GroupId == groupId && !m.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(m => m.Text != null && EF.Functions.ILike(m.Text, $"%{searchText}%"));
            }

            // Sayfalama
            query = query
                .OrderByDescending(m => m.SentAt) // En yeni mesajlar önce
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public Task<List<GroupUser>> GetGroupUsersAsync()
        {
            return _context.GroupUsers.ToListAsync();
        }

        public Task<List<JoinRequest>> GetJoinRequestsAsync()
        {
            return _context.GroupJoins.ToListAsync();
        }
    }
}
