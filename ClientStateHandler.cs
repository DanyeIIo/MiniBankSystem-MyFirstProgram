using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public delegate void ClientStateHandler(ClientEventArgs eventArgs);
    public class ClientEventArgs
    {
        public string Message { get; set; }
        public string ClientName { get; set; }
        public int Id { get; set; }
        internal ClientEventArgs(string message, string clientName, int id)
        {
            Message = message;
            ClientName = clientName;
            Id = id;
            Parser parser = new Parser(clientName, id);
        }
        internal ClientEventArgs(string message)
        {
            Message = message;
        }
    }
}
