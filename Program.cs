// See https://aka.ms/new-console-template for more information
using Guider.Models;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

NorthwindContext db = new NorthwindContext();


MainDisplay(db);

static void MainDisplay(NorthwindContext db)
{
    Console.WriteLine("\n------Phone Contact Guide------");
    Console.WriteLine("Add user: A");
    Console.WriteLine("List users: L");
    Console.WriteLine("Search user: S");
    Console.WriteLine("Exit: E");
    ConditionStage(db);
}
static void ConditionStage(NorthwindContext db)
{
ValidInput:
    Console.Write("\nInput:");
    string input = Console.ReadLine();
    switch (input)
    {
        case "A":
            AddContact(db);
            MainDisplay(db);
            break;
        case "L":
            ListUsers(db);
            MainDisplay(db);
            break;
        case "S":
            MainDisplay(db);
            break;
        case "E":
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input!");
            Console.ResetColor();
            goto ValidInput;
    }
}

static void AddContact(NorthwindContext db)
{
    Console.Clear();
WrongName:
    Console.Write("User name: ");
    string contactName = Console.ReadLine();
    if (contactName.IsNullOrEmpty())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Name is cannot be null");
        Console.ResetColor();
        goto WrongName;
    }
WrongSurname:
    Console.Write("User surname: ");
    string contactSurname = Console.ReadLine();
    if (contactSurname.IsNullOrEmpty())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Surname is cannot be null");
        Console.ResetColor();
        goto WrongSurname;
    }
WrongPhone:
    Console.Write("User phone number(Number must contain only 10 character): ");
    string contactPhoneNumber = Console.ReadLine();
    if (contactPhoneNumber.IsNullOrEmpty())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Phone is cannot be null");
        Console.ResetColor();
        goto WrongPhone;
    }
WrongMail:
    Console.Write("User mail: ");
    var contactMail = Console.ReadLine();
    if (contactMail.IsNullOrEmpty())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Phone is cannot be null");
        Console.ResetColor();
        goto WrongMail;
    }
WrongInput:
    Console.WriteLine("Do you want to contunie and add this user to contact? y/n");
    string checker = Console.ReadLine();
    switch (checker)
    {
        case "y":
            break;
        case "n":
            Console.Clear();
            MainDisplay(db);
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input!");
            Console.ResetColor();
            goto WrongInput;
    }
    if (!contactMail.Contains("@") && !contactMail.Contains("."))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Mail is not appropriate!");
        Console.ResetColor();
        goto WrongMail;
    }
    if (contactPhoneNumber.Length != 10)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Phone is nor appropriate!");
        Console.ResetColor();
        goto WrongPhone;
    }
WrongEndInput:
    Console.WriteLine("Do you want to contunie and add this user to contact? y/n");
    string flag = Console.ReadLine();
    switch (flag)
    {
        case "y":
            break;
        case "n":
            Console.Clear();
            MainDisplay(db);
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input!");
            Console.ResetColor();
            goto WrongEndInput;
    }
    db.Contacts.Add(new()
    {
        Name = contactName,
        Surname = contactSurname,
        Phone = contactPhoneNumber,
        Mail = contactMail
    }
);
    bool result = db.SaveChanges() > 0;
    System.Console.WriteLine(result ? "Contact added succesfuly!" : "Process fault!");
EndInput:
    Console.WriteLine("New user add: A");
    Console.WriteLine("Return to Menu: M");
    Console.WriteLine("Exit: E");
    string lastChecker = Console.ReadLine();
    switch (lastChecker)
    {
        case "A":
            AddContact(db);
            break;
        case "M":
            MainDisplay(db);
            break;
        case "E":
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input!");
            Console.ResetColor();
            goto EndInput;
    }
}

