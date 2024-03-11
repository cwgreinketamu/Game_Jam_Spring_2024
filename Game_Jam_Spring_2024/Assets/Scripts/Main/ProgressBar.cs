using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
     public Image progressBar;
    // Start is called before the first frame update
    void Awake()
    {
       progressBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseBar(float increase)
    {
        progressBar.fillAmount += increase;
    }

    public void DecreaseBar(float decrease)
    {
        progressBar.fillAmount -= decrease;
    }
}