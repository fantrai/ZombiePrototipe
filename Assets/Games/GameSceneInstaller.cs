using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] GameObject startPlayerPos;
    [SerializeField] Player playerPrefab;
    [SerializeField] EnemySpawner spawner;

    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<IPlayer>(playerPrefab, startPlayerPos.transform.position, playerPrefab.transform.rotation, null);
        Container.Bind<IPlayer>().FromInstance(player).AsSingle().NonLazy();
    }
}
