using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour
{
    public WinCheck winRef;

    public void CloseMenu()
    {
        winRef.Unpause();
    }
}
