using Real_ChatApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Interfaces
{
    public interface IWithRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<List<Message>> GetMessagesAsync();
        Task<List<Group>> GetGroupsAsync();
        Task<List<GroupUser>> GetGroupUsersAsync();
        Task<List<JoinRequest>> GetJoinRequestsAsync();
        Task<List<Message>> GetMessagesByGroupAsync(Guid groupId, string? searchText = null, int pageNumber = 1, int pageSize = 50);

    }
}
