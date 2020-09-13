using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSelectOnHover : MonoBehaviour {

    //Called as an OnPointerEnter Event Trigger
    public void HoverOverSelect() {
        Button button = this.GetComponent<Button>();

        if (button.interactable) {
            button.Select();
        }
    }
}
