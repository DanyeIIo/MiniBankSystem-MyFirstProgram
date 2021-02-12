using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class Client
    {
        protected event ClientStateHandler OnCreated;
        protected event ClientStateHandler OnRegular;
        protected event ClientStateHandler OnCredit;

        public string Name { get; set; }
        public int Id { get; set; }
        public RegularAccount regular;
        public CreditAccount credit;
        public Client(string name, int id)
        {
            OnCreated += CreateHandler;
            OnRegular += CreateRegularHandler;
            OnCredit += CreateCreditHandler;
            Name = name;
            Id = id;
            CreateAccount(new ClientEventArgs($"{name}'s Account has been created ", name, Id));
        }
        public Client(int id , string name)
        {

            Name = name;
            Id = id;
            OnCreated += CreateHandler;
            OnRegular += CreateRegularHandler;
            OnCredit += CreateCreditHandler;
        }
        private void CallEvent(ClientEventArgs e, ClientStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(e);
            }
        }
        public void CreateAccount(ClientEventArgs client)
        {
            CallEvent(client, OnCreated);
        }
        public void CreateRegularAcc(ClientEventArgs client)
        {
            CallEvent(client, OnRegular);
        }
        public void CreateCreditAcc(ClientEventArgs client)
        {
            CallEvent(client, OnCredit);
        }
        public void Regular(int id, long amount, params ClientStateHandler[] handlers)
        {
            regular = new RegularAccount(amount, id);
            CreateRegularAcc(new ClientEventArgs("Regular account was create", Name, id));
        }
        public void Credit(int id, long amount, params ClientStateHandler[] handlers)
        {
            credit = new CreditAccount(amount, id);
            CreateCreditAcc(new ClientEventArgs("Credit account was create", Name, id));
        }
        #region Handlers
        private static void CreateHandler(ClientEventArgs e)
        {
            Console.WriteLine($"{e.Message} Id of account {e.Id}");
        }
        private static void CreateCreditHandler(ClientEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void CreateRegularHandler(ClientEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        #endregion
    }
}
