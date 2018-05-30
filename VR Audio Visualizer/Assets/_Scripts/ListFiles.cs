using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class ListFiles : MonoBehaviour {

    // Use this for initialization
    protected bool needUpdate = false;
    protected int updateCell = 0;
    protected DirectoryInfo currentDirectory;
    protected DirectoryInfo[] folders;
    protected FileInfo[] files;
    protected List<FileSystemInfo> fileSystem = new List<FileSystemInfo>();
    protected int startingIndex = 0;
    protected string myPath = "/storage/emulated/0/Download/";
    protected bool toggledOn = true;
    // private FileButtonVR fileButtonVR;
    protected VRStandardAssets.Utils.VRInput VRInput;                     // Reference to the VRInput in order to know when Cancel is pressed.

    void Start () {
        #if UNITY_EDITOR
            myPath = "C:/Users/Gisela/Downloads/";
        #endif
        Debug.Log(myPath);

        #if UNITY_ANDROID
            // UniAndroidPermission.RequestPremission (AndroidPermission.READ_EXTERNAL_STORAGE, () => {
            //     // add permit action
            // }, () => {
            //     // add not permit action
            //     Application.Quit();
            // });
        #endif
        currentDirectory = new DirectoryInfo(myPath);
        // VRInput.OnCancel += ToggleFileBrowser;
        // UNITY_ANDROID.Livestreaming.GetStatus();
        UpdateUI();
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel")){
            ToggleFileBrowser();
        }
    }

    void UpdateUI() {
        // Debug.Log(myPath);
        currentDirectory = new DirectoryInfo(myPath);
        // Debug.Log(currentDirectory.FullName);
        FileInfo[] mp3 = currentDirectory.GetFiles("*.mp3");
        FileInfo[] m4a = currentDirectory.GetFiles("*.m4a");
        folders = currentDirectory.GetDirectories();
        // Debug.Log("Prev FS Count:" + fileSystem.Count);
        fileSystem.Clear();
        fileSystem.AddRange(mp3);
        fileSystem.AddRange(m4a);
        fileSystem.AddRange(folders);
        // Debug.Log("Current FS Count" + fileSystem.Count);
        // for(int i = 0; i < this.gameobject.transform.GetChildCount(); i++)
        // {
        //    GameObject Go = this.gameobject.transform.GetChild(i);
        // }
        GameObject list = GameObject.Find("List");
        for (int i = 0; i < list.transform.childCount; i++){
            Transform button = list.transform.GetChild(i).GetChild(0); 
            Text cell;
            if(button)
                cell = button.gameObject.GetComponent<Text>();
            else{
                updateCell = i;
                continue;
            }
            if(startingIndex+i < fileSystem.Count){
                FileSystemInfo fs = fileSystem[startingIndex+i];
                if(fs.GetType().Name == "DirectoryInfo"){
                    cell.text = "/" + fs.Name;
                    // button.GetComponent<Button>().onClick.RemoveAllListeners();
                    // button.GetComponent<Button>().onClick.AddListener ( () => EnterDirectory(fs.Name));
                    if(button.GetComponent<FileButtonVR>() != null){
                        button.GetComponent<FileButtonVR>().path = fs.Name;
                        button.GetComponent<FileButtonVR>().isDirectory = true;
                        // Debug.Log("PATH now is " + button.GetComponent<FileButtonVR>().path);
                    }
                }
                else{
                    cell.text = fs.Name;
                    // button.GetComponent<Button>().onClick.RemoveAllListeners();
                    // button.GetComponent<Button>().onClick.AddListener ( () => LoadSongPath(fs.FullName));
                    if(button.GetComponent<FileButtonVR>() != null){
                        button.GetComponent<FileButtonVR>().path = fs.FullName;
                        button.GetComponent<FileButtonVR>().isDirectory = false;
                        // Debug.Log("PATH now is " + button.GetComponent<FileButtonVR>().path);
                    }
                }
            } else {
                cell.text = "";
                // button.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }
    public void Next(){
        startingIndex = (startingIndex + 9) % fileSystem.Count;
        UpdateUI();
    }

    public void Prev(){
        startingIndex = ((startingIndex - 9) >= 0) ? (startingIndex - 9) : 0;
        UpdateUI();
    }

    public void Up(){
        myPath = Path.GetFullPath(Path.Combine(myPath, "../"));
        UpdateUI();
    }

    public void EnterDirectory(string newDirectory){
        myPath = Path.GetFullPath(Path.Combine(myPath, newDirectory));
        Debug.Log("Set Entering folder: " + newDirectory);
        Debug.Log("Set path: " + myPath);
        startingIndex = 0;
        UpdateUI();
    }

    public void LoadSongPath(string songPath){
        string forward = songPath.Replace("\\", @"/").Replace(" ", "%20");
        string p = "file://" + forward;
        Debug.Log("Preescaped: " + p);
        WWW www = new WWW(p);
        while(!www.isDone){
            Debug.Log(p + "Loading...");
        }
        AudioSource source;
        source = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        // while()
        source.clip = www.GetAudioClip(false, true);

        if (!string.IsNullOrEmpty(www.error)){
            Debug.Log("WWW ERROR: " + www.error);
        }
        else{
            Debug.Log(p + " Loaded!");
            source.Play();
            ToggleFileBrowser();
        }
    }

    public void ToggleFileBrowser(){
        GameObject fb = GameObject.Find("FileBrowser");
        // for(int i = 0; i < fb.transform.childCount; i++){
        //     fb.transform.GetChild(i).gameObject.SetActive(false);
        // }
        // toggledOn = false;
        // Debug.Log("Calling toggle");
        if(toggledOn){
            Debug.Log("Toggled Off");
            for(int i = 0; i < fb.transform.childCount; i++){
                fb.transform.GetChild(i).gameObject.SetActive(false);
            }
            toggledOn = false;
        }
        else{
            Debug.Log("Toggled On");
            for(int i = 0; i < fb.transform.childCount; i++){
                fb.transform.GetChild(i).gameObject.SetActive(true);
            }
            toggledOn = true;
        }
    }
}