static void ListUsers(NorthwindContext db) 
{
    var contacts = db.Contacts.ToList();
    Console.WriteLine("Id   Name    Surname    Phone     Mail  ");
    foreach (var contact in contacts)
    {
        System.Console.WriteLine($"{contact.Id,-3} {contact.Name,5} {contact.Surname,10} {contact.Phone,10} {contact.Mail,10}");
    }
    WrongInput:
    Console.WriteLine("Update user: U");
    Console.WriteLine("Delete user: D");
    Console.WriteLine("Return Menu: M");
    Console.WriteLine("Exit: E");
    string input = Console.ReadLine();
    switch (input)
    {
        case "U":
            UpdateContact(db);
            MainDisplay(db);
            break;
        case "D":
            DeleteContact(db);
            MainDisplay(db);
            break;
        case "M":
            MainDisplay(db);
            break;
        case "E":
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input!");
            Console.ResetColor();
            goto WrongInput;
    }
}
static void DeleteContact(NorthwindContext db) 
{
    Console.Write("Please enter ID of contact which you want to delete:");
    int id = Convert.ToInt32(Console.ReadLine());
    WrongInputDelete:
    Console.WriteLine("Do you want this person? y/n");
    string input= Console.ReadLine();
    switch (input) 
    {
        case "y":
            break;
        case "n":
            ListUsers(db);
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter valid input!");
            Console.ResetColor();
            goto WrongInputDelete;
    }
    try
    {
        var deletedCnt = db.Contacts.Find(id);
        db.Contacts.Remove(deletedCnt);
        bool deleteResult = db.SaveChanges() > 0;
        System.Console.WriteLine(deleteResult ? "Contact deleted" : "Remove process failed!");
    }
    catch 
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nUser not founded!");
        Console.ResetColor();
        Console.Clear();
        ListUsers(db);
    }
}

static void UpdateContact(NorthwindContext db)
{
    try 
    {
        Console.Write("\nPlease enter id of contact which you want to update:");
        int id = Convert.ToInt32(Console.ReadLine());
        var updateContact = db.Contacts.Find(id);
    WrongName:
        Console.Write("User name: ");
        string contactName = Console.ReadLine();
        if (contactName.IsNullOrEmpty())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Name is cannot be null");
            Console.ResetColor();
            goto WrongName;
        }
    WrongSurname:
        Console.Write("User surname: ");
        string contactSurname = Console.ReadLine();
        if (contactSurname.IsNullOrEmpty())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Surname is cannot be null");
            Console.ResetColor();
            goto WrongSurname;
        }
    WrongPhone:
        Console.Write("User phone number(Number must contain only 10 character): ");
        string contactPhoneNumber = Console.ReadLine();
        if (contactPhoneNumber.IsNullOrEmpty())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Phone is cannot be null");
            Console.ResetColor();
            goto WrongPhone;
        }
    WrongMail:
        Console.Write("User mail: ");
        var contactMail = Console.ReadLine();
        if (contactMail.IsNullOrEmpty())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Phone is cannot be null");
            Console.ResetColor();
            goto WrongMail;
        }
    WrongInput:
        Console.WriteLine("Do you want to contunie and add this user to contact? y/n");
        string checker = Console.ReadLine();
        switch (checker)
        {
            case "y":
                break;
            case "n":
                Console.Clear();
                MainDisplay(db);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter valid input!");
                Console.ResetColor();
                goto WrongInput;
        }
        if (!contactMail.Contains("@") && !contactMail.Contains("."))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Mail is not appropriate!");
            Console.ResetColor();
            goto WrongMail;
        }
        if (contactPhoneNumber.Length != 10)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Phone is nor appropriate!");
            Console.ResetColor();
            goto WrongPhone;
        }
        updateContact.Name = contactName;
        updateContact.Surname = contactSurname;
        updateContact.Phone = contactPhoneNumber;
        updateContact.Mail = contactMail;
        bool updatedResult = db.SaveChanges() > 0;
        System.Console.WriteLine(updatedResult ? "Contact updated" : "Update process failed!");
    }
    catch 
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nThis user not founded!");
        Console.ResetColor();
        Console.Clear();
        ListUsers(db);
    }

}