using System.Collections;
using UnityEngine;

public class Dog : MonoBehaviour
{
    private GameObject m_Target;
    private GameObject m_LeashOrigin;
    //private Vector3 m_LeashOrigin;
    public float m_LeashRange = 5f;
    public float m_ChaseSpeed = 4f;
    public float m_RotationSpeed = 3f;
    public float m_ReturnSpeed = 1f;
    public float m_IdleTime = 3f;

    private bool m_Returning;
    private bool m_StandingIdle;
    private bool m_CanChase;
	// Use this for initialization
	virtual public void Start ()
    {
        //m_LeashOrigin = transform.position;
        m_LeashOrigin = transform.parent.gameObject;
        m_Target = null;
        m_Returning = false;
        m_StandingIdle = false;
        m_CanChase = true;
	}
	
	// Update is called once per frame
	virtual public void Update ()
    {
		if(null != m_Target)
        {
            var distance = Vector3.Distance(m_LeashOrigin.transform.position, transform.position);
            if (!m_StandingIdle && !m_Returning && distance > m_LeashRange)
            {
                StartCoroutine(StandIdle());
            }

            if (!m_StandingIdle)
            {
                var moveSpeed = (m_Returning) ? m_ReturnSpeed : m_ChaseSpeed;

                transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, moveSpeed * Time.deltaTime);
                var vectorToTarget = m_Target.transform.position - transform.position;
                var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                var qt = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * m_RotationSpeed);

                if (distance < m_LeashRange * 0.8f)
                {
                    m_StandingIdle = false;
                }
            }
        }
	}

    public void SetTarget(GameObject pTarget)
    {
        m_Target = pTarget;
    }

    public GameObject GetTarget()
    {
        return m_Target;
    }

    public bool Returning
    {
        get
        {
            return m_Returning;
        }
        set
        {
            m_Returning = value;
        }
    }

    public bool CanChase
    {
        get
        {
            return m_CanChase;
        }
        set
        {
            m_CanChase = value;
        }
    }

    IEnumerator StandIdle()
    {
        m_StandingIdle = true;
        yield return new WaitForSeconds(1);
        m_StandingIdle = false;
        SetTarget(m_LeashOrigin);
        StartCoroutine(ReturnToPole());
    }

    IEnumerator ReturnToPole()
    {
        Returning = true;
        CanChase = false;
        yield return new WaitForSeconds(m_IdleTime);
        CanChase = true;
    }

    public bool IsStandingIdle()
    {
        return m_StandingIdle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var pc = collision.gameObject.GetComponent<PlayerController>();
            pc.Die();
        }
    }
}
