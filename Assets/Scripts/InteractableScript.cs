using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhotoOneScript : MonoBehaviour
{
    [SerializeField] private DialogueObject text;
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialogueUI.instance.ShowDialogue(text);
        }
    }
}
