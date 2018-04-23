using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler : MonoBehaviour
{
    private GameObject m_ShotTarget;

    public float m_ShotCooldown = 1f;
    private bool m_ShotOnCooldown;

    private void Awake()
    {
        StartCoroutine(DelayBeforeWakeup());
    }

    // Use this for initialization
    void Start ()
    {
        
        m_ShotTarget = transform.GetChild(0).gameObject;
        var childCount = transform.GetChild(3).childCount;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameMaster.Pause && !m_ShotOnCooldown)
        {
            for (int i = 0; i < transform.GetChild(3).childCount; ++i)
            {
                var projectile = transform.GetChild(3).GetChild(i).GetComponent<Projectile>();
                var rb = projectile.GetRigidbody2D();
                if (!projectile.IsWoke())
                {
                    rb.transform.position = transform.position;
                    projectile.SetTargetLocation(m_ShotTarget.transform.position);
                    projectile.GetSpriteRenderer().sortingLayerName = "Player";
                    projectile.WakeUp();
					FindObjectOfType<AudioManager> ().Play ("sprinkler");
                    projectile.StartCoroutine(projectile.WaitForDeath());
                    StartCoroutine(ShotOnCooldown());
                    break;
                }
            }
        }
	}

    IEnumerator ShotOnCooldown()
    {
        m_ShotOnCooldown = true;
        yield return new WaitForSeconds(m_ShotCooldown);
        m_ShotOnCooldown = false;
    }

    IEnumerator DelayBeforeWakeup()
    {
        m_ShotOnCooldown = true;
        yield return new WaitForSeconds(Random.Range(0, 2));
        m_ShotOnCooldown = false;
    }
}
