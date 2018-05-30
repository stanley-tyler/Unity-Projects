using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumFlow : MonoBehaviour {

	public Terrain TerrainMain;
    private int xRes;
    private int yRes;
    float [,] heights;

    void Start()
    {
        xRes = TerrainMain.terrainData.heightmapWidth;
        yRes = TerrainMain.terrainData.heightmapHeight;
        Debug.Log("XRes: " + xRes + " | YRes: " + yRes);
        heights = TerrainMain.terrainData.GetHeights(0,0,xRes,yRes);
        int rowLength = heights.GetLength(0);
        int colLength = heights.GetLength(1);
        Debug.Log("Row Length: " + rowLength + "\nCol Length: "+ colLength);

    }

    
    void Update ()
    {
        float[] spectrum = new float[1024]; AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        for (int i = 1; i < xRes/10; i++)
        {
            // Debug.Log("Height at "+i+","+i+": "+heights[i,i]);
            // heights[i,i] = Mathf.Lerp(heights[i,yRes], spectrum[i], Time.deltaTime * 30);
            heights[i,i] = 0.1f;
        }
        TerrainMain.terrainData.SetHeights(0,0, heights);
    }
}
