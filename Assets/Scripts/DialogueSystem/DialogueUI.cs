using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance;//singleton
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject dialogueBox;
    //[SerializeField] private DialogueObject test;
    public bool isTyping = false;
    private TypeWriterEffect TypeWriterEffect;

    private void Awake()
    {
        if (instance != null && instance !=this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

    }

    private void Start()
    {
        TypeWriterEffect = GetComponent<TypeWriterEffect>();
        CloseDialogueBox();
        //ShowDialogue(test);

    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(IterateDialogue(dialogueObject));
    }

    private IEnumerator IterateDialogue(DialogueObject dialogueObject)
    {
        if (!isTyping) {
            isTyping = true;
            foreach (string dialogue in dialogueObject.Dialogue)
            {
                yield return TypeWriterEffect.Run(dialogue, textLabel);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            }
            CloseDialogueBox();
            isTyping = false;
        }
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
