using System;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public bool canGather;
    
    public static Action OnGatheringPlant;
    public static Action OnGatheringMeat;
    
    public void GatherPlant()
    {
        transform.parent.GetComponent<GardenBed>().freePlace = true;
        OnGatheringPlant?.Invoke();
        Destroy(gameObject);
    }
    
    public void GatherMeat()
    {
        OnGatheringMeat?.Invoke();
        Destroy(gameObject);
    }
}
