using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public abstract class Account : IAccount
    {
        public abstract void Withdraw(long money);
        public void Put(long money)
        {
            Amount += money;
            CallEvent(new AccountEventArgs("Money successfully added", Amount, ClientId, AccountType.Unchoosed), Added);

        }
        public long Amount { get; set; }
        public int ClientId { get; set; }
        public Account()
        {
            Withdrawed += WithdawHandler;
            Added += AddHandler;
            Created += CreateHandler;
        }

        protected event AccountStateHandler Withdrawed;

        protected event AccountStateHandler Added;

        protected event AccountStateHandler Created;
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(e);
            }
        }
        protected virtual void OnCreate(AccountEventArgs e)
        {
            CallEvent(e, Created);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void onWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        #region eventHandlers
        public static void WithdawHandler(AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Your current money " + e.Amount);
        }
        public static void AddHandler(AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        public static void CreateHandler(AccountEventArgs e)
        {
            Console.WriteLine(e.Message);

        }
        #endregion
    }
}
