﻿using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Interfaces;

public class FakeUow : IUnitOfWork
{
    public CommandResponse Commit()
    {
        return new CommandResponse(true);
    }

    public void Dispose()
    {
        //
    }
}