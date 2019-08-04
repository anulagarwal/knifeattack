using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public List<Knife> KnifeList;
    public List<Level.LevelSpeedCurve> curve;
    public float Speed;
    public float Duration;
    public float currentSpeed;
    public float StartTime;
    public bool IsAccelrating;
    public int currentCount;
    public bool IsReverse =false;
    public List<Sprite> HappyImages;
    public List<Sprite> NeutralImages;
    public List<Sprite> SadImage;
    public Sprite DeadImage;
    public Sprite DefaultImage;
    public Sprite BonusLevelImage;
    public Sprite ColorLevelImage;
    public bool IsBlubLevel;
    List<GameObject> BonusObj = new List<GameObject>();
    public GameObject BlubObj;
    public GameObject KnifeObj;

    // Update is called once per frame
    //Script for the center rotation wheel
    private void Start()
    {
        currentCount = 0;
        Speed = curve[currentCount].speed;
        currentSpeed = Speed-5;
        Duration = curve[currentCount].duration;
        StartTime = Time.time;
    }
    public void Clear()
    {
        foreach (Knife kn in KnifeList)
        {
            //circle.KnifeList.Remove(kn);
            Destroy(kn.gameObject);
        }
        KnifeList.Clear();
        foreach(GameObject go in BonusObj)
        {
            Destroy(go);
        }
        BonusObj.Clear();
    }
    //Spawning the bonus blubs in the level
    public void SpawnBlub(int number)
    {
        IsBlubLevel = true;
        Vector2 tempPos = Vector2.zero;
        int x = 0;
        List<int> index = new List<int>();
        Transform indexTransform = transform.Find("index");
        while (x < number)
        {
            int r = Random.Range(0, indexTransform.childCount);
            bool exists= false;
            foreach(int i in index)
            {
                if (i == r)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                GameObject go = Instantiate(BlubObj, transform) as GameObject;
                index.Add(r);
                BonusObj.Add(go);
                go.transform.position = indexTransform.GetChild(r).position;
                x++;
            }
        }
    }
    //Spawning the knifes in the level

    public void SpawnKnifeInCircle(int number)
    {
        IsBlubLevel = true;
        Vector2 tempPos = Vector2.zero;
        int x = 0;
        List<int> index = new List<int>();
        while (x < number)
        {
            Transform indexTransform = transform.GetChild(0);
            int r = Random.Range(0, indexTransform.childCount);
            bool exists = false;
            foreach (int i in index)
            {
                if (i == r)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                GameObject go = Instantiate(KnifeObj, indexTransform.GetChild(r).transform) as GameObject;
                go.GetComponent<Knife>().enabled = false;
               // go.transform.LookAt(transform);
                index.Add(r);
                BonusObj.Add(go);
                go.transform.SetParent(indexTransform.GetChild(r).transform);
                go.transform.position = indexTransform.GetChild(r).position;
                x++;
            }
        }
    }
//Method to update the face based on level
    public void UpdateFace(int type)
    {
        //1-4 for the emoji level - 5 is the default image and 6 is bonus level
        if (type == 1)
        {
            GetComponent<SpriteRenderer>().sprite = HappyImages[Random.Range(0, HappyImages.Count)];
        }
        else if (type == 2)
        {
            GetComponent<SpriteRenderer>().sprite = NeutralImages[Random.Range(0, NeutralImages.Count)];
        }

        else if (type == 3)
        {
            GetComponent<SpriteRenderer>().sprite = SadImage[Random.Range(0, SadImage.Count)];
        }
        else if (type == 4)
        {
            GetComponent<SpriteRenderer>().sprite = DeadImage;
        }
        else if (type == 5)
        {
            GetComponent<SpriteRenderer>().sprite = DefaultImage;
        }
        else if (type == 6)
        {
            GetComponent<SpriteRenderer>().sprite = BonusLevelImage;
        }
        else if(type==7){

            GetComponent<SpriteRenderer>().sprite = ColorLevelImage;

        }
    }

    //Here we set the speed of the wheel and make it rotate
    void Update()
    {
        if (currentSpeed == Speed && IsAccelrating)
        {
            IsAccelrating = false;
            StartTime = Time.time;
        }
        if(StartTime + Duration < Time.time && !IsAccelrating)
        {
            if (currentCount == curve.Count - 1)
            {
                IsReverse = true;
            }
           if(currentCount == 0)
            {
                IsReverse = false;
            }
            if (IsReverse)
            {
                currentCount--;
            }
            else
            {
                currentCount++;
            }
            Speed = curve[currentCount].speed;
            Duration = curve[currentCount].duration;
            IsAccelrating = true;
        }
        if(IsAccelrating)
            currentSpeed = Mathf.Lerp(currentSpeed , Speed , 1f); //Using lerp to make the wheel rotate like an engine
        transform.Rotate(0, 0, currentSpeed * Time.deltaTime); 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Emoji)
        {
            GetComponent<Spawner>().SpawnObjects(5, collision.gameObject.transform, 1);
        }
        if(GameManager.instance.gameMode == GameManager.GameMode.Color){

            GetComponent<SpriteRenderer>().color = Random.ColorHSV(0.2f, 0.7f, 0.8f, 0.95f, 0.8f, 0.9f);

        }
    }
}
