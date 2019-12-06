using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsFrame : MonoBehaviour {
    //universal
    public int ObsId;//jenis Obstacle
    public int Life;
    public float Speed,EndWalk;
    public Main Mainsc;
    public GameObject playerGO;
    public GameObject Bomb;
    //Obstacle 0

    //Obstacle 1
    double grav = 0.098, velJump, TimeJump;
    float startY;
    bool JumPhase;

    //Obstacle 2
    bool Fly;
    int UpDown = 1;
    float FarFromMid,JiggleSpd,MidPos;
    bool SlideFlying = true;
    void Start()
    {
        switch (ObsId)
        {
            case 0://walker
                Life = 100;
                Speed = -3;
                break;
            case 1://jumper
                Life = 100;
                JumpBoong(0.1);
                Speed = -1;
                break;
            case 2://flying
                Life = 100;
                Speed = -3;
                Fly = true;
                SlideFlying = true;
                MidPos = transform.position.y;
                JiggleSpd = 0.8f;
                FarFromMid = 0.4f;
                break;
        }
        
    }
    void JumpBoong(double velJump)
    {
        JumPhase = true;
        this.velJump = velJump;
        this.TimeJump = 0;
        startY = transform.position.y;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Magic")
        {
            int pow = col.gameObject.GetComponent<MagicFrame>().pow;
            switch (col.gameObject.GetComponent<MagicFrame>().MagId)
            {
                case 2:
                    Speed = Speed/ 2;
                    break;
                default:
                    Life -= 50 * pow;
                    col.gameObject.GetComponent<MagicFrame>().pow -= 1;
                    break;
            }
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerScript>().MainSc.GameOver();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (SlideFlying)
            {
                Vector3 Newpos = transform.position;
                Newpos.y += JiggleSpd * Time.deltaTime* (float)UpDown;
                transform.position = Newpos;
                if(Mathf.Abs(transform.position.y-MidPos)> FarFromMid)
                {
                    UpDown = UpDown *- 1;
                }
            }
            if (Life <= 0)
            {
                Destroy(this.gameObject);
            }
            if(transform.position.x < EndWalk)
            {
                Destroy(gameObject);
            }
        }
        switch (ObsId)
        {
            case 0:
                if (Time.timeScale != 0f)
                {
                    transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
                }
                break;
            case 1:
                if (Time.timeScale != 0f)
                {
                    Vector3 newVec = transform.position + new Vector3(Speed * Time.deltaTime, 0, 0);
                    if (JumPhase)//jump boongan
                    {
                        newVec.y += (float)(velJump);
                        velJump -= grav * TimeJump;
                        TimeJump += 0.01;
                        if (newVec.y - 0.01 < startY)
                        {
                            newVec.y -= (newVec.y - startY);
                        }
                    }
                    transform.position = (newVec);
                    if (newVec.y == startY)
                    {
                        JumpBoong(0.1);
                    }
                }
                break;
            case 2:
                if(Time.timeScale != 0f)
                {
                    Vector3 newPos = (transform.position + new Vector3(Speed * Time.deltaTime, 0, 0));
                    if (Fly)
                    {
                        if (newPos.x < playerGO.transform.position.x)
                        {
                            newPos.x = playerGO.transform.position.x;
                            Instantiate(Bomb, transform.position, Quaternion.identity);
                            Fly = false;
                        }
                    }
                    transform.position = newPos;
                }
                break;
        }
    }
}
