using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Contracts.Services
{
    public interface IFocusSessionService 
    {

        Task<FocusSession> CreateSessionAsync(FocusSession session);
        Task<bool> StartSessionAsync(int sessionId, string userId);
        Task<bool> EndSessionAsync(int sessionId, string userId);
        Task<bool> DeleteSessionAsync(int sessionId, string userId);
        Task<bool> UpdateSessionAsync(FocusSession session, string userId);

        Task<IReadOnlyList<FocusSession>> GetAllSessionsAsync(string userId);
        Task<FocusSession?> GetByIdAsync(int id, string userId);

        Task<IReadOnlyList<FocusSession>> GetSessionsByDateAsync(string userId, DateTime from, DateTime to);
        Task<int> CalculateUserStreakAsync(string userId);
        Task<IReadOnlyList<FocusSession>> GetSessionsWithPaginationAsync(string userId, int skip, int take, string? sort);



    }
}
