using IssWebRazorApp.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    [Serializable]
    public class UserName
    {
        private Encoding Enc;

        private string _DisplayName;

        public string DisplayName
        {
            get
            { return _DisplayName; }
            set
            { 
                if (_DisplayName == value)
                    return;

                //必須項目チェック
                if (String.IsNullOrWhiteSpace(value)) 
                {
                    throw new ISSModelException(nameof(UserName),nameof(DisplayName), "UNDPNME001", "表示名は必須入力項目です。");
                }

                var name = value.Trim();
                //文字数チェック
                if (Enc.GetByteCount(name) > 7)
                {
                    throw new ISSModelException(nameof(UserName), nameof(DisplayName), "UNDPNME002", "表示名は半角7文字以下である必要があります。");
                }

                _DisplayName = name;
            }
        }

        private string _FirstNameKanji;

        public string FirstNameKanji
        {
               get
            { return _FirstNameKanji; }
            set
            { 
                if (_FirstNameKanji == value)
                    return; 

                //必須項目チェック
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ISSModelException(nameof(UserName), nameof(FirstNameKanji), "UNFNKJE001", "名前（漢字）は必須入力項目です。");
                }

                var name = value.Trim();
                //全角文字チェック
                if (Enc.GetByteCount(name) != name.Length * 2)
                {
                    throw new ISSModelException(nameof(UserName), nameof(FirstNameKanji), "UNFNKJE002", "名前（漢字）は全角文字である必要があります。");
                }
                //文字数チェック
                if (name.Length > 6)
                {
                    throw new ISSModelException(nameof(UserName), nameof(FirstNameKanji), "UNFNKJE003", "名前（漢字）が長すぎます。");
                }
                _FirstNameKanji = name;
            }
        }

        private string _LastNameKanji;

        public string LastNameKanji
        {
            get
            { return _LastNameKanji; }
            set
            { 
                if (_LastNameKanji == value)
                    return;

                //必須項目チェック
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ISSModelException(nameof(UserName), nameof(LastNameKanji), "UNLNKJE001", "名前（漢字）は必須入力項目です。");
                }

                var name = value.Trim();
                //全角文字チェック
                if (Enc.GetByteCount(name) != name.Length * 2)
                {
                    throw new ISSModelException(nameof(UserName), nameof(LastNameKanji), "UNLNKJE002", "名前（漢字）は全角文字である必要があります。");
                }
                //文字数チェック
                if (name.Length > 6)
                {
                    throw new ISSModelException(nameof(UserName), nameof(LastNameKanji), "UNLNKJE003", "名前（漢字）が長すぎます。");
                }
                _LastNameKanji = name;
            }
        }
        public string NameKanji { get { return _LastNameKanji + " " + _FirstNameKanji; } }

        private string _FirstNameRoman;

        public string FirstNameRoman
        {
            get
            { return _FirstNameRoman; }
            set
            { 
                if (_FirstNameRoman == value)
                    return;

                //必須項目チェック
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ISSModelException(nameof(UserName), nameof(FirstNameRoman), "UNFNRME001", "名前（ローマ字）は必須入力項目です。");
                }

                var name = value.Trim();
                //制御文字チェック
                if (Regex.IsMatch(name,@"[^a-zA-Z]"))
                {
                    throw new ISSModelException(nameof(UserName), nameof(FirstNameRoman), "UNFNRME002", "名前（ローマ字）はアルファベットのみである必要があります。");
                }
                //文字数チェック
                if (name.Length > 15)
                {
                    throw new ISSModelException(nameof(UserName), nameof(FirstNameRoman), "UNFNRME003", "名前（ローマ字）が長すぎます。");
                }
                _FirstNameRoman = name;
            }
        }


        private string _LastNameRoman;

        public string LastNameRoman
        {
            get
            { return _LastNameRoman; }
            set
            { 
                if (_LastNameRoman == value)
                    return;

                //必須項目チェック
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ISSModelException(nameof(UserName), nameof(LastNameRoman), "UNLNRME001", "名前（ローマ字）は必須入力項目です。");
                }

                var name = value.Trim();
                //制御文字チェック
                if (Regex.IsMatch(name, @"[^a-zA-Z]"))
                {
                    throw new ISSModelException(nameof(UserName), nameof(LastNameRoman), "UNLNRME002", "名前（ローマ字）はアルファベットのみである必要があります。");
                }

                //文字数チェック
                if (name.Length > 15)
                {
                    throw new ISSModelException(nameof(UserName), nameof(LastNameRoman), "UNLNRME003", "名前（ローマ字）が長すぎます。");
                }
                _LastNameRoman = name;
            }
        }

        public string NameRoman { get { return _FirstNameRoman + " " + _LastNameRoman; } }

        public UserName(string displayName,string firstNameKanji,string lastNameKanji,string firstNameRoman,string lastNameRoman) 
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Enc = Encoding.GetEncoding("Shift_JIS");
            DisplayName = displayName;
            FirstNameKanji = firstNameKanji;
            LastNameKanji = lastNameKanji;
            FirstNameRoman = firstNameRoman;
            LastNameRoman = lastNameRoman;
        }
    }
}
