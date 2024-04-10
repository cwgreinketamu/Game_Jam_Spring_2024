using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;
    private Dictionary<int, float> progressAmounts = new Dictionary<int, float>();
    private GameManagerScript gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        progressBar = GetComponent<Image>();
        progressAmounts.Add(1, 0.05f);
        progressAmounts.Add(2, 0.05f);
        progressAmounts.Add(3, 0.05f);
        progressAmounts.Add(-1, 0.05f);
        progressAmounts.Add(4, 0.05f);
        progressAmounts.Add(5, 0.05f);
        progressAmounts.Add(6, 0.00f);
        progressAmounts.Add(7, 0.05f);
        progressAmounts.Add(8, 0.05f);
        progressAmounts.Add(9, 0.05f);
        progressAmounts.Add(10, 0.05f);
        progressAmounts.Add(-2, 0.05f);
        progressAmounts.Add(-3, 0.05f);
        progressAmounts.Add(-4, 0f);
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
        progressBar.fillAmount += progressAmounts[taskId];
        gameManager.IncreaseProgress(progressAmounts[taskId]);
    }

    public void DecreaseBar(int taskId)
    {
        progressBar.fillAmount -= progressAmounts[taskId];
        gameManager.DecreaseProgress(progressAmounts[taskId]);
    }

    public void SetBar(float value)
    {
        progressBar.fillAmount += value;
    }
}