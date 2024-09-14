using UnityEngine;

public class GardenChecker : MonoBehaviour
{
    public bool canPlant;
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("GardenBed"))
        {
            canPlant = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("GardenBed"))
        {
            canPlant = false;
        }
    }
}
