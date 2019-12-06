using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainBG : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject otherMountain;
    public Main Mainscr;
    public Vector3 CameraVec;
    public float SpeedMountain;
    float AMountWidth;
    void Start()
    {
        AMountWidth = transform.GetComponent<ChildMount>().widthM;
        CameraVec = Mainscr.CameraVec;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            Vector3 newPos = transform.position;
            if (newPos.x <= -CameraVec.x * 2 + (CameraVec.x * 2 - AMountWidth))
            {
                newPos.x = otherMountain.transform.position.x + (AMountWidth);
            }
            transform.position = newPos + (new Vector3(-SpeedMountain * Time.deltaTime, 0, 0));
        }
    }
}
