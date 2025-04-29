using Zenject;

public class InputControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InputController inputController = new InputController();
        inputController.Enable();
        Container.Bind<InputController>().FromInstance(inputController).AsSingle();
    }
}
