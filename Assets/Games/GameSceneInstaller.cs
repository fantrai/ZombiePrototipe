using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] GameObject startPlayerPos;
    [SerializeField] Player playerPrefab;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AbstractWeapon baseWeaponPlayerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        var player = Container.InstantiatePrefabForComponent<IPlayer>(playerPrefab, startPlayerPos.transform.position, playerPrefab.transform.rotation, null);
        Container.Bind<IPlayer>().FromInstance(player).AsSingle().NonLazy();
        player.AddWeapon(baseWeaponPlayerPrefab);
    }
}
