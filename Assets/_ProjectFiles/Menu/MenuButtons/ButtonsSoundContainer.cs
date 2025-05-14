using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    [CreateAssetMenu(menuName = "Buttons Sound Container")]
    public class ButtonsSoundContainer : ScriptableObject
    {
        public AudioClip hoverSound;
        public AudioClip clickSound;
    }
}