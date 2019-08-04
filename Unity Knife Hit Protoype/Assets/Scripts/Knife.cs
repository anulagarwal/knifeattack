using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public bool IsShooting;
    public bool HasHit;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (IsShooting && !GameManager.instance.IsPaused)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        }
    }
    public void Shoot()
    {
        IsShooting = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!HasHit)
        {
            if (collision.gameObject.name == "Circle")
            {
                if (GameManager.instance.gameMode== GameManager.GameMode.Blub)
                {
                    HitKnife();
                }
                else if (  gameObject.name != "PreSpawned(Clone)")
                {
                    collision.GetComponent<Circle>().KnifeList.Add(this);
                    transform.SetParent(collision.gameObject.transform);
                    transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                    HitCircle();
                }
            }
            else if (collision.gameObject.tag == "Knife")
            {
                 HitKnife();
            }
            else if (GameManager.instance.gameMode == GameManager.GameMode.Blub)
            {
                collision.GetComponentInParent<Circle>().KnifeList.Add(this);
                transform.SetParent(collision.gameObject.transform);
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                HitCircle();
            }
            HasHit = true;
        }
    }
    public void EndGame()
    {
        GameManager.instance.LoseGame();
        Destroy(gameObject);
    }
    public void HitKnife()
    {
        GameObject.Find("Shooter").GetComponent<Shooter>().enabled = false;
        EndGame();
    }
    public void HitCircle()
    {
        this.enabled = false;
        GameObject.Find("Shooter").GetComponent<Shooter>().SpawnKnife();
        GameManager.instance.UpdateKnifeHit();
    }
}
