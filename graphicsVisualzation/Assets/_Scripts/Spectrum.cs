using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour {

    public GameObject prefab;
    public int numberOfObjects = 256;
    public float radius = 150f;
    public float intensity = 1500;
    public GameObject[] cubes;
    private float offset = 0f;
    public float a = 0.9983f;
    public float b = 0.5063f;
    public float distance = 2f;
    public float height = 2f;
    protected GameObject leftCubes;
    protected GameObject rightCubes;
    protected GameObject skyCubes;
    protected GameObject planets;
    public float skyCubesIntensityMod = 1;
    public float baseMultiplier = 1000;
    float[] spectrumMaxs = new float[256];
    ParticleSystem rainbowParticles;
    ParticleSystem.EmissionModule emission;
    ParticleSystem.MainModule main;
    ParticleSystem waveform;
    ParticleSystem stars;
    Transform starsTransform;
    bool triggerDown;

    void Start()
    {
        leftCubes = GameObject.Find("LeftCubes");
        rightCubes = GameObject.Find("RightCubes");
        skyCubes = GameObject.Find("SkyCubes");
        planets = GameObject.Find("Planets");
        rainbowParticles = GameObject.Find("Flare04-ColorBurst").GetComponent<ParticleSystem>();
        emission = rainbowParticles.emission;
        main = rainbowParticles.main;
        waveform = GameObject.Find("WaveForm").GetComponent<ParticleSystem>();
        triggerDown = false;
        stars = GameObject.Find("Flare02").GetComponent<ParticleSystem>();
        starsTransform = GameObject.Find("Flare02").transform;
        for (int i = 0; i < numberOfObjects; i++)
        {
            spectrumMaxs[i] = 0;
            
            // float angle = i * Mathf.PI / numberOfObjects;
            float hue = i/((float)numberOfObjects-1);
            // Debug.Log("Hue = " + hue);
            Color color = Color.HSVToRGB(hue, 1, 1);

            float ang = (i/(float)numberOfObjects) * Mathf.PI * 2f;
            Vector3 pos = new Vector3(Mathf.Cos(ang), 0, Mathf.Sin(ang))*5f;
            GameObject cube = Instantiate(prefab, pos, Quaternion.Euler(0, -360 * (i/(float)numberOfObjects), 0));
            cube.GetComponent<Renderer>().material.color = color;
            cube.SetActive(true);
            cube.transform.localScale = new Vector3(1f,0.1f,0.1f);
            cube.transform.parent = leftCubes.transform;


            // Vector3 pos1 = new Vector3(1, 2f, z);
            // GameObject cube1 = Instantiate(prefab, pos1, Quaternion.identity);
            // cube1.GetComponent<Renderer>().material.color = color;
            // cube1.transform.parent = rightCubes.transform;
            // cube1.SetActive(true);
            // cube1.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

            // float angle = ((i / (float)numberOfObjects) * Mathf.PI) - (Mathf.PI / 4f);
            // if(i>30){continue;}
            // float hue1 = i/30f;
            // Color color1 = Color.HSVToRGB(hue1, 1, 1);
            // float angle = (i/30f) * Mathf.PI * 2f;
            // Vector3 pos2 = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 500f;
            // pos2.y = Mathf.Sin(i*Mathf.PI/3)*350f;
            // GameObject cube2 = Instantiate(prefab, pos2, Quaternion.Euler(0, -360 * (i/30f), 0));
            // cube2.transform.parent = skyCubes.transform;
            // cube2.transform.localScale = new Vector3(1f,1f,1f);
            // cube2.GetComponent<Renderer>().material.color = color1;
            // cube2.SetActive(true);
        }
    }

    
    void Update ()
    {
        float[] spectrum = new float[256]; AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        float[] output = new float[256]; AudioListener.GetOutputData(output, 0);
        float volume = 0f;
        for(int i = 0; i < leftCubes.transform.childCount; i++){
            // Debug.Log("LeftCube"+i);
            Vector3 pos = leftCubes.transform.GetChild(i).position;
            pos.y = (output[i]*2)+height;
            leftCubes.transform.GetChild(i).position = pos;
            volume += Mathf.Abs(output[i]);
        }
        volume /= 256f;
        emission.rateOverTime = (int)(volume * 200);
        main.simulationSpeed = (int)(volume * 100) + 5;
        for(int i = 0; i < planets.transform.childCount; i++){
            ParticleSystem.SizeOverLifetimeModule size = planets.transform.GetChild(i).GetComponent<ParticleSystem>().sizeOverLifetime;
            size.size = spectrum[i]*2 + 1;
            ParticleSystem.MainModule main = planets.transform.GetChild(i).GetComponent<ParticleSystem>().main;
            float newSpeed = (int)(volume * 10) + 1;
            float oldSpeed = main.simulationSpeed;
            main.simulationSpeed = Mathf.Lerp(newSpeed, oldSpeed, Time.deltaTime * 20);
            // pos.y = (output[i]*2)+height;
            // leftCubes.transform.GetChild(i).position = pos;
            // volume += Mathf.Abs(output[i]);
        }
        ParticleSystem.EmissionModule emit = waveform.emission;
        ParticleSystem.MainModule m = waveform.main;
        if (Input.GetButtonDown("Fire1")){
            triggerDown = true;
        } else if (Input.GetButtonUp("Fire1")){
            triggerDown = false;
        }
        if(triggerDown){
            emit.rateOverTime = 100;
            // m.startSpeed = (int)(volume * 15)+1;
            m.startSize = 0.4f * volume;
        } else {
            emit.rateOverTime = 0;
            // m.startSpeed = (int)(volume * 15)+1;
        }
        float newStarSpeed = (int)(volume * 3000) + 200;
        float oldStarSpeed = stars.main.startSpeed.constant;
        ParticleSystem.MainModule sm = stars.main;
        sm.startSpeed = Mathf.Lerp(newStarSpeed, oldStarSpeed, Time.deltaTime * 20);
    }
}
