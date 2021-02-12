using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class CreditAccount : Account
    {
        public int MaxAllowedDept { get; set; } = -1000000;
        public CreditAccount(long amount, int id) : base()
        {
            ClientId = id;
            Amount+= amount;
            OnCreate(new AccountEventArgs("Money successfully added", Amount, ClientId , AccountType.Credit));
        }
        public CreditAccount(int id, long amount)
        {
            ClientId = id;
            Amount = amount;
        }
        public override void Withdraw(long money)
        {
            money = (money < 0) ? money * -1 : money;
            if (Amount - money >= MaxAllowedDept)
            {
                Amount -= money;
                onWithdrawed(new AccountEventArgs($"Money Succesfully Withdrawed corrent money: {Amount}", Amount, ClientId, AccountType.Credit));
            }
            else
            {
                onWithdrawed(new AccountEventArgs($"Not enough money Current amount: {Amount}", Amount, ClientId, AccountType.Credit));
            }
        }

    }
}
