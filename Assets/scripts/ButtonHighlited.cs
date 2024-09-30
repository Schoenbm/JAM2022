using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlited : Button
{

    public bool GetHighlighted()
    {
        return (this.IsHighlighted());
    }

    public void updateButtonState()
    {
        this.DoStateTransition(SelectionState.Highlighted, true);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

    }
}
