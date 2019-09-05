using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour {

    public Selectable selectOnEnable;


    private void OnEnable()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            selectOnEnable.Select();
        }
    }


}
