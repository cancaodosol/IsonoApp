using IssWebRazorApp.Data;
using IssWebRazorApp.Models.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class FootballNote
    {

        private int _NoteId;

        public int NoteId
        {
            get
            { return _NoteId; }
        }

        private Context _Context;

        public Context Context
        {
            get
            { return _Context; }
            set
            { 
                if (_Context == value)
                    return;
                _Context = value;
            }
        }

        private UploadFile _Photo;

        public UploadFile Photo
        {
            get
            { return _Photo; }
            set
            { 
                if (_Photo == value)
                    return;
                _Photo = value;
            }
        }

        private Editor _Editor;

        public Editor Editor
        {
            get
            { return _Editor; }
            set
            { 
                if (_Editor == value)
                    return;
                _Editor = value;
            }
        }

        private NoteTarget _NoteTarget;
        private FootballNotePostRequestModel noteModel;
        private User createUser;

        public NoteTarget NoteTarget
        {
            get
            { return _NoteTarget; }
            set
            { 
                if (_NoteTarget == value)
                    return;
                _NoteTarget = value;
            }
        }

        public NoteType NoteType
        {
            get
            { 
                if(_NoteTarget.NoteId == 0) return NoteType.Parent;
                return NoteType.Child;
            }
        }
        public FootballNote(FootballNoteData data)
        {
            _NoteId = data.NoteId;
            _Context = new Context(data.Title, data.Context);
            _Editor = new Editor(data.CreateUserData.ToModel(),data.CreateDate, data.LastUpdateUserData.ToModel(), data.LastUpdateDate);
            _NoteTarget = new NoteTarget()
            {
                NoteId = data.TargetNoteId,
                Session = data.TargetSession,
                CategoryCode = data.TargetCategoryCode,
                PlaybookId = data.TargetPlaybookId,
                ScheduleId = data.TargetScheduleId
            };
        }
        public FootballNote(NoteType type,Context context, IFormFile file, User createUser,NoteTarget target)
        {
            Context = context;
            Photo = new UploadFile(file);
            Editor = new Editor(createUser, DateTime.Now);
            NoteTarget = target;
        }

        public FootballNote(FootballNotePostRequestModel noteModel, User createUser)
        {
            Context = new Context(noteModel.Title, noteModel.Context);
            Editor = new Editor(createUser, DateTime.Now, createUser, DateTime.Now);
            NoteTarget = new NoteTarget()
            {
                NoteId = noteModel.TargetNoteId,
                Session = noteModel.TargetSession,
                CategoryCode = noteModel.TargetCategoryCode,
                PlaybookId = noteModel.TargetPlaybookId,
                ScheduleId = noteModel.TargetScheduleId
            };
            Photo = new UploadFile("");
        }
        public FootballNoteData ToData()
        {
            var data = new FootballNoteData();
            data.NoteId = NoteId;
            data.Title = Context.Title;
            data.Context = Context.Text;
            data.PhotoUrl = Photo.Url;
            data.CreateUserId = Editor.CreateUser.UserId;
            data.CreateDate = Editor.CreateDate;
            data.LastUpdateUserId = Editor.LastUpdateUser.UserId;
            data.LastUpdateDate = Editor.LastUpdateDate;
            data.TargetNoteId = NoteTarget.NoteId;
            data.TargetSession = NoteTarget.Session;
            data.TargetCategoryCode = NoteTarget.CategoryCode;
            data.TargetPlaybookId = NoteTarget.PlaybookId;
            data.TargetPositionId = NoteTarget.PositionId;
            data.TargetScheduleId = NoteTarget.ScheduleId;
            return data;
        }
    }
    public enum NoteType
    {
        Parent,
        Child
    }
}
