using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private float _yOffset = 2f;
    [SerializeField] private List<ButtonController> _buttons = new List<ButtonController>();


    private void OnEnable()
    {
        SelectionManager.OnSelectedObject += SetCanvas;
    }
    private void OnDisable()
    {
        SelectionManager.OnSelectedObject -= SetCanvas;
    }
    public void SetCanvas(ObjectController objectController)
    {
        transform.position = new Vector3(objectController.transform.position.x, objectController.transform.position.y - _yOffset, transform.position.z);

        if(objectController != null )
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].SetButton(objectController.performActions[i]);
            }
        }
    }
}
