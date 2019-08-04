using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public List<GameObject> HappyEmojis;
    public List<GameObject> SadEmojis;
    private float startTime;
    public float intervalTime;
    public bool hasFallingStarted;
    public int numberSpawned;
    public int numberNeedToSpawn;
    Transform posi;
    public int type;
    // Use this for initialization
    void Start()
    {
    }
    public void SpawnObjects(int number, Transform pos, int type)
    {
        numberSpawned = 0;
        numberNeedToSpawn = number;
        startTime = Time.time;
        hasFallingStarted = true;
        posi = pos;
        this.type = type;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasFallingStarted && numberSpawned <= numberNeedToSpawn && posi!=null)
        {
            if (startTime + intervalTime < Time.time)
            {
                if (type == 1)
                {
                    GameObject tm = Instantiate(HappyEmojis[Random.Range(0, HappyEmojis.Count)], posi.position, Quaternion.identity) as GameObject;
                    var randomR = Random.Range(0.05f, 0.2f);
                    tm.transform.localScale = new Vector3(randomR, randomR, randomR);
                    Destroy(tm, 2f);
                }
                else if (type == 2)
                {
                    GameObject tm = Instantiate(SadEmojis[Random.Range(0, SadEmojis.Count)], posi.position, Quaternion.identity) as GameObject;
                    var randomR = Random.Range(0.05f, 0.2f);
                    tm.transform.localScale = new Vector3(randomR, randomR, randomR);
                    Destroy(tm, 2f);
                }
                // tm.transform.SetParent(posi);
                numberSpawned++;
                startTime = Time.time;
            }
        }
        else
        {
            hasFallingStarted = false;
            numberSpawned = 0;
        }

    }
}
