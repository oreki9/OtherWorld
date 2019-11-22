using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsFrame : MonoBehaviour {
    //universal
    public int ObsId;//jenis Obstacle
    public int Life, TimeWatch;
    //Obstacle 0

    //Obstacle 1
    double grav = 0.098, velJump, TimeJump;
    float startY;
    bool JumPhase;


    void Start()
    {
        switch (ObsId)
        {
            case 0:
                Life = 100;
                TimeWatch = 250;
                break;
            case 1:
                Life = 100;
                TimeWatch = 500;
                JumpBoong(0.3, 0);
                break;
        }
        
    }
    void JumpBoong(double velJump, double TimeJump)
    {
        JumPhase = true;
        this.velJump = velJump;
        this.TimeJump = TimeJump;
        startY = transform.position.y;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Magic")
        {
            int pow = col.gameObject.GetComponent<MagicFrame>().pow;
            Debug.Log(pow);
            Life -= 50 * pow;
            col.gameObject.GetComponent<MagicFrame>().pow -= 1;
            switch (ObsId)
            {
                case 0:
                    break;
                case 1:
                    break;
            }
        }
        if (col.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            switch (ObsId){
                case 0:
                    break;
                case 1:
                    
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            TimeWatch -= 1;
            if (TimeWatch == 0)
            {
                Destroy(gameObject);
            }
            if (Life <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        switch (ObsId)
        {
            case 0:
                if (Time.timeScale != 0f)
                {
                    transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));
                }
                break;
            case 1:
                if (Time.timeScale != 0f)
                {
                    Vector3 newVec = transform.position + new Vector3(-2 * Time.deltaTime, 0, 0);
                    if (JumPhase)//jump boongan
                    {
                        newVec.y += (float)(velJump);
                        velJump -= grav * TimeJump;
                        TimeJump += 0.05;
                        if (newVec.y - 0.05 < startY)
                        {
                            newVec.y -= newVec.y - startY;
                        }
                    }
                    transform.position = (newVec);
                    if (newVec.y == startY)
                    {
                        JumpBoong(0.3, 0);
                    }
                }
                break;
        }
    }
}
