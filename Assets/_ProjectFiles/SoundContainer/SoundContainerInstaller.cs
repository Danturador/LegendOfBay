using UnityEngine;
using Zenject;

namespace _ProjectFiles.SoundContainer
{
    // use on project context !!!
    [CreateAssetMenu(menuName = "Installers/Sound Container installer")]
    public class SoundContainerInstaller : ScriptableObjectInstaller<SoundContainerInstaller>
    {
        [SerializeField] private SoundContainer soundContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundContainer>().FromInstance(soundContainer).AsSingle();
        } 
    }
}