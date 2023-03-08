using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelEventService _levelEventService;
    [SerializeField] private CanvasControl _canvasController;
    [SerializeField] private PlayerMovement _player;
   [SerializeField] private PlayerBlockStack _playerblockStack;
   

    public override void InstallBindings()
    {
        BindLevelManager();
        BindCanvasController();        
        BindPlayer();    
        BindBlockStack();
    }
       
    private void BindLevelManager()
    {
        Container.Bind<LevelEventService>().FromInstance(_levelEventService).AsSingle();
    }
    private void BindCanvasController()
    {
        Container.Bind<CanvasControl>().FromInstance(_canvasController).AsSingle();
    }

    void BindPlayer()
    {
        Container.Bind<PlayerMovement>().FromInstance(_player).AsSingle();
    }
  
     private void BindBlockStack()
    {
       Container.Bind<PlayerBlockStack>().FromInstance(_playerblockStack).AsSingle();
    }
}