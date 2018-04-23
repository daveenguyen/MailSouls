using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //private Vector3 m_Velocity;
    private Transform m_Player;
    //private float m_DampTime = 0.15f;
    //private float m_Speed = 2.0f;
    //private float m_DeltaTime = 1f;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (null != m_Player)
            TrackPlayer();
    }

    void TrackPlayer()
    {
        //var camera = GetComponent<Camera>();
        //var point = camera.WorldToViewportPoint(m_Player.position);
        //var delta = m_Player.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        //var destination = transform.position + delta;
        //transform.position = Vector3.SmoothDamp(transform.position, destination, ref m_Velocity, m_DampTime);

        //var interpolation = m_Speed * Time.deltaTime;

        //var position = transform.position;
        //position.y = Mathf.Lerp(transform.position.y, m_Player.transform.position.y, interpolation);
        //position.x = Mathf.Lerp(transform.position.x, m_Player.transform.position.x, interpolation);
        //transform.position = position;

        //var newPosition = m_Player.position;
        //newPosition.z = transform.position.z;
        //transform.position = Vector3.Slerp(transform.position, newPosition, m_Speed * Time.deltaTime);

        var newPosition = transform.position;
        newPosition.x = 0;
        transform.position = newPosition;
    }
}
