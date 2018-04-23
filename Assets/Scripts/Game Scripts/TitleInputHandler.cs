using UnityEngine;

public class TitleInputHandler : MonoBehaviour
{
    private GameObject m_GameMaster;
	// Use this for initialization
	void Start ()
    {
        m_GameMaster = GameObject.FindGameObjectWithTag("GameMaster");
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKeyDown)
        {
            var gm = m_GameMaster.GetComponent<GameMaster>();
            gm.StartGame();
        }
	}
}
