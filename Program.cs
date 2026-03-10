using System;
using System.Collections.Generic;

// Detta är en klass som representerar transkationer i budgeten
class Transaction
{
    // Public på alla för att kunna nås utanför klassen
    // Namnet på transaktionen
    public string Name { get; set; }
    // Beloppet för transaktionen
    public double Amount { get; set; }
    // Typ av transaktion (t.ex. "Inkomst" eller "Utgift")
    public string Type { get; set; }
}

class Program
{
    static void Main()
    {
        // Skapar en lista för att lagra alla transaktioner
        List<Transaction> transactions = new List<Transaction>();

        // Denna variabeln styr om programmet ska köras eller inte
        bool running = true;

        // Så länge 'running' är sant/true så kommer programmet att fortsätta
        while (running)
        {
            // Visar menyn för användaren
            ShowMenu();

            // Läser vad användaren skriver 
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Lägg till en inkomst
                    AddTransaction(transactions, "Inkomst");
                    break;

                case "2":
                    // Lägg till en utgift
                    AddTransaction(transactions, "Utgift");
                    break;

                case "3":
                    // Visa lla transaktioner
                    ShowTransactions(transactions);
                    break;

                case "4":
                    // Visa totala balansen
                    ShowBalance(transactions);
                    break;

                case "5":
                    // Avsluta programmet
                    running = false;
                    Console.WriteLine("Avslutar programmet. Tack för att du använde Budget Tracker!");
                    break;

                default:
                    // Om användaren skriver något som inte är 1-5 så visas detta meddelandet
                    Console.WriteLine("Ogiltigt val. Vänligen välj ett nummer mellan 1 och 5.");
                    break;
            }

        }
    }

    // Denna metod visar menyn för användaren
    static void ShowMenu()
    {
        Console.WriteLine("\nVälkommen till Budget Tracker!");
        Console.WriteLine("1. Lägg till inkomst");
        Console.WriteLine("2. Lägg till utgift");
        Console.WriteLine("3. Visa alla transaktioner");
        Console.WriteLine("4. Visa total balans");
        Console.WriteLine("5. Avsluta");
        Console.Write("Välj ett alternativ: ");
    }

    // Denna metod lägger till en ny transaktion
    // Beroende om användaren väljer case 1 eller 2 så komemr 'type' variabeln att vara "inkomst" eller "utgift"
    // Detta beror på att vi har "inkomst" och "utgift" som argument i switch-case blocket
    // Båda är argument 2 som skickas till AddTransaction metodens 'type' som är parameter 2
    static void AddTransaction(List<Transaction> transactions, string type)
    {
        // Ställer användaren fråga om transaktionens namn, svaret sparas som lowercase i variabeln
        Console.Write($"Skriv namn på {type.ToLower()}: ");
        string name = Console.ReadLine();

        // Kontrollerar så att namnet är inte blankt
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Namnet på transaktionen kan inte vara blankt.");
            return;
        }

        // Denna variabeln används för att kontrollera om namnet innehåller minst en bokstav
        bool containsLetter = false;

        // Kontrollerar så att namnet innehåller minst en bokstav 
        foreach (char c in name)
        {
            if (char.IsLetter(c))
            {
                // Om det finns en bokstav så sätts containsLetter till true och loopen bryts
                containsLetter = true;
                break;
            }
        }

        // Om namnet inte innehåller någon bokstav så visas detta meddelandet
        if (!containsLetter)
        {
            Console.WriteLine("Namnet på transaktionen måste innehålla minst en bokstav.");
            return;
        }

        Console.Write("Skriv beloppet: ");

        // Sparar användarens svar i input variabeln
        string input = Console.ReadLine();

        // Försöker konvertera input till en double alltså ett nummer
        if (double.TryParse(input, out double amount))
        {
            // Kontrollerar så att beloppet är större än noll
            if (amount <= 0)
            {
                Console.WriteLine("Beloppet måste vara större än noll.");
                return;
            }
            // Skapar ett nytt Transaction-objekt
            Transaction transaction = new Transaction
            {
                Name = name,    //Sätter namnet
                Amount = amount, //Sätter beloppet
                Type = type    //Sätter typen (Inkomst eller Utgift)    
            };

            // Lägger till objektet i listan
            transactions.Add(transaction);

            Console.WriteLine($"{type} har lagts till.");
        }
        else
        {
            Console.WriteLine("Ogiltigt belopp. Vänligen ange ett giltigt belopp.");
        }
    }


    static void ShowTransactions(List<Transaction> transactions)
    {
        Console.WriteLine("\nAlla transaktioner:");

        // Om listan är tom så visas detta meddelandet
        if (transactions.Count == 0)
        {
            Console.WriteLine("Inga transaktioner att visa.");
            return;
        }

        // Loppar igenom alla objetk i listan
        for (int i = 0; i < transactions.Count; i++)
        {
            //Skriver ut varje transaktion
            Console.WriteLine($"{i + 1}. {transactions[i].Type}: {transactions[i].Name} - {transactions[i].Amount} kr");
        }

    }

    static void ShowBalance(List<Transaction> transactions)
    {
        // Startvärdet för balansen
        double balance = 0;

        // går igenom varje transaktion i listan
        foreach(Transaction transaction in transactions)
        {
            // Om typen av transaktionen är "inkomst" så läggs beloppet till i balansen
            if (transaction.Type == "Inkomst")
            {
                balance += transaction.Amount;
            }

            // Om typen av transaktionen är "utgift" så dras beloppet från balansen
            else if (transaction.Type == "Utgift")
            {
                balance -= transaction.Amount;
            }
        }
        
        Console.WriteLine($"\nTotal balans: {balance:F2} kr");
    }




}  