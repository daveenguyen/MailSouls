using UnityEngine;

public class SprinklerTarget : MonoBehaviour
{
    public float m_Speed = 1f;
    private Transform[] m_Targets;
    private Transform m_MaxTarget;
    private Transform m_MinTarget;
    private int m_Target = 0;
    //private Transform m_Target;
    private Vector3 m_PreviousPosition;
	// Use this for initialization
	void Start ()
    {
        m_MaxTarget = transform.parent.GetChild(1);
        m_MinTarget = transform.parent.GetChild(2);
        m_Targets = new Transform[2] { m_MaxTarget, m_MinTarget };

        //m_Target = m_MaxTarget;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_PreviousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, m_Targets[m_Target].transform.position, m_Speed * Time.deltaTime);
        if (transform.position.Equals(m_PreviousPosition))
        {
            SwapTarget();
        }
    }

    void SwapTarget()
    {
        m_Target++;
        m_Target %= 2;
    }
}
