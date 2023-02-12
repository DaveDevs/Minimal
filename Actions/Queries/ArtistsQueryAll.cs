﻿using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Queries;

public class ArtistsQueryAll : QueryList<Artist, UserRequestBase>
{
    public override Task<List<Artist>> Execute()
    {
        return Context.Artists.ToListAsync();
    }
}