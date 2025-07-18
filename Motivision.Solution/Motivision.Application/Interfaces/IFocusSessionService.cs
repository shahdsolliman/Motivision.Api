using Motivision.Application.Features.FocusSessions.Commands;
using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Interfaces
{
    public interface IFocusSessionService 
    {
        Task<int> StartSessionAsync(CreateFocusSessionCommand command);
        Task<bool> EndSessionAsync(EndFocusSessionCommand command);
        Task<bool> UpdateSessionAsync(UpdateFocusSessionCommand command);
        Task<bool> DeleteSessionAsync(int sessionId);
        Task<List<FocusSession>> GetUserSessionsAsync(string userId);
        Task<FocusSession?> GetByIdAsync(int id, string userId);

    }
}
