using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerScript>().MainSc.GameOver();
        }
    }
        // Update is called once per frame
    void Update () {
        transform.position = transform.position - (new Vector3(0, 4, 0) * Time.deltaTime);
	}
}
