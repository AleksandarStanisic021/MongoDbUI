
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Data Processing...!");
Console.WriteLine(GetConnectionString_1("Default"));

var contact = new ContactModel
{
    FirstName = "saska",
    LastName = "stanisic"
};
contact.EmailAddresses.Add(new EmailAddressModel
{
    EmailAddress = "sasa@sxsav"
});
contact.EmailAddresses.Add(new EmailAddressModel
{
    EmailAddress = "sasa@sxsav"
});
contact.PhoneNumbers.Add(new PhoneNumberModel
{
    PhoneNumber = "123-123-1234"
});
contact.PhoneNumbers.Add(new PhoneNumberModel
{
    PhoneNumber = "123-123-1234"
});



MongoDbDataAccess db;
Console.WriteLine("write data");
db = new MongoDbDataAccess("MongoDb", GetConnectionString_1("Default"));


db.UpsertRecord("Contacts2", contact.Id, contact, "Contacts2");

var contacts = db.LoadRecords<ContactModel>("Contacts2");
foreach (var item in contacts)
{
    Console.WriteLine($"{item.Id}: {item.FirstName} {item.LastName}");
    if (item.EmailAddresses != null)
    {
        foreach (var email in item.EmailAddresses)
        {
            Console.WriteLine($"  {email.EmailAddress}");
        }
    }
    if (item.PhoneNumbers != null)
    {
        foreach (var phone in item.PhoneNumbers)
        {
            Console.WriteLine($"  {phone.PhoneNumber}");
        }
    }
}


Console.ReadLine();

string GetConnectionString_1(string connnection="Default")
{
    string output = "";
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("app.json");
    var configuration = builder.Build();
    output = configuration.GetConnectionString(connnection);
    return output;
}   
