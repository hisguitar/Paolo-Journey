using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float pulseSpeed = 1f;
    [SerializeField] private float minScale = 0.8f;
    [SerializeField] private float maxScale = 1.2f;

    private RectTransform imageRectTransform;

    private void Awake()
    {
        if (targetImage != null)
        {
            imageRectTransform = GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (targetImage != null && targetImage.gameObject.activeInHierarchy)
        {
            float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * pulseSpeed, 1));
            imageRectTransform.localScale = new Vector3(scale, scale, 1);
        }
    }
}