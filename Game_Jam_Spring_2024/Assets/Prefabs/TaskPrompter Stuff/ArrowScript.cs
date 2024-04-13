using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform rectTrans;
    private bool isOffScreen;
    public float borderSize = 100f;
    private Image image;
    private bool active;

    public Sprite GreenArrow;
    public Sprite YellowArrow;
    public Sprite RedArrow;

    // Start is called before the first frame update
    void Awake()
    {
        targetPosition = transform.parent.transform.parent.transform.position;
        rectTrans = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        active = false;
        image.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            Vector3 fromPosition = GameObject.Find("Player").transform.position;
            Vector3 dir = (targetPosition - fromPosition).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            if (angle < 0)
            {
                angle += 360;
            }
            rectTrans.localEulerAngles = new Vector3(0, 0, angle);

            Vector3 targetPosScreen = Camera.main.WorldToScreenPoint(targetPosition);
            if (targetPosScreen.x <= borderSize || targetPosScreen.x >= Screen.width - borderSize || targetPosScreen.y <= borderSize || targetPosScreen.y >= Screen.height - borderSize)
            {
                isOffScreen = true;
            }
            else
            {
                isOffScreen = false;
            }

            if (isOffScreen)
            {
                image.enabled = true;
                Vector3 cappedTargetScreenPos = targetPosScreen;
                if (cappedTargetScreenPos.x <= borderSize)
                {
                    cappedTargetScreenPos.x = borderSize;
                }
                if (cappedTargetScreenPos.x >= Screen.width - borderSize)
                {
                    cappedTargetScreenPos.x = Screen.width - borderSize;
                }
                if (cappedTargetScreenPos.y <= borderSize)
                {
                    cappedTargetScreenPos.y = borderSize;
                }
                if (cappedTargetScreenPos.y >= Screen.height - borderSize)
                {
                    cappedTargetScreenPos.y = Screen.height - borderSize;
                }

                Vector3 worldPos = Camera.main.ScreenToWorldPoint(cappedTargetScreenPos);
                rectTrans.position = worldPos;
                rectTrans.localPosition = new Vector3(rectTrans.localPosition.x, rectTrans.localPosition.y, 0);
            }
            else
            {
                image.enabled = false;
            }
        }

    }
    public void Enable()
    {
        image.enabled = true;
        active = true;
    }
    public void Disable()
    {
        image.enabled = false;
        active = false;
    }

    public void SetColor(string color)
    {
        if (color == "Green")
        {
            image.sprite = GreenArrow;
        }
        else if (color == "Yellow")
        {
            image.sprite = YellowArrow;
        }
        if (color == "Red")
        {
            image.sprite = RedArrow;
        }
    }
}
