using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LockerScript : MonoBehaviour
{
    public bool Interactable = true;
    public GameObject LockCanvas;
    public TextMeshProUGUI[] Text;
    public GameObject gameOverPanel;
    public Texture2D handCursor; 
    public Texture2D defaultCursor;
    private AudioSource audio_unlock;

    public string Password;
    public string[] LockCharacterChoices;
    public int[] _pointerArray;
    private string _insertedPassword;
    public Sprite UnlockSprite;
    public int direction;
    private int SelectedNumber;


    void Start()
    {
        audio_unlock = GetComponent<AudioSource>();
        _pointerArray = new int[Password.Length];
        UpdateUI();
    }

    public void Selected(int number)
    {
        SelectedNumber = number;

    }

    public void HandleScroll()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0) 
        {
            direction = -1;
            ChangePassword(SelectedNumber, direction); 
        }
        else if (scroll < 0) 
        {
            direction = 1;
            ChangePassword(SelectedNumber, direction); 
        }
    }



    public void OnPointerEnter()
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void ChangePassword(int number, int direction)
    {
        if (direction == 1)
        {
            _pointerArray[number]++;
        }
        else
        {
            _pointerArray[number]--;
        }

        if (_pointerArray[number] >= LockCharacterChoices[number].Length)
        {
            _pointerArray[number] = 0;
        }

        if (_pointerArray[number] < 0)
        {
            _pointerArray[number] = 9;
        }

        UpdateUI();
    }


    public void ValidatePassword()
    {
        CheckPassword();
    }


    public void CheckPassword()
    {
        int pw_length = Password.Length;
        _insertedPassword = "";
        for (int i = 0; i < Password.Length; i++)
        {
            _insertedPassword += LockCharacterChoices[i][_pointerArray[i]].ToString();
        }
        if (Password == _insertedPassword)
        {
            Unlock();
        }

    }

    private void OnMouseDown()
    {
        if ((Input.GetMouseButtonDown(0)) && !DialogueUI.instance.isTyping) 
        {
            LockCanvas.SetActive(true);
        }
    }

    public void Unlock()
    {
        Interactable = false;
        StopInteract();
        gameObject.GetComponent<SpriteRenderer>().sprite = UnlockSprite;
        audio_unlock.Play();
        gameOverPanel.SetActive(true);

    }

    public void UpdateUI()
    {
        for (int i = 0; i <Text.Length; i++)
        {
            Text[i].text = LockCharacterChoices[i][_pointerArray[i]].ToString();
        }

    }


    public void Interact()
    {
        if (Interactable)
        {
            LockCanvas.SetActive(true);
        }

    }

    public void StopInteract()
    {
        LockCanvas.SetActive(false);
    }


}
