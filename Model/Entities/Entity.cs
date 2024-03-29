﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Model.Utils;

namespace Model.Entities;

public abstract class Entity
{
    protected Entity(int id)
    {
        Id = id;
    }

    public virtual async Task DoCreate()
    {
        await this.ModelContext.DataMapper.Create(this);
    }

    public virtual async Task DoDelete()
    {
        await this.ModelContext.DataMapper.Delete(this);
    }

    public virtual async Task DoUpdate()
    {
        await this.ModelContext.DataMapper.Update(this);
    }

    [JsonInclude]
    public int Id { get; protected set; }

    [JsonIgnore] [NotMapped] 
    public ModelContext ModelContext { get; protected set; } = null!;

    public void SetModelContext(ModelContext modelContext)
    {
        ModelContext = modelContext;
    }
}