using System;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using Terminal.Gui;

namespace TelegramClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run<TelegramTerminalClient>();
            //StartClient();
            //Console.WriteLine("Press any key to exit");
            //Console.ReadLine();
        }

        //using Terminal.Gui;



    private static async Task StartClient()
        {
            try
            {
                var apiId = ;
                var apiHash = "";
                Console.WriteLine("Hello World!");
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
                var client = new TLSharp.Core.TelegramClient(apiId, apiHash);
                await client.ConnectAsync();
                if (client.Session == null)
                {
                    await TLConnect(client);
                }

                // this is because the contacts in the address come without the "+" prefix
                string NumberToSendMessage = "+";
                var normalizedNumber = NumberToSendMessage.StartsWith("+") ?
                    NumberToSendMessage.Substring(1, NumberToSendMessage.Length - 1) :
                    NumberToSendMessage;

                var result = await client.GetContactsAsync();

                var user = result.Users
                    .OfType<TLUser>()
                    .FirstOrDefault(x => (x.Phone == normalizedNumber));

                if (user == null)
                {
                    throw new System.Exception("Number was not found in Contacts List of user: " + NumberToSendMessage);
                }

                var a = await client.SendMessageAsync(new TLInputPeerUser { UserId = user.Id }, "TEST2");
                int x = 5;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        private static async Task TLConnect(TLSharp.Core.TelegramClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var hash = await client.SendCodeRequestAsync("+");
            var code = ""; // you can change code in debugger //TODO: Cahnge this to Console.ReadLine

            var user = await client.MakeAuthAsync("+", hash, code);
        }
    }
}