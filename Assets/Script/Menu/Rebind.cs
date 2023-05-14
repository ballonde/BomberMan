using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class Rebind : MonoBehaviour
{
    //[SerializeField]
    //private InputActionReference ActionToRemap1;

    [SerializeField]
    private TextMeshProUGUI _buttonText;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;



    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartRebind(InputActionReference ActionToRemap)
    {
        EventSystem.current.SetSelectedGameObject(null);

        ActionToRemap.action.Disable();

        _buttonText.text = "Press new Input";

        _rebindingOperation = ActionToRemap.action.PerformInteractiveRebinding().OnMatchWaitForAnother(0.1f).OnComplete(operation =>
        {
            _buttonText.text = InputControlPath.ToHumanReadableString(ActionToRemap.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            _rebindingOperation.Dispose();
            ActionToRemap.action.Enable();
        }).Start();

    }
}
