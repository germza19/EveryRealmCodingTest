using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button _button;
    private Image _image;
    public PerformAction performAction;
    public ObjectController currentController;

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
    public void InitializeButton(PerformAction performAction)
    {
        this.performAction = performAction;

        SetButtonColor(CheckIfHasBehaviour());
    }
    public void SetButton(ObjectController currentController)
    {
        this.currentController = currentController;

        SetButtonColor(CheckIfHasBehaviour());
    }

    private void HandleButtonClick()
    {
        if (performAction != null && currentController != null)
        {
            currentController.HandleActionButton(performAction);

            SetButtonColor(CheckIfHasBehaviour());
        }
    }

    private void SetButtonColor(bool value)
    {
        if(value)
        {
            _image.color = Color.green;
        }
        else
        {
            _image.color = Color.gray;
        }
    }
    private bool CheckIfHasBehaviour()
    {
        if(currentController != null)
        {
            if (currentController.performActions.Contains(performAction))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
