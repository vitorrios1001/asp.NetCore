using System;
using Eventos.IO.Domain.Models;
namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var evento = new EventoModel(
                "Nome evento",
                DateTime.Now,
                DateTime.Now,
                false,
                200,
                true,
                "Vitor Rios");

            var evento2 = evento;
            
            
            Console.WriteLine(evento.ToString());
            Console.WriteLine(evento.Equals(evento2));
            Console.ReadKey();

        }
    }
}