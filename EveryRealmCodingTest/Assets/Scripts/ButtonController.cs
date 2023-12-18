using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button _button;
    private Image _image;
    public PerformAction performAction;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }
    void Start()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(HandleButtonClick);
        }
    }

    public void SetButton(PerformAction performAction)
    {
        this.performAction = performAction;
        SetColor();
    }

    private void SetColor()
    {
        if (performAction.hasBeenActivated)
        {
            _image.color = Color.green;
        }
        else
        {
            _image.color = Color.gray;
        }
    }
    private void HandleButtonClick()
    {
        if (performAction != null)
        {
            performAction.ChangeActivationState();
            SetColor();
        }
    }
}
