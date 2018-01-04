using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Eventos.Repository;

public class FakeEventoRepository : IEventoRepository
{

    public void Add(EventoModel obj)
    {
        //
    }

    public void Dispose()
    {
        //
    }

    public IEnumerable<EventoModel> Find(Expression<Func<EventoModel, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<EventoModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public EventoModel GetById(Guid id)
    {
        return new EventoModel("Fake", DateTime.Now, DateTime.Now, true, 0, true, "Empresa");
    }

    public void Remove(Guid id)
    {
        //
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void Update(EventoModel obj)
    {
        //
    }
}
