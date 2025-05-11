using Cinemachine;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] parallaxScales;
    [SerializeField] private GameObject mainCamera;

    private Vector3 previousCameraPosition;

    private void Awake()
    {
        mainCamera = FindAnyObjectByType<CinemachineBrain>().gameObject;
    }
    private void Start()
    {
        previousCameraPosition = mainCamera.transform.position;
    }

    private void Update()
    {
        Vector3 cameraDelta = mainCamera.transform.position - previousCameraPosition;

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].position += new Vector3(cameraDelta.x * parallaxScales[i],0,0);
        }

        previousCameraPosition = mainCamera.transform.position;
    }
}
