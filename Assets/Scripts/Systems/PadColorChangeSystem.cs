using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PadColorChangeSystem : ReactiveSystem<GameEntity>
{
    public PadColorChangeSystem(Contexts contexts) : base(contexts.game) {}

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Pad);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPad && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.pad.isTriggered)
            {
                entity.view.spriteRenderer.color = Color.green;
            }
        }
    }
}