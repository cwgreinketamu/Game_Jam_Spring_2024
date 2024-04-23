using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PostItScript : MonoBehaviour
{

    public TMPro.TMP_Text taskText;
    public float textHeight = 50f;
    public Vector3 textPos = new Vector3(-844, -273, 0);

    private Dictionary<int, string> descriptions = new Dictionary<int, string>();
    private List<TMPro.TMP_Text> taskList = new List<TMPro.TMP_Text>();
    private List<Image> postits = new List<Image>(); 

    private int id;

    public Image postitPrefab; 


    // Start is called before the first frame update
    void Awake()
    {
        descriptions.Add(1, "Fix hacked PC");
        descriptions.Add(2, "Remove bugs from code");
        descriptions.Add(3, "Get coffee for boss");
        descriptions.Add(-1, "Deliver coffee to boss");
        descriptions.Add(4, "Create sound effects");
        descriptions.Add(5, "Create art assets");
        descriptions.Add(6, "Upload files");
        descriptions.Add(7, "Fix server issues");
        descriptions.Add(8, "Reupload files");
        descriptions.Add(9, "Playtest level");
        descriptions.Add(10, "Write dialogue");
        descriptions.Add(-2, "Pick up memos");
        descriptions.Add(-3, "Deliver memos");
        descriptions.Add(-4, "Head to the meeting room to get your first task");

        foreach (var taskId in descriptions.Keys)
        {
            CreatePostit(taskId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePostit(int taskId)
    {
        Image newPostit = Instantiate(postitPrefab, transform);
        newPostit.transform.localPosition = new Vector3(0, 0, 0);
        newPostit.enabled = false;
        postits.Add(newPostit);
        AddText(taskId, newPostit);
    }

    public void AddText(int taskId, Image postit)
    {
        TMPro.TMP_Text tempText = Instantiate(taskText, postit.transform);
        tempText.rectTransform.localPosition = Vector3.zero; // Position the text at the center of the post-it
        tempText.text = descriptions[taskId];
        Debug.Log("Adding task " + tempText.text);
        taskList.Add(tempText);
    }

    public void RemoveText(int taskId)
    {
        Debug.Log("Removing task ");
        for (int i = 0; i < taskList.Count; i++)
        {
            if (taskList[i].text == descriptions[taskId])
            {
                Destroy(taskList[i]);
                taskList.RemoveAt(i);
                break;
            }
        }
    }
    public void ShowPostit(int taskId)
    {
        Debug.Log("Showing postit " + taskId);
        int id = 0;
        if (taskId < 0)
        {
            if (taskId == -1)
            {
                id = 3;
            }
            else if (taskId == -2)
            {
                id = 11;
            }
            else if (taskId == -3)
            {
                id = 12;
            }
            else if (taskId == -4)
            {
                id = 13;
            }
        }
        else
        {
            id = taskId -1;
        }
        if (id >= 0 && id < postits.Count)
        {
            Debug.Log(postits[id]);
            postits[id].enabled = true;
        }
        else
        {
            Debug.LogError("Invalid id: " + id);
        }
    }
}
