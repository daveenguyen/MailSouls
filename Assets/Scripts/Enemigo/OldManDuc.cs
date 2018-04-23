using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManDuc : MonoBehaviour
{
    //public float m_MoveSpeed = 3f;
    public float m_ShotCooldown = 3f;
    private bool m_ShotOnCooldown = false;
    private bool m_Preshoot = false;
    private GameObject m_Target;
    private const int BULLET_ARRAY_INDEX = 0;

    private Animator m_Animator;
    private int SHOOT_HASH = Animator.StringToHash("Shoot");


    //public float m_MinXLocation = -5f;
    //public float m_MaxXLocation = 5f;
    private void Awake()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player");
        //m_MinXLocation += transform.position.x;
        //m_MaxXLocation += transform.position.x;
        m_Animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!GameMaster.Pause)
            if (!m_ShotOnCooldown)
                ShootAtTarget();
	}

    IEnumerator ShotOnCooldown()
    {
        m_ShotOnCooldown = true;
        yield return new WaitForSeconds(m_ShotCooldown);
		FindObjectOfType<AudioManager> ().Play ("boss");
        m_ShotOnCooldown = false;
    }

    IEnumerator Preshoot()
    {
        m_Preshoot = true;
        yield return new WaitForEndOfFrame();
        m_Preshoot = false;
    }

    public void ShootAtTarget()
    {
        m_Animator.SetTrigger(SHOOT_HASH);


        if (!m_Preshoot)
        { 
            var direction = m_Target.transform.position - transform.position;
            const float MIN_VALUE = -3;
            const float MAX_VALUE = 3;
            var minX = (direction.x < 0) ? MIN_VALUE : 0;
            var maxX = (direction.x > 0) ? MAX_VALUE : 0;
            var minY = (direction.y < 0) ? MIN_VALUE : 0;
            var maxY = (direction.y > 0) ? MAX_VALUE : 0;
            for (int i = 0; i < transform.GetChild(BULLET_ARRAY_INDEX).childCount; ++i)
            {
                var projectile = transform.GetChild(BULLET_ARRAY_INDEX).GetChild(i).GetComponent<Projectile>();
                //var rb = projectile.GetRigidbody2D();
                projectile.transform.position = transform.position;


                projectile.SetTargetLocation(m_Target.transform.position + new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)));
                projectile.GetSpriteRenderer().sortingLayerName = "Player";
                projectile.WakeUp();
                FindObjectOfType<AudioManager>().Play("gunshot");
                projectile.StartCoroutine(projectile.WaitForDeath());
                var vectorToTarget = m_Target.transform.position - transform.position;

                //var vectorToTarget = transform.position - GetTarget().transform.position;
                //var vectorToTarget = GetTarget().transform.position - projectile.transform.position;
                var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

                var qt = Quaternion.AngleAxis(angle, Vector3.forward);
                projectile.transform.rotation = qt;

                //projectile.transform.LookAt(GetTarget().transform);
                //projectile.GetRigidbody2D().velocity = new Vector2(Mathf.Cos(angle + Mathf.Deg2Rad * (22.5f * i)) * projectile.m_Speed, Mathf.Sin(angle + Mathf.Deg2Rad * (22.5f * i)) * projectile.m_Speed);
                //projectile.transform.Translate(0, projectile.m_Speed, 0);
            }

            StartCoroutine(ShotOnCooldown());
        }
    }
}
