using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Model.Entities;

namespace Model.Utils;

public class ModelContextInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(
        MaterializationInterceptionData materializationData,
        object instance)
    {
        if (instance is Entity entity)
        {
            var context = materializationData
                .Context
                .GetService<ModelContext>();
            entity.SetModelContext(context);
        }

        return instance;
    }
}