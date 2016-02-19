using UnityEngine;

public class PopupController : MonoBehaviour
{

    public bool isOpen;

    public virtual void Update()
    {
        UpdateHideIfClickedOutside();
    }

    private void UpdateHideIfClickedOutside()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(),
                                                                    Input.mousePosition,
                                                                    Camera.main))
            {
                OnClick();
            }
            else
            {
                OnClickOutside();
            }
        }
    }

    /// <summary>
    /// Yêu cầu: RectTransform phải có kích thước là vùng giới hạn của popup
    /// </summary>
    protected virtual void OnClickOutside()
    {
    }

    protected virtual void OnClick()
    {
    }

    public virtual void Open()
    {
        isOpen = true;
    }

    public virtual void Close()
    {
        isOpen = false;
    }

    public virtual void Toggle()
    {
        //gameObject.SetActive(!gameObject.activeSelf);
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
