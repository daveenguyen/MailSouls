using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    //private List<Tilemap> m_ActiveTiles;

    //private Tilemap m_StarterTile;

    private static bool m_Created;
    public static bool Pause
    {
        get;
        set;
    }

    private bool m_CanStart;

    void Awake()
    {
        if(!m_Created)
        {
            DontDestroyOnLoad(gameObject);
            m_Created = true;
            m_CanStart = true;
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
        }
    }

    // Use this for initialization
    void Start ()
    {
        //m_ActiveTiles = new List<Tilemap>(9);
        m_Created = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(m_CanStart)
        {
            if(Input.anyKeyDown)
            {
                Pause = false;
                m_CanStart = false;
                SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
            }
        }
	}

    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
    }

    public IEnumerator RestartingGame()
    {
        Pause = true;
        m_CanStart = false;
        yield return new WaitForSeconds(2);
        m_CanStart = true;
    }

}
