using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Linq;

public class PadColorChangeSystem : ReactiveSystem<GameEntity>
{
    private readonly IGroup<GameEntity> _padsGroup;

    public PadColorChangeSystem(Contexts contexts) : base(contexts.game) 
    {
        _padsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Pad, GameMatcher.View));
    }

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
        bool isReset = entities.Any(e => e.pad.isTriggered == false);
        
        if (isReset)
        {
            foreach (var pad in _padsGroup.GetEntities())
            {
                pad.view.spriteRenderer.color = Color.white;
            }
        }
        else
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
}