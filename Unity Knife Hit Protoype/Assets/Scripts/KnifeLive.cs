using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeLive : MonoBehaviour
{
    public GameObject obj;
    public List<GameObject> knifelive;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SpawnKnifeLive()
    {
        knifelive.Add(Instantiate(obj, transform));
    }
    public void RemoveKnife()
    {
        Destroy(transform.GetChild(knifelive.Count - 1).gameObject);
        knifelive.RemoveAt(knifelive.Count - 1);
    }
}
