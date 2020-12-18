using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if TELESHARP
using TLSharp.Core;
using TeleSharp.TL;
#else
using TgSharp.Core;
using TgSharp.TL;
using TgSharp.TL.Contacts;
#endif

namespace MyTelegramClient
{
    internal class TelegramHelper
    {
#region Fields
        private readonly TelegramClient _client;
#endregion Fields

#region Properties
        internal bool IsUserAuthorized => _client.IsUserAuthorized();
#endregion Properties

#region Constructors
        internal TelegramHelper()
        {
            var version = System.Reflection.Assembly.GetAssembly(typeof(FakeSessionStore)).GetName().Version;
            Console.WriteLine($"Telegram is starting with version {version}");
        }

        internal TelegramHelper(MyTelegramOptions options) : this()
        {
            var apiId = options.apiId;
            var apiHash = options.apiHash;
            _client = new TelegramClient(apiId, apiHash);
        }
#endregion Constructors

#region Methods
        internal async Task ConnectAsync()
        {
            await _client.ConnectAsync();
        }

        internal async Task<TLUser> LoginAsync(string phoneNumber, string hash, string code)
        {
            return await _client.MakeAuthAsync(phoneNumber, hash, code);
        }

        internal async Task<string> SendCodeRequestAsync(string phoneNumber)
        {
            return await _client.SendCodeRequestAsync(phoneNumber);
        }

        internal async Task<TLContacts> GetContactsAsync() => await _client.GetContactsAsync();
        #endregion Methods
    }

}