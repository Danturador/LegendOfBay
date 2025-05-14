using UnityEngine;
using Zenject;

namespace _ProjectFiles.Menu.MenuButtons
{
    [CreateAssetMenu(menuName = "Installers/Buttons sound Container installer")]
    public class ButtonsSoundContainerInstaller : ScriptableObjectInstaller<ButtonsSoundContainerInstaller>
    {
        [SerializeField] private ButtonsSoundContainer soundContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<ButtonsSoundContainer>().FromInstance(soundContainer).AsSingle();
        } 
    }
}