using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public delegate void AccountStateHandler(AccountEventArgs e);
    public class AccountEventArgs
    {
        public string Message { get; set; }
        public long Amount { get; set; }
        public int ClientId { get; set; }
        internal AccountEventArgs(string message, long amount, int clientId,AccountType type)
        {
            Message = message;
            Amount = amount;
            ClientId = clientId;
            Parser parser = new Parser(clientId, amount, type);
        }
    }
}