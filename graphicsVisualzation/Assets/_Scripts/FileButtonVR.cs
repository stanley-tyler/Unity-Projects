using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using System.IO;

    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
public class FileButtonVR : MonoBehaviour
{
    [SerializeField] private Color normalColor;                
    [SerializeField] private Color overColor;                  
    [SerializeField] private Color clickedColor;               
    // [SerializeField] private Material m_DoubleClickedMaterial;         
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    // [SerializeField] private Renderer m_Renderer;
    [SerializeField] public string path;
    [SerializeField] public bool isDirectory;


    private void Awake ()
    {
        // m_Renderer.material = m_NormalMaterial;
    }


    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
    }


    //Handle the Over event
    private void HandleOver()
    {
        Text text = gameObject.GetComponent<Text>();
        text.color = overColor;
        // Debug.Log("Show over state");
        // m_Renderer.material = m_OverMaterial;
    }


    //Handle the Out event
    private void HandleOut()
    {
        Text text = gameObject.GetComponent<Text>();
        text.color = normalColor;
        // Debug.Log("Show out state");
        // m_Renderer.material = m_NormalMaterial;
    }


    //Handle the Click event
    private void HandleClick()
    {
        Text text = gameObject.GetComponent<Text>();
        text.color = clickedColor;
        if(isDirectory){
            ListFiles ls = GameObject.FindGameObjectWithTag("FileBrowser").GetComponent<ListFiles>();
            ls.EnterDirectory(path);
        } else {
            LoadSongPath(path);
        }
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        // Debug.Log("Show double click");
        // m_Renderer.material = m_DoubleClickedMaterial;
    }

    public void LoadSongPath(string songPath){
        string forward = songPath.Replace("\\", @"/").Replace(" ", "%20");
        string p = "file://" + forward;
        WWW www = new WWW(p);
        while(!www.isDone){ Debug.Log("Loading..."); }
        AudioSource source;
        source = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        source.clip = www.GetAudioClip(false, true);
        if (!string.IsNullOrEmpty(www.error)){
            Debug.Log("WWW ERROR: " + www.error);
        }
        else{
            source.Play();
        }
    }
}