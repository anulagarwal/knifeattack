using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    public List<Knife> KnifeList;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 110 * Time.deltaTime); //rotates 50 degrees per second around z axis
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        collision.gameObject.transform.SetParent(transform);
        KnifeList.Add(collision.gameObject.GetComponent<Knife>());
    }
    
}
