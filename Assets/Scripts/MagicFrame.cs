using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFrame : MonoBehaviour {
    
    public int pow;
    public int MagId;

    //MagId 1
    int time = 100;
    void Start()
    {
        switch (MagId)
        {
            case 0://bullet
                pow = 1;
                break;
            case 1://burst
                pow = 2;
                break;
            default:
                pow = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
    
}
