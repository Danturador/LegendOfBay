using UnityEngine;

namespace _ProjectFiles.Menu
{
    public class VideoScaler : MonoBehaviour
    {
        [SerializeField] private RenderTexture renderTexture;

        private void Awake()
        {
            renderTexture.width = 3840;
            renderTexture.height = 1080;
        }
    }
}