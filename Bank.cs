using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public enum AccountType
    {
        Regular,
        Credit,
        Unchoosed,
    }
    class Bank
    {
        List<Client> clients = new List<Client>();
        public void Registration()
        {

            Parser parser = new Parser(ref clients);
            int currentId = -1;
            long money = 0;
            bool toggle = true;
            while (true)
            {
                bool IsOn = true;
                Console.WriteLine("1: Create account, 2: Join, 0: Leave");
                uint.TryParse(Console.ReadLine(), out uint choose);
                if (choose == 1)
                {
                    AccountType type = new AccountType();
                    Console.WriteLine("Input name");
                    Console.Clear();
                    string name = Console.ReadLine();
                    if (clients.Count == 0)
                    {
                        clients.Add(new Client(name, 1));
                        currentId = 1;
                    }
                    else
                    {
                        clients.Add(new Client(name, clients.Count + 1));
                        currentId = clients.Count;
                    }
                    Console.WriteLine("Input account type:\n1. Credit, 2. Regular , 0. Left");
                    byte.TryParse(Console.ReadLine(), out byte result);
                    if (result == 1)
                        type = AccountType.Credit;

                    if (result == 2)
                        type = AccountType.Regular;

                    CreateAccount(type, currentId);
                }
                else if (choose == 2)
                {
                    Console.WriteLine("Input id to join");
                    while (currentId > clients.Count || currentId < 0)
                    {
                        Int32.TryParse(Console.ReadLine(), out currentId);
                        if (clients.Count == 0)
                        {
                            Console.WriteLine("No accounts have been created yet");
                            IsOn = false;
                            break;
                        }
                        else if (currentId > clients.Count)
                        {

                            Console.WriteLine("Uncorrect id");
                            Console.WriteLine("Press enter and try again");
                            Console.ReadKey();
                            Console.Clear();

                        }
                    }
                    while (IsOn)
                    {
                        bool isWork = true;
                        Console.WriteLine("1: Credit card\t2: Regular card\t3: Leave out");
                        uint.TryParse(Console.ReadLine(), out choose);
                        if (choose == 1)
                        {
                            
                            while (isWork)
                            {
                                if (clients[currentId - 1].credit != null)
                                {
                                    toggle = true;
                                    while (toggle == true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("1: Input money\n2: Withdraw money\n0: Leave");
                                        uint.TryParse(Console.ReadLine(), out choose);
                                        toggle = true;
                                        switch (choose)
                                        {
                                            case 1:
                                                {
                                                    Console.Clear();
                                                    Console.Write("Input count of money to add-> ");
                                                    long.TryParse(Console.ReadLine(), out money);
                                                    clients[currentId - 1].credit.Put(money);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    Console.Clear();
                                                    Console.Write("Input count of money to withdraw->");
                                                    long.TryParse(Console.ReadLine(), out money);
                                                    clients[currentId - 1].credit.Withdraw(money);
                                                    break;
                                                }
                                            case 0:
                                                {
                                                    toggle = false;
                                                    isWork = false;
                                                    break;
                                                }
                                        }
                                        Console.ReadKey();
                                    }
                                }
                                else
                                {
                                    byte choise = 2;
                                    while (choise != 0 || choise != 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Account wasn't create");
                                        Console.WriteLine("1: Create a credit account");
                                        Console.WriteLine("0: Leave out");
                                        byte.TryParse(Console.ReadLine(), out choise);
                                        if (choise == 1)
                                        {
                                            CreateAccount(AccountType.Credit, currentId);
                                            break;
                                        }
                                        else if (choise == 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (choose == 2)
                        {
                            while (isWork)
                            {
                                Console.Clear();
                                if (clients[currentId - 1].regular != null)
                                {
                                    toggle = true;
                                    while (toggle == true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("1: Transfer money by ID\n2: Input money\n3: Withdraw money\n0: Leave");
                                        uint.TryParse(Console.ReadLine(), out choose);
                                        switch (choose)
                                        {
                                            case 1:
                                                {
                                                    Console.WriteLine("Enter the ID of the card to which you want to transfer money");
                                                    Int32.TryParse(Console.ReadLine(), out int idToTransfer);
                                                    Console.WriteLine("Enter the count of money!");
                                                    long.TryParse(Console.ReadLine(), out long moneyToTrunsfer);
                                                    clients[currentId - 1].regular.Transfer(idToTransfer, moneyToTrunsfer);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    Console.WriteLine("Input count of money");
                                                    long.TryParse(Console.ReadLine(), out money);
                                                    clients[currentId - 1].regular.Put(money);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    Console.WriteLine("Input count of money to withdraw");
                                                    long.TryParse(Console.ReadLine(), out money);
                                                    clients[currentId - 1].regular.Withdraw(money);
                                                    break;
                                                }
                                            case 0:
                                                {
                                                    toggle = false;
                                                    isWork = false;
                                                    break;
                                                }
                                        }
                                        Console.ReadKey();
                                    }
                                }
                                else
                                {
                                    byte choise = 2;
                                    while (choise != 0 || choise != 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Account wasn't create");
                                        Console.WriteLine("1: Create a regular account");
                                        Console.WriteLine("0: Leave out");
                                        byte.TryParse(Console.ReadLine(), out choise);
                                        if (choise == 1)
                                        {
                                            CreateAccount(AccountType.Regular, currentId);
                                            break;
                                        }
                                        else if (choise == 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                                Console.ReadKey();
                            }
                        }
                        if (choose == 3) break;
                    }
                }
                else if (choose == 0)
                {
                    Console.WriteLine("See ya moron!");
                    break;
                }
            }
        }
        private void CreateAccount(AccountType type, int currentId)
        {
            switch (type)
            {
                case AccountType.Credit:
                    {

                        if (clients[currentId - 1].credit == null)
                        {
                            Console.WriteLine("Input start amount");
                            clients[currentId - 1].Credit(currentId, Convert.ToInt32(Console.ReadLine()));
                        }
                        break;
                    }
                case AccountType.Regular:
                    {
                        if (clients[currentId - 1].regular == null)
                        {
                            Console.WriteLine("Input start amount");
                            clients[currentId - 1].Regular(currentId, Convert.ToInt32(Console.ReadLine()));
                        }
                        break;
                    }
            }
            Console.Read();
        }
    }
}
