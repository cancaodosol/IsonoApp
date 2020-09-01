using IssWebRazorApp.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.LINENotify
{
    public class LINENotifyService
    {
        /// <summary>
        /// LINE Notify設定情報
        /// </summary>
        private readonly LINENotifyServiceConfig _config;

        /// <summary>
        /// アクセストークンID
        /// </summary>
        private readonly string _accessToken = "DgkzTWHWJoluPXaUcCsaKcIyfgZizXDeH1HXGeYwNOI";

        public LINENotifyService()
        {
        }

        /// <summary>
        /// メッセージの送付
        /// </summary>
        /// <param name="message"></param>
        public async void SendMessage(string message)
        {
            using (var client = new HttpClient())
            {
                // 通知するメッセージ
                var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "message", "\n" + message } });

                // ヘッダーにアクセストークンを追加
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);

                // 実行
                var result = await client.PostAsync("https://notify-api.line.me/api/notify", content);

                Console.WriteLine(result);
            }
        }
    }
}
