using UnityEngine;

public class FieldOfVision : MonoBehaviour
{
    
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var dog = GetComponentInParent<Dog>();
        //dog.SetTarget(collision.gameObject);
        //dog.SetReturning(false);
		if (collision.CompareTag ("Player")) {
			FindObjectOfType<AudioManager> ().Play ("bark");
		}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var dog = GetComponentInParent<Dog>();
            if (dog.CanChase)
            {
                dog.Returning = false;
                dog.SetTarget(collision.gameObject);
            }
            //dog.SetReturning(false);
        }
    }
}
