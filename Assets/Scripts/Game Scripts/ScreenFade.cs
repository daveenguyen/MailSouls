using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    private void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        var spriteRenderer = GetComponent<SpriteRenderer>();
        for (float f = 0f; f < 1; f += 0.1f)
        {
            var color = spriteRenderer.color;
            color.a = f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().RestartingGame());
    }
}
