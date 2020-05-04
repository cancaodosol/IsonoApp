using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IFootballNoteService
    {
        public List<FootballNoteUnit> FindAllUnits();
        public FootballNoteUnit FindUnit(int noteId);
        public FootballNote Find(int noteId);
        public Task AddAsync(FootballNote note);
    }
}
