using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundwaveScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points;
    public float amplitude = 1;
    public float frequency = 1;
    public float timeConstant = 1;
    public float xStart = 0;
    public float lengthConstant = 3;
    public float xConstant = 1;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (Random.Range(0,2) == 0)
        {
            frequency = Random.Range(0.05f, 0.10f);
        }
        else
        {
            frequency = Random.Range(0.14f, 0.20f);
        }
        
        sound = GetComponent<AudioSource>();
        sound.Play();
        sound.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    void Draw()
    {
        float Tau = lengthConstant*Mathf.PI;
        float xFinish = Tau;

        lineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint/(points-1);
            float x = Mathf.Lerp(xStart,xFinish, progress);
            float y = amplitude*Mathf.Sin(xConstant*((Tau*frequency*x)+(timeConstant*Time.timeSinceLevelLoad)));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }
}
