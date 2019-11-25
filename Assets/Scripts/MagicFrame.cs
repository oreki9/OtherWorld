using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFrame : MonoBehaviour {
    
    public int pow, MagId,TimeLast;
    public GameObject player;
    //MagId 1
    int time = 100;


    //Magid 3
    public GameObject TargetPos;
    public float speed;
    void Start()
    {
        switch (MagId)
        {
            case 0://bullet
                TimeLast = 200;
                pow = 2;
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
                speed = 3;
                break;
            case 4:
                pow = 100;
                TimeLast = 10;
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
                    if (Time.timeScale != 0f)
                    {
                        transform.Translate(new Vector3(6f * Time.deltaTime, 0, 0));
                    }
                    break;
                case 1:
                    if (Time.timeScale != 0f)
                    {
                        time--;
                        if (time >= time * 7 / 10)
                        {
                            transform.localScale += new Vector3(1, 0.8f, 0) * Time.deltaTime;
                            transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
                            transform.position = transform.position + new Vector3(6f * Time.deltaTime, 0, 0);
                        }
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
                        newPos = newPos + transform.right * ((float)speed / 10);
                        transform.rotation = Quaternion.Euler(0, 0, Vector);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                    transform.position = newPos;
                    break;
                case 4:

                    break;
            }
        }
    }
    
}
