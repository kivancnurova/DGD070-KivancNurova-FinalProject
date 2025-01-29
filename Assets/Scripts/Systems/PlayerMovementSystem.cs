using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PlayerMovementSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _movableEntities;
    private readonly IGroup<GameEntity> _boundaryEntities;

    public PlayerMovementSystem(Contexts contexts)
    {
        _movableEntities = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Position,
            GameMatcher.Movable,
            GameMatcher.Player));
        
        _boundaryEntities = contexts.game.GetGroup(GameMatcher.Boundary);
    }

    public void Execute()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        foreach (var entity in _movableEntities)
        {
            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            Vector2 newPosition = entity.position.value + movement * entity.movable.speed * Time.deltaTime;

            var boundary = _boundaryEntities.GetSingleEntity().boundary;
            newPosition.x = Mathf.Clamp(newPosition.x, boundary.minX, boundary.maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, boundary.minY, boundary.maxY);

            entity.ReplacePosition(newPosition);
        }
    }
}