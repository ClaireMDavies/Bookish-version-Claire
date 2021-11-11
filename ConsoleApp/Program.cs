using System;
using System.Linq;
using Bookish.DataAccess;

namespace Bookish.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BookRepository boooook = new BookRepository();

            Console.WriteLine(boooook.GetUseridByName("Sophie"));
            //foreach(var book in boooook.GetCopiesofBook(3))
            //{
            //    //Console.WriteLine(book.author);
            //   //Console.WriteLine(book.title);
               
                
            //}
            Console.ReadLine();
        }
    }
}
