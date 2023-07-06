using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorView : MonoBehaviour
{
    private RectTransform rectTransform;

    private float lerpSpeed = 25f;

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update() 
    {
        var selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null) return;

        transform.position = Vector3.Lerp(transform.position, selected.transform.position, lerpSpeed * Time.deltaTime);

        var otherRect = selected.GetComponent<RectTransform>();

        var horizontalLerp = Mathf.Lerp(rectTransform.rect.size.x, otherRect.rect.size.x, lerpSpeed * Time.deltaTime);
        var verticalLerp = Mathf.Lerp(rectTransform.rect.size.y, otherRect.rect.size.y, lerpSpeed * Time.deltaTime);

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalLerp);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalLerp);
    }
}
