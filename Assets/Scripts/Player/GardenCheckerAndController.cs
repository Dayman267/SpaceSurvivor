using System;
using UnityEngine;

public class GardenCheckerAndController : MonoBehaviour
{
    private GameObject freeGardenBed;
    private bool canPlant;
    private bool isDragging;
    [SerializeField] private GameObject smallPlant;

    public static Action OnSeedPlanted;
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("GardenBed"))
        {
            if(!collider.gameObject.GetComponent<GardenBed>().freePlace) return;
            canPlant = true;
            freeGardenBed = collider.gameObject;
            if(isDragging) freeGardenBed.GetComponent<GardenBed>().SetOutlineOn();
            else freeGardenBed.GetComponent<GardenBed>().SetOutlineOff();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("GardenBed"))
        {
            canPlant = false;
            freeGardenBed.GetComponent<GardenBed>().SetOutlineOff();
            freeGardenBed = null;
        }
    }


    private void DragStatement(bool isDragging)
    {
        this.isDragging = isDragging;
    }

    private void Plant()
    {
        if(!canPlant) return;
        GameObject plant = Instantiate(
            smallPlant, 
            freeGardenBed.transform.position + new Vector3(0, 0.3f, 0), 
            Quaternion.identity);
        plant.transform.parent = freeGardenBed.transform;
        canPlant = false;
        freeGardenBed.GetComponent<GardenBed>().freePlace = false;
        freeGardenBed.GetComponent<GardenBed>().SetOutlineOff();
        freeGardenBed = null;
        OnSeedPlanted?.Invoke();
    }

    private void OnEnable()
    {
        Dragable.OnSeedDragged += DragStatement;
        Dragable.OnSeedReleased += Plant;
    }

    private void OnDisable()
    {
        Dragable.OnSeedDragged -= DragStatement;
        Dragable.OnSeedReleased -= Plant;
    }
}
