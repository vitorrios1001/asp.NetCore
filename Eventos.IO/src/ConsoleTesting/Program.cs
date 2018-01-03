using System;
using Eventos.IO.Domain.Eventos;
namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var evento = new EventoModel(
                "",
                DateTime.Now,
                DateTime.Now,
                true,
                200,
                false,
                "");
                
            
            
            Console.WriteLine(evento.ToString());

            Console.WriteLine(evento.EhValido());

            if (!evento.ValidationResult.IsValid)
            {
                foreach(var erro in evento.ValidationResult.Errors)
                {
                    Console.WriteLine(erro.ErrorMessage);
                }
            }

            Console.ReadKey();

        }
    }
}