using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlited : Button
{
    bool mouseover = false;
    public bool GetHighlighted()
    {
        return (mouseover);
    }

    private void OnMouseExit()
    {
        mouseover = false;
        InstantClearState();
    }


    private void OnMouseEnter()
    {
        mouseover = true;
        InstantClearState();
    }

}
