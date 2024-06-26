using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCircle : MonoBehaviour
{
    public Image progressCircle;
    private Dictionary<int, float> progressIncreases = new Dictionary<int, float>();
    private Dictionary<int, float> progressDecreases = new Dictionary<int, float>();
    private GameManagerScript gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        progressCircle = GetComponent<Image>();

        progressIncreases.Add(1, 0.05f);
        progressIncreases.Add(2, 0.05f);
        progressIncreases.Add(3, 0.00f);
        progressIncreases.Add(-1, 0.10f);
        progressIncreases.Add(4, 0.05f);
        progressIncreases.Add(5, 0.05f);
        progressIncreases.Add(6, 0.00f);
        progressIncreases.Add(7, 0.05f);
        progressIncreases.Add(8, 0.10f);
        progressIncreases.Add(9, 0.05f);
        progressIncreases.Add(10, 0.05f);
        progressIncreases.Add(-2, 0.0f);
        progressIncreases.Add(-3, 0.075f);
        progressIncreases.Add(-4, 0f);

        progressDecreases.Add(1, 0.025f);
        progressDecreases.Add(2, 0.025f);
        progressDecreases.Add(3, 0.025f);
        progressDecreases.Add(-1, 0.025f);
        progressDecreases.Add(4, 0.025f);
        progressDecreases.Add(5, 0.025f);
        progressDecreases.Add(6, 0.025f);
        progressDecreases.Add(7, 0.025f);
        progressDecreases.Add(8, 0.025f);
        progressDecreases.Add(9, 0.025f);
        progressDecreases.Add(10, 0.025f);
        progressDecreases.Add(-2, 0.025f);
        progressDecreases.Add(-3, 0.025f);
        progressDecreases.Add(-4, 0f);

        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseBar(int taskId)
    {
        float increasePercentage = progressIncreases[taskId];
        progressCircle.fillAmount += increasePercentage;
        gameManager.IncreaseProgress(increasePercentage);
    }


    public void DecreaseBar(int taskId)
    {
        float increasePercentage = progressIncreases[taskId];
        progressCircle.fillAmount += increasePercentage;
        gameManager.IncreaseProgress(increasePercentage);
    }

     public void SetBar(float value)
    {
        progressCircle.fillAmount += value;
    }
}
