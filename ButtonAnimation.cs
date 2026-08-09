using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // ક્લિક કરતા બટન સેજ નાનું થશે
        transform.localScale = new Vector3(0.95f, 0.95f, 1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // છોડતા જ નોર્મલ સાઈઝ થઈ જશે
        transform.localScale = Vector3.one;
    }
}