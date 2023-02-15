using Model.Utils;

namespace Actions;

public class Action
{
    public ModelContext ModelContext { get; protected set; } = null!;

    public void SetModelContext(ModelContext context)
    {
        this.ModelContext = context;
    }
}