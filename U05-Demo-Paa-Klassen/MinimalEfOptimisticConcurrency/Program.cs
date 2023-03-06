// See https://aka.ms/new-console-template for more information

using System.Data;
using Microsoft.EntityFrameworkCore;
using MinimalEfOptimisticConcurrency;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //CreateInitialData();
        //OptimisticConcurrency();
        PhantomConcurrency();
    }


    private static void OptimisticConcurrency()
    {
        using var context = new PersonContext();

        var person = context.Persons.First(a => a.LastName == "Hanks");
        person.FirstName = "Tut";
        context.SaveChanges();
    }


    private static void CreateInitialData()
    {
        using var context = new PersonContext();

        if (!context.Persons.Any())
        {
            var person = new Person
            {
                FirstName = "Tom",
                LastName = "Hanks"
            };

            context.Persons.Add(person);

            context.SaveChanges();
        }
    }


    private static void PhantomConcurrency()
    {
        using var context = new PersonContext();
        using var transaction = context.Database.
            BeginTransaction(IsolationLevel.Serializable);
        try
        {
            if (!context.Persons.Any(a => a.LastName == "Hanks"))
            {
                var person = new Person
                {
                    FirstName = "Tut",
                    LastName = "Hanks"
                };
                context.Persons.Add(person);
                context.SaveChanges();
                transaction.Commit();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            transaction.Rollback();
        }
    }
}