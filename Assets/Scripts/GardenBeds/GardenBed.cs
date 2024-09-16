using UnityEngine;

public class GardenBed : MonoBehaviour
{
    public bool freePlace;
    private MeshRenderer mr;
    private Material ownOutline;

    private void Awake()
    {
        freePlace = true;
        mr = GetComponent<MeshRenderer>();
        ownOutline = new Material(mr.materials[1]);
        ownOutline = mr.materials[1];
        ownOutline.SetFloat(ownOutline.shader.GetPropertyName(2), 0.0f);
    }
    
    public void SetOutlineOn() => ownOutline.SetFloat(ownOutline.shader.GetPropertyName(2), 1.0f);
    public void SetOutlineOff() => ownOutline.SetFloat(ownOutline.shader.GetPropertyName(2), 0.0f);
}
