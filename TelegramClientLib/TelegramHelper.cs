using System;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;

namespace TelegramClientLib
{
    public class TelegramHelper
    {
        private const int API_ID = ;
        private const string API_HASH = ;

        private readonly TLSharp.Core.TelegramClient _client;
        private string _phoneNumber;
        private string _hash;

        public bool IsConnected => _client.Session != null;

        public TelegramHelper()
        {
            _client = new TLSharp.Core.TelegramClient(API_ID, API_HASH);
            _phoneNumber = default!;
            _hash = default!;
        }

        public void StartClient()
        {
            _client.ConnectAsync().Wait();
            //var session = new Session
            //{
            //    AuthKey = ,
            //    Id = ,
            //    LastMessageId = ,
            //    Salt = ,
            //    Sequence = ,
            //    SessionExpires = ,
            //    TimeOffset = ,
            //    TLUser = 
            //}
        }

        public async Task<TeleSharp.TL.Contacts.TLContacts> GetContacts()
        {
            try
            {
                var result = await _client.GetContactsAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; //TODO: Change behaviour
            }
        }

        public async Task SendMessage(int userId, string message) {
            // this is because the contacts in the address come without the "+" prefix
            //string NumberToSendMessage = "+972504443975";
            //var normalizedNumber = phoneNumber.StartsWith("+")
            //? phoneNumber.Substring(1, phoneNumber.Length - 1)
            //: phoneNumber;

            //var user = result.Users
            //    .OfType<TLUser>()
            //    .FirstOrDefault(x => (x.Phone == normalizedNumber));

            //if (user == null)
            //{
            //    throw new System.Exception("Number was not found in Contacts List of user: " + _phoneNumber);
            //}

            var a = await _client.SendMessageAsync(new TLInputPeerUser { UserId = userId }, message);
            //int x = 5;
        }

        public async Task SendCodeRequest(string phoneNumber) //TODO: Change to private
        {
            //if (_client == null)
            //{
            //    throw new ArgumentNullException(nameof(_client));
            //}
            _phoneNumber = phoneNumber;
            _hash = await _client.SendCodeRequestAsync(_phoneNumber);
        }

        public async Task Authenticate(string code)
        {
            //var hash = "43b4108f3144c3b325";
            //var code = "61985"; // you can change code in debugger //TODO: Cahnge this to Console.ReadLine

            var user = await _client.MakeAuthAsync(_phoneNumber, _hash, code);
        }
    }
}