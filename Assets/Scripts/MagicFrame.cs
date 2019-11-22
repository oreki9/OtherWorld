using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFrame : MonoBehaviour {
    
    public int pow, MagId,TimeLast;

    //MagId 1
    int time = 100;
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
            default:
                TimeLast = 100;
                pow = 1;
                break;
        }
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
            }
        }
    }
    
}
