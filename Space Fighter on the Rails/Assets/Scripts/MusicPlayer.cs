using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {




    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;

        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Use this for initialization
    void Start () {
        Invoke("LoadFirstScene", 5f);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
