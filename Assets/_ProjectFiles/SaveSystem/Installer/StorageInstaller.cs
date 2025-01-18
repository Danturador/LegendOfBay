using Zenject;

namespace _ProjectFiles.SaveSystem.Installer
{
    public class StorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStorage();
        }

        private void BindStorage()
        {
            Container.BindInterfacesTo<Storage>().AsSingle();
        }
    }
}