using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    private int index;

    private void Start() {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update() {
        if (textComponent.text == lines[index]) {
            NextLine();
        }
    }

    private void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text += string.Empty;
            StartCoroutine(TypeLine());
        }
    }

}
