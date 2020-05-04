using IssWebRazorApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class FootballNoteRepository : IFootballNoteRepository
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        public FootballNoteRepository(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }
        public IList<FootballNoteData> FindAll() 
        {
            var datas = _context.FootballNoteData.ToList<FootballNoteData>();
            var userRepository = new UserRepository(_context);

            foreach (var data in datas)
            {
                data.CreateUserData = userRepository.Find(data.CreateUserId);
                data.LastUpdateUserData = userRepository.Find(data.LastUpdateUserId);

            }

            return datas;
        }
        public IList<FootballNoteData> FindUnit(int noteId)
        {
            var datas = _context.FootballNoteData.Where(m => m.NoteId == noteId || m.TargetNoteId == noteId).ToList<FootballNoteData>();
            var users = new UserRepository(_context).FindAll();

            foreach (var data in datas)
            {
                data.CreateUserData = users.FirstOrDefault(m => m.UserId == data.CreateUserId);
                data.LastUpdateUserData = users.FirstOrDefault(m => m.UserId == data.LastUpdateUserId);

            }

            return datas;
        }
        public FootballNoteData Find(int noteId) 
        {
            return _context.FootballNoteData.AsNoTracking().FirstOrDefault(m => m.NoteId == noteId);
        }

        public async Task AddAsync(FootballNoteData data)
        {
            _context.FootballNoteData.Add(data);
            await _context.SaveChangesAsync();
        }
    }
}
