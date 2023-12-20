using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private float _yOffset = 2f;
    private List<ButtonController> _buttons = new List<ButtonController>();
    
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private ButtonController _buttonsPrefab;
    private CanvasGroup _canvasGroup;
    
    private void OnEnable()
    {
        SelectionManager.OnSelectedObject += SetCanvas;
    }
    private void OnDisable()
    {
        SelectionManager.OnSelectedObject -= SetCanvas;
    }
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        SetCanvasGroup(false);
        CreateBehaviourButtons();
    }
    public void SetCanvas(ObjectController objectController)
    {
        SetCanvasGroup(true);

        transform.position = new Vector3(objectController.transform.position.x, objectController.transform.position.y - _yOffset, transform.position.z);

        if(objectController != null )
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].SetButton(objectController);
            }
        }
    }
    private void CreateBehaviourButtons()
    {
        foreach (PerformAction performAction in ActionsManager.Instance.performActions)
        {
            ButtonController newButton = Instantiate(_buttonsPrefab, _buttonsParent);
            newButton.InitializeButton(performAction);
            _buttons.Add(newButton);
        }
    }
    private void SetCanvasGroup(bool value)
    {
        if(value)
        {
            _canvasGroup.alpha = 1;
        }
        else
        {
            _canvasGroup.alpha = 0;
        }
    }
    
}
