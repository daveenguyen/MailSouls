using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().Win();
            StartCoroutine(GameObject.FindGameObjectWithTag("YouWin").GetComponent<ScreenFade>().FadeIn());
			FindObjectOfType<AudioManager> ().Play ("mail");
            GameMaster.Pause = true;
        }
    }
}
