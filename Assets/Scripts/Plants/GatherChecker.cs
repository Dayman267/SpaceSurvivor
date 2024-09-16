using UnityEngine;
using UnityEngine.EventSystems;

public class GatherChecker : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.collider.CompareTag("Plant"))
                hit.collider.GetComponent<Gatherable>().GatherPlant();
            else if (hit.collider.CompareTag("Animal") && hit.collider.GetComponent<Gatherable>().canGather)
                hit.collider.GetComponent<Gatherable>().GatherMeat();
        }
    }
}
