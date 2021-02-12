using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Exam
{
    public class Parser
    {
        private int id;
        private string name;
        private long amount;
        public Parser(ref List<Client> clients)
        {
            XmlToCollection(ref clients);
        }
        public Parser(string name, int id)
        {
            this.name = name;
            this.id = id;
            AddXml();
        }
        public Parser(int id, long amount, AccountType type)
        {
            this.id = id;
            this.amount = amount;
            WorkWithXml(type);
        }
        private void AddXml()
        {
            bool toggle = false;
            try
            {
                XDocument xdoc = XDocument.Load("XML.xml");
                XElement root = xdoc.Element("clients");
                foreach (XElement item in root.Elements("client").ToList())
                {
                    if (item.Attribute("ID").Value == id.ToString())
                    {
                        toggle = true;
                    }
                }
                if (toggle == true)
                    return;
                XElement client = new XElement("client");
                XAttribute clientId = new XAttribute("ID", id.ToString());
                XElement xname = new XElement("name", name);
                XElement regularAcc = new XElement("regular", "false");
                XElement creditAcc = new XElement("credit", "false");
                XAttribute amount = new XAttribute("amount", -1);
                regularAcc.Add(amount);
                creditAcc.Add(amount);
                client.Add(clientId);
                client.Add(xname);
                client.Add(regularAcc);
                client.Add(creditAcc);
                XElement Root = xdoc.Element("clients");
                
                    Root.Add(client);
                xdoc.Save("XML.xml");
            }
            catch 
            {
                XDocument xdoc = new XDocument();
                XElement client = new XElement("client");
                XAttribute clientId = new XAttribute("ID", id.ToString());
                XElement xname = new XElement("name", name);
                XElement regularAcc = new XElement("regular", "false");
                XElement creditAcc = new XElement("credit", "false");
                XAttribute amount = new XAttribute("amount", -1);
                regularAcc.Add(amount);
                creditAcc.Add(amount);
                client.Add(clientId);
                client.Add(xname);
                client.Add(regularAcc);
                client.Add(creditAcc);
                XElement clients = new XElement("clients");
                clients.Add(client);
                xdoc.Add(clients);
                xdoc.Save("XML.xml");
            }
        }
        private void WorkWithXml(AccountType type)
        {
            try
            {
                XDocument xdoc = XDocument.Load("XML.xml");
                XElement root = xdoc.Element("clients");
                switch (type)
                {
                    case AccountType.Credit:
                        {
                            foreach (XElement item in root.Elements("client").ToList())
                            {
                                if (item.Attribute("ID").Value == id.ToString())
                                {

                                    item.Element("credit").Attribute("amount").Value = amount.ToString();
                                    item.Element("credit").Value = "true";
                                }
                            }
                            break;
                        }
                    case AccountType.Regular:
                        {
                            foreach (XElement item in root.Elements("client").ToList())
                            {
                                if (item.Attribute("ID").Value == id.ToString())
                                {

                                    item.Element("regular").Attribute("amount").Value =
                                        (Convert.ToInt64(item.Element("regular").Attribute("amount").Value.ToString())
                                        + amount).ToString();
                                    item.Element("regular").Value = "true";
                                }
                            }
                            break;
                        }
                }
                xdoc.Save("XML.xml");
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void XmlToCollection(ref List<Client> clients)
        {
            try
            {
                XDocument xdoc = XDocument.Load("XML.xml");
                XElement root = xdoc.Element("clients");
                foreach (var item in root.Elements("client").ToList())
                {
                    id = Convert.ToInt32(item.Attribute("ID").Value);
                    name = item.Element("name").Value;
                    clients.Add(new Client(id, name));
                    if (item.Element("regular").Value == "true")
                    {
                        amount = Convert.ToInt32(item.Element("regular").Attribute("amount").Value);
                        clients[clients.Count - 1].regular = new RegularAccount(id, amount);
                    }
                    if (item.Element("credit").Value == "true")
                    {
                        amount = Convert.ToInt32(item.Element("credit").Attribute("amount").Value);
                        clients[clients.Count - 1].credit = new CreditAccount(id, amount);
                    }
                }
            }
            catch
            {
            }
        }
    }
}