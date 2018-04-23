using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_Speed = 2f;
    public float m_LifeTime = 4f;
    private Vector3 m_TargetLocation;
    private Rigidbody2D m_Rigidbody;
    private SpriteRenderer m_SpriteRenderer;
    private Vector3 m_DirectionVector;
    private bool m_IsWoke;


    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_IsWoke = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if(m_IsWoke)
        {   
            var newPosition = transform.position;
            newPosition.x += m_DirectionVector.x * Time.deltaTime;
            newPosition.y += m_DirectionVector.y * Time.deltaTime;
            transform.position = newPosition;
        }

            //transform.position = Vector3.MoveTowards(transform.position, m_TargetLocation, m_Speed * Time.deltaTime);
	}

    public void SetTargetLocation(Vector3 pTarget)
    {
        m_TargetLocation = pTarget;
    }

    public IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(m_LifeTime);
        m_IsWoke = false;
        gameObject.layer = 1;
        m_Rigidbody.Sleep();
        m_SpriteRenderer.sortingLayerName = "Default";
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return m_Rigidbody;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return m_SpriteRenderer;
    }

    public void WakeUp()
    {
        m_DirectionVector = m_TargetLocation - transform.position;
        m_DirectionVector.Normalize();
        m_DirectionVector *= m_Speed;
        m_IsWoke = true;
        gameObject.layer = 8;
    }

    public bool IsWoke()
    {
        return m_IsWoke;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if(null != player)
            player.Die();
    }
}
