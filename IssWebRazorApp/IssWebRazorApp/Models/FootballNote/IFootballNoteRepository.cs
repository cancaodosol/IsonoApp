using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IFootballNoteRepository
    {
        public IList<FootballNoteData> FindAll();
        public IList<FootballNoteData> FindUnit(int noteId);
        public FootballNoteData Find(int noteId);
        public Task AddAsync(FootballNoteData data);
    }
}
