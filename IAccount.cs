using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    interface IAccount
    {
        public void Put(long money);
        public void Withdraw(long money);
    }

}
