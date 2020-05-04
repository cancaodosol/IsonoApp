using IssWebRazorApp.Data;
using IssWebRazorApp.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    [Serializable]
    public class User
    {


        private int _UserId ;

        public int UserId
        {
            get
            { return _UserId; }
            set
            { 
                if (_UserId == value)
                    return;
                _UserId = value;
            }
        }


        private UserName _UserName;

        public  UserName UserName
        {
            get
            { return _UserName; }
            set
            { 
                if (_UserName == value)
                    return;
                _UserName = value;
            }
        }

        private string _LoginId;

        public string LoginId
        {
            get
            { return _LoginId; }
            set
            { 
                if (_LoginId == value)
                    return;

                var id = value;

                // 入力文字制御
                // 数字とアルファベットに加えてハイフンとアンダースコアもOK
                if (Regex.IsMatch(id, @"[^a-zA-Z0-9-_]")) 
                {
                    throw new ISSModelException(nameof(User),nameof(LoginId),"UMLGIDE001","ログインIDには「数字とアルファベット、ハイフン(-)、アンダースコア(_)」以外使用できません。");
                }

                // 文字数制限
                // 15文字以下
                if (id.Length > 15)
                {
                    throw new ISSModelException(nameof(User), nameof(LoginId), "UMLGIDE002","ログインIDは15文字以下である必要があります。");
                }
                // 5文字以上
                if (id.Length < 5)
                {
                    throw new ISSModelException(nameof(User), nameof(LoginId), "UMLGIDE003","ログインIDは5文字以上である必要があります。");
                }

                _LoginId = value;
            }
        }

        private string _LoginPassword;

        public string LoginPassword
        {
            get
            { return _LoginPassword; }
            set
            { 
                if (_LoginPassword == value)
                    return;

                var pass = value;

                // 入力文字制御
                // 数字とアルファベットに加えてハイフンとアンダースコアもOK
                if (Regex.IsMatch(pass, @"[^a-zA-Z0-9-_@]"))
                {
                    throw new ISSModelException(nameof(User), nameof(LoginPassword), "UMLGPSE001", "パスワードには「数字とアルファベット、ハイフン(-)、アンダースコア(_)、アットマーク(@)」以外使用できません。");
                }

                // 文字数制限
                // 15文字以下
                if (pass.Length > 15)
                {
                    throw new ISSModelException(nameof(User), nameof(LoginPassword), "UMLGPSE002", "パスワードは15文字以下である必要があります。");
                }
                // 5文字以上
                if (pass.Length < 5)
                {
                    throw new ISSModelException(nameof(User), nameof(LoginPassword), "UMLGPSE003", "パスワードは5文字以上である必要があります。");
                }
                _LoginPassword = value;
            }
        }

        public int UniformNumber { get; set; }
        public Position Position { get; set; }
        public string UserType { get; set; }
        public string SystemRole { get; set; }
        public string Education { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public User(int id, int number)
        {
            UserId = id;
            UniformNumber = number;
        }
        public User(int id ,UserName name, string password, int uniformNumber, Position position, string userType, string systemRole, string education, double height, double weight) {
            UserId = id;
            UserName = name;
            LoginPassword = password;
            ChangeUniformNumber(uniformNumber);
            ChangePosition(position);
            ChangeUserType(userType);
            ChangeSystemRole(systemRole);
            ChangeEducation(education);
            ChangeHeight(height);
            ChangeWeight(weight);
        }
        public UserData ToData()
        {
            var data = new UserData();
            data.UserId = UserId;
            data.LoginId = LoginId;
            data.DisplayName = UserName.DisplayName;
            data.FirstNameKanji = UserName.FirstNameKanji;
            data.LastNameKanji = UserName.LastNameKanji;
            data.FirstNameRoman = UserName.FirstNameRoman;
            data.LastNameRoman = UserName.LastNameRoman;
            data.LoginPassword = LoginPassword;
            data.UniformNumber = UniformNumber;
            data.PositionId = Position.PositionId;
            data.PositionData = Position.ToData();
            data.UserType = UserType;
            data.SystemRole = SystemRole;
            data.Education = Education;
            data.Height = Height;
            data.Weight = Weight;
            return data;
        }
        public void ChangeUniformNumber(int uniformNumber) 
        {
            UniformNumber = uniformNumber;
        }
        public void ChangePosition(Position position) 
        {
            Position = position;
        }
        public void ChangeUserType(string userType) 
        {
            UserType = userType;
        }
        public void ChangeSystemRole(string role) 
        {
            SystemRole = role;
        }
        public void ChangeEducation(string education) 
        {
            Education = education;
        }
        public void ChangeHeight(double height)
        {
            Height = height;
        }
        public void ChangeWeight(double weight)
        {
            Weight = weight;
        }
    }
}
