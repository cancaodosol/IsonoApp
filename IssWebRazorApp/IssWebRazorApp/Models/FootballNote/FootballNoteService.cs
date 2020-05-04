using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class FootballNoteService : IFootballNoteService
    {
        private readonly IFootballNoteRepository _footballNoteRepository;
        public FootballNoteService(IFootballNoteRepository footballNoteRepository)
        {
            _footballNoteRepository = footballNoteRepository;
        }

        public List<FootballNoteUnit> FindAllUnits() 
        {
            var notes = _footballNoteRepository.FindAll().Select(m => m.ToModel()).ToList<FootballNote>();
            var units = notes.Where(m => m.NoteType == NoteType.Parent).Select(m => new FootballNoteUnit(m)).ToList<FootballNoteUnit>();

            foreach (var unit in units)
            {
                unit.ChildrenNotes = notes.Where(m => m.NoteType == NoteType.Child && m.NoteTarget.NoteId == unit.ParentNote.NoteId).OrderBy(_=>_.Editor.CreateDate).ToList<FootballNote>();
            }

            return units;
        }
        public FootballNoteUnit FindUnit(int noteId) 
        {
            var datas = _footballNoteRepository.FindUnit(noteId);
            var notes = datas.Select(m => m.ToModel()).ToList<FootballNote>();
            var unit = new FootballNoteUnit(notes.FirstOrDefault(m => m.NoteType == NoteType.Parent));
            unit.ChildrenNotes = notes.Where(m => m.NoteType == NoteType.Child).OrderBy(n => n.Editor.CreateDate).ToList<FootballNote>();
            return unit;
        }
        public FootballNote Find(int noteId) 
        {
            return null;
        }

        public async Task AddAsync(FootballNote note) 
        {
            var data = note.ToData();
            await _footballNoteRepository.AddAsync(data);
            return;
        }
    }
}
