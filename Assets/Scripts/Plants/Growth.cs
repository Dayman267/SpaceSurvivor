using System.Collections;
using UnityEngine;

public class Growth : MonoBehaviour
{
    private GameObject bigPlant;
    
    private void Start()
    {
        bigPlant = Resources.Load<GameObject>("Prefabs/BigPlant");
        if (CompareTag("Plant")) StartCoroutine(PlantGrowing());
        else if (CompareTag("Animal")) StartCoroutine(AnimalGrowing());
    }

    private IEnumerator PlantGrowing()
    {
        yield return new WaitForSeconds(3);
        Instantiate(
            bigPlant,
            transform.position,
            Quaternion.identity,
            gameObject.transform.parent);
        Destroy(gameObject);
    }

    private IEnumerator AnimalGrowing()
    {
        yield return new WaitForSeconds(3);
        transform.localScale *= 2;
        GetComponent<Gatherable>().canGather = true;
    }
}
