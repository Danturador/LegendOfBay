using Zenject;

namespace _ProjectFiles.SaveSystem.Installer
{
    public class SaveSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStorage();
            BindSaveSystemController();
        }

        private void BindStorage()
        {
            Container.BindInterfacesAndSelfTo<Storage>().AsSingle();
        }

        private void BindSaveSystemController()
        {
            Container.Bind<SaveSystemController>().AsSingle();
        }
    }
}