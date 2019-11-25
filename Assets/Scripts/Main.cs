using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    int TimeInst = 0;
    public Vector3 CameraVec;
    public List<GameObject> ObstacleList = new List<GameObject>();
    public List<GameObject> MgcList = new List<GameObject>();
    void Start()
    {
        TimeInst = 100;
        CameraVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            TimeInst -= 1;
            if (TimeInst <= 0)
            {
                int InsRndm = Random.Range(0, ObstacleList.Count);
                Vector3 NewObsPos = new Vector3(CameraVec.x + transform.position.x, -2f, 0);
                if (InsRndm == 2)
                {
                    NewObsPos.y += 1.5f;
                }
                GameObject Obstacle = Instantiate(ObstacleList[InsRndm], NewObsPos, Quaternion.identity);
                Obstacle.GetComponent<ObsFrame>().EndWalk = -(CameraVec.x + transform.position.x+1f);
                TimeInst = 50+(Random.Range(0,10)*10);
            }
        }
    }
}
