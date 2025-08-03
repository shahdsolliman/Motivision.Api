using Motivision.Core.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Motivision.Infrastructure.Persistence;
using Motivision.Core.Contracts.Services;
using Motivision.Core.Business.Enums;
using Motivision.Core.Contracts;
using Motivision.Core.Business.Specifications.FocusSessionSpecs;

namespace Motivision.Application.Services
{
    public class FocusSessionService : IFocusSessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FocusSessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FocusSession> CreateSessionAsync(FocusSession session)
        {
            await _unitOfWork.Repository<FocusSession>().AddAsync(session);
            await _unitOfWork.CompleteAsync();
            return session;
        }

        public async Task<bool> StartSessionAsync(int sessionId, string userId)
        {
            var session = await _unitOfWork.Repository<FocusSession>().GetAsync(sessionId);
            if (session == null || session.UserId != userId || session.StartTime != null)
                return false;

            session.StartTime = DateTime.UtcNow;
            session.SessionStatus = SessionStatus.InProgress;

            _unitOfWork.Repository<FocusSession>().Update(session);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> EndSessionAsync(int sessionId, string userId)
        {
            var session = await _unitOfWork.Repository<FocusSession>().GetAsync(sessionId);
            if (session == null || session.UserId != userId || session.StartTime == null)
                return false;

            session.EndTime = DateTime.UtcNow;
            session.SessionStatus = SessionStatus.Completed;

            if (session.StartTime.HasValue)
            {
                var duration = session.EndTime.Value - session.StartTime.Value;
                session.Notes = (session.Notes ?? "") + $"\nDuration: {duration.TotalMinutes} minutes.";
            }

            _unitOfWork.Repository<FocusSession>().Update(session);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateSessionAsync(FocusSession session, string userId)
        {
            var entity = await _unitOfWork.Repository<FocusSession>().GetAsync(session.Id);
            if (entity == null || entity.UserId != userId)
                return false;

            entity.Title = session.Title ?? entity.Title;
            entity.Description = session.Description ?? entity.Description;
            entity.Notes = session.Notes ?? entity.Notes;

            _unitOfWork.Repository<FocusSession>().Update(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteSessionAsync(int sessionId, string userId)
        {
            var session = await _unitOfWork.Repository<FocusSession>().GetAsync(sessionId);
            if (session == null || session.UserId != userId)
                return false;

            _unitOfWork.Repository<FocusSession>().Delete(session);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<FocusSession?> GetByIdAsync(int id, string userId)
        {
            var session = await _unitOfWork.Repository<FocusSession>().GetAsync(id);
            if (session == null || session.UserId != userId)
                return null;

            return session;
        }

        public async Task<IReadOnlyList<FocusSession>> GetAllSessionsAsync(string userId)
        {
            var spec = new FocusSessionByUserSpecification(userId);
            return await _unitOfWork.Repository<FocusSession>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<FocusSession>> GetSessionsByDateAsync(string userId, DateTime from, DateTime to)
        {
            var spec = new FocusSessionByUserAndDateRangeSpecification(userId, from, to);
            return await _unitOfWork.Repository<FocusSession>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<FocusSession>> GetSessionsWithPaginationAsync(string userId, int skip, int take, string? sort = null)
        {
            var spec = new FocusSessionWithFiltersForPaginationSpecification(userId, skip, take, sort);
            return await _unitOfWork.Repository<FocusSession>().ListAsync(spec);
        }

        public async Task<int> CalculateUserStreakAsync(string userId)
        {
            var allSessions = await _unitOfWork.Repository<FocusSession>().GetAllAsync();
            var userSessions = allSessions
                .Where(s => s.UserId == userId && s.SessionStatus == SessionStatus.Completed && s.EndTime.HasValue)
                .OrderByDescending(s => s.EndTime!.Value.Date)
                .Select(s => s.EndTime!.Value.Date)
                .Distinct()
                .ToList();

            int streak = 0;
            DateTime currentDay = DateTime.UtcNow.Date;

            foreach (var day in userSessions)
            {
                if (day == currentDay)
                {
                    streak++;
                    currentDay = currentDay.AddDays(-1);
                }
                else break;
            }

            return streak;
        }
    }
}
