using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PadTriggerSystem : ReactiveSystem<GameEntity>
{
    private readonly IGroup<GameEntity> _playerGroup;
    private readonly IGroup<GameEntity> _padsGroup;
    private int _triggeredPadCount = 0;

    public PadTriggerSystem(Contexts contexts) : base(contexts.game)
    {
        _playerGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
        _padsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Pad, GameMatcher.Position));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var playerEntity = _playerGroup.GetSingleEntity();
        Vector2 playerPos = playerEntity.position.value;

        foreach (var padEntity in _padsGroup.GetEntities())
        {
            if (!padEntity.pad.isTriggered && Vector2.Distance(playerPos, padEntity.position.value) < 1f)
            {
                padEntity.ReplacePad(padEntity.pad.padId, true);
                _triggeredPadCount++;



                if (_triggeredPadCount >= 4)
                {
                    ShowWinScreen();
                }
            }
        }
    }

    private void ShowWinScreen()
    {
        Debug.Log("A WINRAR IS YOU!");
    }
}