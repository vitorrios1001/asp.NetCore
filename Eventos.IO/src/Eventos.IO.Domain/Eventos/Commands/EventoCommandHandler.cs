using Eventos.IO.Domain.CommandHandlers;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Eventos.Events;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Interfaces;
using System;


namespace Eventos.IO.Domain.Eventos.Commands
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>,
        IHandler<AtualizarEventoCommand>,
        IHandler<ExcluirEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IBus _bus;

        public EventoCommandHandler(IEventoRepository eventoRepository,
                                    IUnitOfWork uow,
                                    IBus bus,
                                    IDomainNotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _eventoRepository = eventoRepository;
        }        

        public void Handle(RegistrarEventoCommand message)
        {
            var evento = new EventoModel(
               message.Nome,
               message.DataInicio,
               message.DataFim,
               message.Gratuito,
               message.Valor,
               message.Online,
               message.NomeEmpresa);

            if (!evento.EhValido())
            {
                NotificarValidacoesErro(evento.ValidationResult);
                return;
            }
            //TODO:
            //Validações de negócio!
            //Persistencia

            _eventoRepository.Add(evento);

            if (Commit())
            {
                Console.WriteLine("Evento Registrado com sucesso!");
                _bus.RaiseEvent(new EventoRegistradoEvent(evento.Id,evento.Nome, 
                                                          evento.DataInicio, 
                                                          evento.DataFim, 
                                                          evento.Gratuito, 
                                                          evento.Valor, 
                                                          evento.Online, 
                                                          evento.NomeEmpresa));
            }
        }

        public void Handle(ExcluirEventoCommand message)
        {
            if (!EventoExistente(message.Id, message.MessageType)) return;

            _eventoRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoExcluidoEvent(message.Id));
            }
        }

        public void Handle(AtualizarEventoCommand message)
        {
            if (!EventoExistente(message.Id, message.MessageType)) return;

            var evento = EventoModel.EventoFactory.NovoEventoCompleto(message.Id, message.Nome, message.DescricaoCurta,
                message.DescricaoLonga, message.DataInicio, message.DataFim, message.Gratuito, message.Valor,
                message.Online, message.NomeEmpresa, null);

            if (!EventoValido(evento))return;

            _eventoRepository.Update(evento);

            if(Commit())
            {
                _bus.RaiseEvent(new EventoAtualizadoEvent(evento.Id, evento.Nome, evento.DescricaoCurta,
                evento.DescricaoLonga, evento.DataInicio, evento.DataFim, evento.Gratuito, evento.Valor,
                evento.Online, evento.NomeEmpresa));
            }

        }

        private bool EventoExistente(Guid id, string messageType)
        {
            var evento = _eventoRepository.GetById(id);

            if (evento != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado."));
            return false;
        }

        private bool EventoValido(EventoModel evento)
        {
            if (evento.EhValido()) return true;

            NotificarValidacoesErro(evento.ValidationResult);
            return false;
        }
    }

}
