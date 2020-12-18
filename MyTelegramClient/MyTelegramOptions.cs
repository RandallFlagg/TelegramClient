using System;

namespace MyTelegramClient
{
    /// <summary>
    /// A sample JSON file created by this record:
    /// {
    ///     "MyTelegramOptions": {
    ///         "apiHash": "",
    ///         "apiId": 0,
	///         "phoneNumber": ""
    ///     }
    /// }
    /// </summary>
    public record MyTelegramOptions(string apiHash = default, int apiId = default, string phoneNumber = default);
}