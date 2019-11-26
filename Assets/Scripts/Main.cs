using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    int TimeInst = 0;
    public Vector3 CameraVec;
    public List<GameObject> ObstacleList = new List<GameObject>();
    public List<GameObject> ObstacleInArea = new List<GameObject>();
    public List<GameObject> MgcList = new List<GameObject>();

    public GameObject playerGO;
    public Vector3 PlayerStartPos;

    public Text ScoreText;
    public int Score = 0;
    void Start()
    {
        TimeInst = 100;
        CameraVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        PlayerStartPos = playerGO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();
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
                Score++;
                ObstacleInArea.Add(Obstacle);
                ObsFrame ObsScript = Obstacle.GetComponent<ObsFrame>();
                ObsScript.EndWalk = -(CameraVec.x + transform.position.x+1f);
                ObsScript.playerGO = playerGO;
                TimeInst = 50+(Random.Range(0,10)*10);
            }
        }
    }
}
