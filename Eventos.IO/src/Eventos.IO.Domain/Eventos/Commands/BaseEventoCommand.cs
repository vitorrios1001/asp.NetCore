using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.Commands
{
    public abstract class BaseEventoCommand
    {
        public string Nome { get; protected set; }

        public DateTime DataInicio { get; protected set; }

        public DateTime DataFim { get; protected set; }

        public bool Gratuito { get; protected set; }

        public decimal Valor { get; protected set; }

        public bool Online { get; protected set; }

        public string NomeEmpresa { get; protected set; }
    }
}
