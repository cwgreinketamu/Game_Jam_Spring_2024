using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class usersoundwaveScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points;
    public float amplitude = 1;
    public float frequency = 2;
    public float timeConstant = 1;
    public float xStart = 0;
    public float lengthConstant = 3;
    public GameObject slider;
    private float minX;
    private float maxX;
    private soundwaveScript goalScript;
    public float interval1 = 0.01f;
    public float interval2 = 0.05f;
    public float interval3 = 0.10f;
    public float duration = 1;
    private float timer;
    private GameManagerScript gameManager;
    private int taskId = 4;

    AudioSource staticSound;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        goalScript = GameObject.Find("GoalSoundwave").GetComponent<soundwaveScript>();
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        minX = slider.GetComponent<SliderScript>().minX;
        maxX = slider.GetComponent<SliderScript>().maxX;

        staticSound = GetComponent<AudioSource>();
        staticSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        frequency = 0.01f + (slider.transform.position.x - minX) / (maxX - minX) * (0.20f - 0.01f);
        Draw();
        float frequencyDifference = Mathf.Abs(goalScript.frequency - frequency);
        float normalizedDifference = Mathf.Clamp01(frequencyDifference / (0.20f - 0.01f));
        staticSound.volume = normalizedDifference;

        if (goalScript.frequency - interval1 < frequency && goalScript.frequency + interval1 > frequency)
        {
            lineRenderer.material.SetColor("_Color", new Color(0, 255, 0, 255));
            if (timer == -1)
            {
                timer = Time.time;
            }
            else if (Time.time - timer > 1)
            {
                //Debug.Log("Sound Matched!");
                if (GameObject.Find("GameManager") != null)
                {
                    Invoke("End", 1);
                }
            }
        }
        else
        {
            timer = -1;
            if (goalScript.frequency - interval2 < frequency && goalScript.frequency + interval2 > frequency)
            {
                lineRenderer.material.SetColor("_Color", new Color(255, 255, 0, 255));
            }
            else 
            {
                lineRenderer.material.SetColor("_Color", new Color(255, 0, 0, 255));
            }
        }
    }

    void Draw()
    {
        float Tau = lengthConstant * Mathf.PI;
        float xFinish = Tau;

        lineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * frequency * x) + (timeConstant * Time.timeSinceLevelLoad));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    private void End()
    {
        staticSound.Stop();
        gameManager.EndTask(taskId);
    }
}
