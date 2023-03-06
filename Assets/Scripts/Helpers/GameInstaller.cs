using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelEventService _levelEventService;
    [SerializeField] private CanvasControl _canvasController;
    [SerializeField] private PlayerMovement _player;
   

    public override void InstallBindings()
    {
        BindLevelManager();
        BindCanvasController();        
        BindPlayer();    
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
  

}