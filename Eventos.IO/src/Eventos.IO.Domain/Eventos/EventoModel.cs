using System;
using System.Collections.Generic;
using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Organizadores;
using FluentValidation;


namespace Eventos.IO.Domain.Eventos
{
    public class EventoModel : Entity<EventoModel>
    {
        public EventoModel(
            string nome, 
            DateTime dataInicio, 
            DateTime dataFim, 
            bool gratuito, 
            decimal valor,
            bool online,
            string nomeEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;

        }

        private EventoModel() { }
        

        public string Nome { get; private set; }

        public string DescricaoCurta { get; private set; }

        public string DescricaoLonga { get; private set; }

        public DateTime DataInicio { get; private set; }

        public DateTime DataFim { get; private set; }

        public bool Gratuito { get; private set; }

        public decimal Valor { get; private set; }

        public bool Online { get; private set; }

        public string NomeEmpresa { get; private set; }
    
        public CategoriaModel Categoria { get; private set; }

        public ICollection<TagsModel> Tags { get; private set; }

        public EnderecoModel Endereco { get; private set; }

        public OrganizadorModel Organizador { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações        

        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarEmpresa();
            ValidationResult = Validate(this);
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do evento está vazio")
                .Length(2, 150).WithMessage("O nome do evento tem que conter entre 2 e 150 caracteres");
                
        }

        private void ValidarValor()
        {
            if (!Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 500000)
                    .WithMessage("O valor precisa estar entre 1 e 500000");

            if (Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(0, 0).When(e => e.Gratuito)
                    .WithMessage("O valor não deve ser diferente de 0 quando o evento é gratuito");
        }

        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                .GreaterThan(c => c.DataFim)
                .WithMessage("A data de início deve ser maior que a data do final do evento");

            RuleFor(c => c.DataFim)
                .LessThan(DateTime.Now)
                .WithMessage("A data de início não deve ser menor que a data atual");
        }

        private void ValidarLocal()
        {
            if (Online)
                RuleFor(c => c.Endereco)
                    .Null().When(c => c.Online)
                    .WithMessage("O evento não deve possuir um endenreço se for online");

            if (!Online)
                RuleFor(c => c.Endereco)
                    .NotNull().When(c => c.Online == false)
                    .WithMessage("O evento deve possuir um endereço");
        }

        private void ValidarEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome do organizador precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do organizador precisa conter entre 2 e 150 caracteres");
        }



        #endregion

        public static class EventoFactory
        {
            public static EventoModel NovoEventoCompleto(Guid id,
                                                         string nome,
                                                         string descCurta,
                                                         string descLonga,
                                                         DateTime dataInicio,
                                                         DateTime dataFim,
                                                         bool gratuito,
                                                         decimal valor,
                                                         bool online,
                                                         string nomeEmpresa,
                                                         Guid? organizadorId)
            {
                var evento = new EventoModel()
                {
                    Id = Guid.NewGuid(),
                    Nome = nome,
                    DescricaoCurta = descCurta,
                    DescricaoLonga = descLonga,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Gratuito = gratuito,
                    Valor = valor,
                    Online = online,
                    NomeEmpresa = nomeEmpresa
                };

                if (organizadorId != null)
                    evento.Organizador = new OrganizadorModel(organizadorId.Value);

                return evento;
            }

        }

    }
}