using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject fileSystemPanel;
    public GameObject VRButton;
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Play(string audioTag)
    {
        Debug.Log("Play Called!");
        GameObject.FindGameObjectWithTag(audioTag)
        .GetComponent<AudioSource>().Play();
    }
    public void Pause(string audioTag)
    {
        Debug.Log("Pause Called!");
        GameObject.FindGameObjectWithTag(audioTag)
            .GetComponent<AudioSource>().Pause();
    }
    public void Restart(string audioTag)
    {
        Debug.Log("Restart Called!");
        GameObject.Find("DebugString").GetComponent<Text>().text = "Restart Called!";
        GameObject.FindGameObjectWithTag(audioTag)
            .GetComponent<AudioSource>().Stop();
    }
    public void GetPath()
    {
        Text debugUI = GameObject.Find("DebugString").GetComponent<Text>();
        string myPath = "C:/Users/Gisela/Downloads/";
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.mp3");
        foreach (FileInfo f in info) 
        {
            GameObject obj = Instantiate(VRButton) as GameObject;
            obj.transform.parent = fileSystemPanel.transform;
            debugUI.text += f.FullName;
            Debug.Log(f.DirectoryName);
        }
    }
    public void LoadSong()
    {
        Text debugUI = GameObject.Find("DebugString").GetComponent<Text>();
        string path = "file:///storage/emulated/0/Download/dmsnal.mp3";
        string myPath;
        #if UNITY_EDITOR
            path = "file:///C:/Users/Gisela/Downloads/Avicii%20-%20Levels.mp3";
            myPath = "C:/Users/Gisela/Downloads/";
        #endif
        WWW www = new WWW(path);
        while(!www.isDone){
            debugUI.text = "Loading...";
        }
        // AudioClip f = WWWAudioExtensions.GetAudioClip(www, true, true, AudioType.MPEG);
        // AudioClip = www.audioClip;
        AudioSource source;
        source = GameObject.Find("Speaker").GetComponent<AudioSource>();
        // while()
        source.clip = www.GetAudioClip();

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            debugUI.text = www.error;
        }
        else
        {
            debugUI.text = path + " Loaded!";
        }
    }
}
