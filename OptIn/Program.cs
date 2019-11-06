using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OptIn.Model;
using OptIn.Repository;

namespace OptIn
{
    class Program
    {
        // teixeira.carlos@microsoft.com

        const int MAX_ACCOUNTS = 30;
        const int MAX_EVENTS = 10;

        static void Main(string[] args)
        {
            var datetime = DateTime.Now;
            Console.WriteLine($"Start time: {DateTime.Now.ToShortTimeString()}");

            Create();
            //Find();

            var elapsedTime = DateTime.Now - datetime;

            Console.WriteLine($"End time: {DateTime.Now.ToShortTimeString()}");
            Console.WriteLine($"Elapsed Time: {elapsedTime.ToString() }");

            Console.WriteLine("Fim");
            Console.ReadKey();
        }

        static void Find()
        {
            var repository = new AccountRepository();

            var startDate = new DateTime(2019, 10, 29, 0, 0, 0);
            var endDate = new DateTime(2019, 10, 29, 23, 59, 59);
            
            var accounts = repository
                .SearchFor(s => s.Events.Where(a => a.TransactionDate >= startDate && a.TransactionDate <= endDate).Take(1).Any() )
                .ToList() ;

            accounts.ToList().ForEach(account => 
            {
                account.Events.ToList().ForEach(evt => 
                {
                    Console.WriteLine($"Act: {account.Id} - Dvc: {evt.DeviceID.ToString()} - Dt: {evt.TransactionDate.ToString()} - IsEnabled: {evt.Enabled}");
                });               
            });

        }

        static void Create()
        {
            var repository = new AccountRepository();

            Random rnd = new Random();
            Random status = new Random();
            Random days = new Random();

            for (int i = 1; i <= MAX_ACCOUNTS; i++)
            {
                var maxEvents = rnd.Next(1, MAX_EVENTS);
                var events = new List<Model.Event>();

                var account = new Account();
                account.HouseHoldId = i;

                var lastStatus = true;
                
                for (int j = 1; j <= maxEvents; j++)
                {
                    var transaction = new OptIn.Model.Event();
                    transaction.DeviceID = Guid.NewGuid().ToString();
                    transaction.TransactionDate = DateTime.Now.AddDays(-1 * days.Next(365));
                    transaction.Enabled = lastStatus;
                    
                    events.Add(transaction);

                    lastStatus = !lastStatus;
                }
                
                account.Events = events.ToList();
                account.LastEvent = account.Events.LastOrDefault();
                repository.Insert(account);
            }

            

        }        
    }
}
