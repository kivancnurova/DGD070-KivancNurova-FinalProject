using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PadTriggerSystem : ReactiveSystem<GameEntity>
{
    private readonly IGroup<GameEntity> _playerGroup;
    private readonly IGroup<GameEntity> _padsGroup;
    private readonly IGroup<GameEntity> _sequenceGroup;
    private int _triggeredPadCount = 0;

    public PadTriggerSystem(Contexts contexts) : base(contexts.game)
    {
        _playerGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
        _padsGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Pad, GameMatcher.Position));
        _sequenceGroup = contexts.game.GetGroup(GameMatcher.PadSequence);
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
        var sequenceEntity = _sequenceGroup.GetSingleEntity();
        Vector2 playerPos = playerEntity.position.value;

        foreach (var padEntity in _padsGroup.GetEntities())
        {
            if (!padEntity.pad.isTriggered && Vector2.Distance(playerPos, padEntity.position.value) < 1f)
            {
                if (padEntity.pad.padId == sequenceEntity.padSequence.correctSequence[sequenceEntity.padSequence.currentSequenceIndex])
                {
                    padEntity.ReplacePad(padEntity.pad.padId, true);
                    
                    sequenceEntity.ReplacePadSequence(
                        sequenceEntity.padSequence.currentSequenceIndex + 1,
                        sequenceEntity.padSequence.correctSequence
                    );
                    _triggeredPadCount++;

                    if (_triggeredPadCount >= 4)
                    {
                        ShowWinScreen();
                    }
                }
                else
                {
                    ResetGame();
                }
            }
        }
    }

    private void ShowWinScreen()
    {
        Debug.Log("A WINRAR IS YOU");

        var contexts = Contexts.sharedInstance;
        var winEntity = contexts.game.CreateEntity();
        winEntity.AddWinScreen(true);

        var playerEntity = _playerGroup.GetSingleEntity();

        if (playerEntity.hasView && playerEntity.view.gameObject != null)
        {
            GameObject.Destroy(playerEntity.view.gameObject);
        }

        playerEntity.Destroy();
    }

    private void ResetGame()
    {
        _triggeredPadCount = 0;
        var sequenceEntity = _sequenceGroup.GetSingleEntity();
        sequenceEntity.ReplacePadSequence(0, sequenceEntity.padSequence.correctSequence);

        foreach (var padEntity in _padsGroup.GetEntities())
        {
            padEntity.ReplacePad(padEntity.pad.padId, false);
        }

        var playerEntity = _playerGroup.GetSingleEntity();
        playerEntity.ReplacePosition(Vector2.zero);
    }
}