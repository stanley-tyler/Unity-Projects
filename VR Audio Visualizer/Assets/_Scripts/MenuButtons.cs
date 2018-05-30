using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using System.IO;

public enum FBAction {
   UpDirectory, NextPage, PrevPage, Toggle, Pause, Play, Mic
}
namespace VRStandardAssets.Examples
{
    public class MenuButtons : MonoBehaviour {

        [SerializeField] private Color normalColor;                
        [SerializeField] private Color overColor;                  
        [SerializeField] private Color clickedColor;               
        // [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        public FBAction setAction;
        // [SerializeField] private Renderer m_Renderer;
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
        }


        //Handle the Out event
        private void HandleOut()
        {
            Text text = gameObject.GetComponent<Text>();
            text.color = normalColor;
        }


        //Handle the Click event
        private void HandleClick()
        {
            Text text = gameObject.GetComponent<Text>();
            text.color = clickedColor;
            ListFiles ls = GameObject.FindGameObjectWithTag("FileBrowser").GetComponent<ListFiles>();
            switch (setAction)
            {
                case FBAction.UpDirectory:
                    ls.Up();
                    break;
                case FBAction.NextPage:
                    ls.Next();
                    break;
                case FBAction.PrevPage:
                    ls.Prev();
                    break;
                case FBAction.Toggle:
                    ls.ToggleFileBrowser();
                    break;
                case FBAction.Pause:
                    GameObject.FindGameObjectWithTag("Music")
                    .GetComponent<AudioSource>().Pause();
                    break;
                case FBAction.Play:
                    GameObject.FindGameObjectWithTag("Music")
                    .GetComponent<AudioSource>().Play();
                    break;
                case FBAction.Mic:
                    ActivateMic();
                    break;
                default:
                    break;
            }
        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
        }

        private void ActivateMic(){
            AudioSource audio = GameObject.FindGameObjectWithTag("Music")
            .GetComponent<AudioSource>();
            audio.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
            audio.loop = true;
            while(!(Microphone.GetPosition(null) > 0)){}
            audio.Play();
        }

    }
}

