using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public RectTransform fillBar; // Drag the pink bar RectTransform here

    private float originalWidth;

    private void Start()
    {
        StartCoroutine(InitWidth());
    }

    private System.Collections.IEnumerator InitWidth()
    {
        yield return null; // Wait one frame for layout to initialize
        if (fillBar != null)
        {
            originalWidth = fillBar.rect.width;
            Debug.Log("[HealthBarUI] Original width = " + originalWidth);
        }
    }

    public void SetHealthFraction(float fraction)
    {
        if (fillBar == null || originalWidth <= 0)
            return;

        fraction = Mathf.Clamp01(fraction);

        // Shrink only width — position stays untouched
        Vector2 size = fillBar.sizeDelta;
        size.x = originalWidth * fraction;
        fillBar.sizeDelta = size;
    }
}
