using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#if TELESHARP
using TLSharp.Core;
using TeleSharp.TL;
#else
using TgSharp.Core;
using TgSharp.TL;
using TgSharp.TL.Contacts;
using TgSharp.TL.Messages;
#endif

namespace MyTelegramClient
{
    internal class TelegramWrapper
    {
        #region Fields
        private readonly TelegramClient _client;
        #endregion Fields

        #region Properties
        internal bool IsUserAuthorized => _client.IsUserAuthorized();
        #endregion Properties

        #region Constructors
        internal TelegramWrapper()
        {
            var version = System.Reflection.Assembly.GetAssembly(typeof(FakeSessionStore)).GetName().Version;
            Console.WriteLine($"Telegram is starting with version {version}");
        }

        internal TelegramWrapper(MyTelegramOptions options) : this()
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

        internal async Task<TLAbsDialogs> GetUserDialogsAsync(int offsetDate = 0, int offsetId = 0, TLAbsInputPeer offsetPeer = null, int limit = 100, CancellationToken token = default(CancellationToken))
        {
            return await _client.GetUserDialogsAsync(offsetDate, offsetId, offsetPeer, limit, token);
        }

        internal async Task<TLAbsMessages> GetHistoryAsync(TLAbsInputPeer peer, int offsetId = 0, int offsetDate = 0, int addOffset = 0, int limit = 100, int maxId = 0, int minId = 0, CancellationToken token = default(CancellationToken))
        {
            return await _client.GetHistoryAsync(peer, offsetId, offsetDate, addOffset, limit, maxId, minId, token);
        }
        #endregion Methods
    }

}