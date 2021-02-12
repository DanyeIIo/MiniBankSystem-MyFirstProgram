using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class RegularAccount : Account
    {
        public RegularAccount(long amount, int id) : base()
        {
            ClientId = id;
            Amount = amount;
            OnCreate(new AccountEventArgs("Money successfully added", Amount, ClientId, AccountType.Regular));

        }
        public RegularAccount(int id, long amount)
        {
            ClientId = id;
            Amount = amount;
        }
        public override void Withdraw(long money)
        {
            money = (money < 0) ? money * -1 : money;
            if (Amount - money >= 0)
            {
                Amount -= money;
                onWithdrawed(new AccountEventArgs($"Money Succesfully Withdrawed corrent money: {Amount}", Amount, ClientId, AccountType.Regular));
            }
        }
        public void Transfer(int recipient, long sendMoney)
        {
            if (Amount - sendMoney >= 0)
            {
                Amount -= sendMoney;
                onWithdrawed(new AccountEventArgs("Money succesfully Transferred", Amount, ClientId, AccountType.Regular));
                OnAdded(new AccountEventArgs($"Money Succesfully Accepted by user ID: {recipient} ", sendMoney, recipient, AccountType.Regular));
            }
            else
            {
                onWithdrawed(new AccountEventArgs("Your money is not enough to transfer", Amount, ClientId, AccountType.Regular));
            }
        }
    }
}
