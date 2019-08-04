using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject KnifeObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.CurrentKnifeHitCount > 0)
        {
            GetComponentInChildren<Knife>().Shoot();
        }
    }

   public void SpawnKnife()
    {
        GameObject t = Instantiate(KnifeObj) as GameObject;
        t.transform.SetParent(transform);
        t.transform.localPosition = GameObject.Find("KnifePos").transform.localPosition;
    }
}
