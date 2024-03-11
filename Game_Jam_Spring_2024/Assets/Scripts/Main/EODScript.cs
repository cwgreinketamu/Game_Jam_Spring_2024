using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EODScript : MonoBehaviour
{
    public static int dayNum;
    public int eodWait = 10;
    public TMPro.TMP_Text dayText;

    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded called");

        if (scene.name != "Main")
        {
            dayNum += 1;
            dayText.text = "End of Day " + dayNum;
        }

        StartCoroutine(EndDay());
    }

    IEnumerator EndDay()
    {
        Debug.Log("EndDay started");

        yield return new WaitForSeconds(eodWait);
        //Scene mainScene = SceneManager.GetSceneByName("Main");
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
