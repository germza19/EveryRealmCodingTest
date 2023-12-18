using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button _button;
    private Image _buttonImage;
    private PerformAction _performAction;
    private ObjectController _currentController;
    [SerializeField] private Image _iconImage;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();
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
        this._performAction = performAction;

        SetIconImage();
        SetButtonColor(CheckIfHasBehaviour());
    }
    public void SetButton(ObjectController currentController)
    {
        this._currentController = currentController;

        SetButtonColor(CheckIfHasBehaviour());
    }

    private void HandleButtonClick()
    {
        if (_performAction != null && _currentController != null)
        {
            _currentController.HandleActionButton(_performAction);

            SetButtonColor(CheckIfHasBehaviour());
        }
    }

    private void SetIconImage()
    {
        _iconImage.sprite = _performAction.iconSprite;
    }
    private void SetButtonColor(bool value)
    {
        if(value)
        {
            _buttonImage.color = Color.green;
        }
        else
        {
            _buttonImage.color = Color.gray;
        }
    }
    private bool CheckIfHasBehaviour()
    {
        if(_currentController != null)
        {
            if (_currentController.performActions.Contains(_performAction))
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
