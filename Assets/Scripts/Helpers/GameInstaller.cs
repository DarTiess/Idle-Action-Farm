using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelEventService _levelEventService;
    [SerializeField] private CanvasControl _canvasController;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private PlayerBlockStack _playerblockStack;
    [SerializeField] private Economics _economics;


    public override void InstallBindings()
    {
        BindLevelManager();
        BindCanvasController();
        BindPlayer();
        BindBlockStack();
        BindEconimics();
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

    private void BindEconimics()
    {
        Container.Bind<Economics>().FromInstance(_economics).AsSingle();
    }
}