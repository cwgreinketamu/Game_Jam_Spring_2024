using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClipboardScript : MonoBehaviour
{
    public Canvas clipboard;
    public TMPro.TMP_Text taskText;
    public float textHeight = 50f;
    public Vector3 textPos = new Vector3(-844, -273, 0);

    private Dictionary<int, string> descriptions = new Dictionary<int, string>();
    private List<TMPro.TMP_Text> taskList = new List<TMPro.TMP_Text>();

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddText(int taskId)
    {
        TMPro.TMP_Text tempText = Instantiate(taskText, clipboard.transform);
        tempText.rectTransform.transform.localPosition = textPos;
        textPos.y -= textHeight;
        tempText.text = descriptions[taskId];
        taskList.Add(tempText);
    }

    public void RemoveText(int taskId)
    {
        Debug.Log("Removing task from clipboard");
        for (int i = 0; i < taskList.Count; i++)
        {
            if (taskList[i].text == descriptions[taskId])
            {
                Destroy(taskList[i]);
                taskList.RemoveAt(i);
                for (int j = i; j < taskList.Count; j++)
                {
                    Vector3 tempPos = taskList[j].rectTransform.transform.localPosition;
                    tempPos.y += textHeight;
                    taskList[j].rectTransform.transform.localPosition = tempPos;
                }
                textPos.y += textHeight;
                break;
            }
        }
    }
}
