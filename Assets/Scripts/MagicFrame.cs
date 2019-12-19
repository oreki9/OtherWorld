using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFrame : MonoBehaviour {
    
    public int pow, MagId,TimeLast;
    public GameObject player;
    public float speed;
    //MagId 1
    int time = 100;
    
    //Magid 3
    public GameObject TargetPos;
    void Start()
    {
        switch (MagId)
        {
            case 0://bullet
                TimeLast = 200;
                pow = 2;
                speed = 10;
                break;
            case 1://burst
                pow = 4;
                TimeLast = 200;
                break;
            case 2://freeze
                pow = 8;
                TimeLast = 50;
                break;
            case 3://hooming
                pow = 1;
                TimeLast = 100;
                speed = 2.5f;
                break;
            case 4://Messiah
                pow = 100;
                TimeLast = 10;
                break;
            case 5://At-Field
                pow = 8;
                TimeLast = 400;
                break;
            case 6://Rain
                pow = 1;
                TimeLast = 100;
                speed = 5;
                break;
            case 7://Fly
                TimeLast = 200;
                pow = 1;
                break;
            default:
                TimeLast = 100;
                pow = 1;
                break;
        }
    }

    float LookAt(Vector3 pos1, Vector3 pos2)
    {
        Vector3 newStartPosition = pos1;
        Vector3 newEndPosition = pos2;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        //2
        float x = newDirection.x;
        float y = newDirection.y;
        return Mathf.Atan2(y, x) * 180 / Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            TimeLast -= 1;
            if (TimeLast <= 0)
            {
                Destroy(gameObject);
            }
            if (pow <= 0)
            {
                Destroy(this.gameObject);
            }
            switch (MagId)
            {
                case 0:
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    break;
                case 1:
                    time--;
                    if (time >= time * 8 / 10)
                    {
                        if (player != null)
                        {
                            Vector3 NewPos = player.transform.position;
                            NewPos.z = -0.1f;
                            transform.position = NewPos;
                        }
                        transform.localScale += new Vector3(2, 2f, 0) * Time.deltaTime;
                        transform.Rotate(new Vector3(0, 0, 250) * Time.deltaTime);
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
                        transform.position = transform.position + new Vector3(6f * Time.deltaTime, 0, 0);
                    }
                    break;
                case 2:
                    if (Time.timeScale != 0f)
                    {

                    }
                    break;
                case 3:
                    Vector3 newPos = transform.position;
                    if (TargetPos != null)
                    {
                        float Vector = LookAt(newPos, TargetPos.transform.position);
                        newPos = newPos + transform.right * ((float)speed / 10) ;
                        transform.rotation = Quaternion.Euler(0, 0, Vector);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                    transform.position = newPos;
                    break;
                case 5:
                    if (player != null)
                    {
                        Vector3 NewPos = player.transform.position;
                        transform.position = new Vector3(NewPos.x, NewPos.y,transform.position.z);
                    }
                    break;
                case 6:
                    Vector3 newpos = transform.position;
                    newpos = newpos - transform.up * ((float)speed / 10);
                    transform.position = newpos;
                    break;
                case 7:
                    transform.position = player.transform.position;
                    break;
            }
        }
    }
    
}
