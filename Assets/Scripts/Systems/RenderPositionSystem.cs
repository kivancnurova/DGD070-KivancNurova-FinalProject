using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RenderPositionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entitiesWithPosition;

    public RenderPositionSystem(Contexts contexts)
    {
        _entitiesWithPosition = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Position, GameMatcher.View));
    }

    public void Execute()
    {
        foreach (var entity in _entitiesWithPosition.GetEntities())
        {
            if (entity.hasView)
            {
                entity.view.gameObject.transform.position = entity.position.value;
            }
        }
    }
}
