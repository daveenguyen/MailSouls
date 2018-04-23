using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_Animator;
    private float m_MoveSpeed = 3;

    private const int STAMINA_DRAIN = 5;
    private const int STAMINA_RECOVER = 2;
    private const int STAMINA_MAX = 1000;
    public int m_Stamina = 1000;

    private bool m_OutOfBreath;
    private bool m_CanMove;

    private int m_IdleHash = Animator.StringToHash("Idle");
    private int m_VertSpeedHash = Animator.StringToHash("vertSpeed");
    private int m_HorizSpeedHash = Animator.StringToHash("horizSpeed");

    // Use this for initialization
    void Start ()
    {
        m_OutOfBreath = false;
        m_CanMove = true;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        if (m_CanMove)
        {
            var vel = GetComponent<Rigidbody2D>().velocity;
            vel.x = 0f;
            vel.y = 0f;
            GetMoveSpeed(ref vel.x, ref vel.y);
            if (vel.x == 0 && vel.y == 0)
                m_Animator.SetBool(m_IdleHash, true);
            else
                m_Animator.SetBool(m_IdleHash, false);
            m_Animator.SetFloat(m_VertSpeedHash, vel.y);
            m_Animator.SetFloat(m_HorizSpeedHash, vel.x);

            GetComponent<Rigidbody2D>().velocity = vel;
        }
    }

    void GetMoveSpeed(ref float horizontalSpeed, ref float verticalSpeed)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            horizontalSpeed -= m_MoveSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
            horizontalSpeed += m_MoveSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
            verticalSpeed += m_MoveSpeed;
        if (Input.GetKey(KeyCode.DownArrow))
            verticalSpeed -= m_MoveSpeed;

        //Sprint
        //if (!m_OutOfBreath && Input.GetKey(KeyCode.J))
        //{
        //    verticalSpeed *= 2;
        //    horizontalSpeed *= 2;
        //    m_Stamina -= STAMINA_DRAIN;
        //    if (m_Stamina <= 0)
        //    {
        //        m_OutOfBreath = true;
        //        m_Stamina = 0;
        //    }
        //}
        //else
        //{
        //    m_Stamina += STAMINA_RECOVER;
        //    if (m_Stamina >= STAMINA_MAX)
        //    {
        //        m_Stamina = STAMINA_MAX;
        //        m_OutOfBreath = false;
        //    }
        //}
    }

    public void Die()
    {
		m_MoveSpeed = 0f;
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		FindObjectOfType<AudioManager> ().Play ("die");
        StartCoroutine(GameObject.FindGameObjectWithTag("GameOver").GetComponent<ScreenFade>().FadeIn());
        GameMaster.Pause = true;
    }
}
