using Eventos.IO.Domain.Core.Models;
using System;

namespace Eventos.IO.Domain.Organizadores
{
    public class OrganizadorModel : Entity<OrganizadorModel>
    {
        public OrganizadorModel(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            throw new System.NotImplementedException();
        }
    }

}