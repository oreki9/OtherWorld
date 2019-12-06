using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMount : MonoBehaviour {

    public GameObject rightM,leftM;
    public float widthM;
    void Start()
    {
        if ((rightM) && (leftM))
        {
            float rigthWidth = rightM.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float leftWidth = leftM.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float jarakRL = Mathf.Abs(rightM.transform.position.x - leftM.transform.position.x);
            widthM = leftWidth + jarakRL + rigthWidth;
        }
        else
        {
            widthM = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }
}
