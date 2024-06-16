using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTypeEffect : MonoBehaviour
{
    TextMeshProUGUI tmp;
    [TextArea]
    public string nonTypingString;
    public string typingString;
    public float typingSpeed;
    public bool loop;
    public bool runOnAwake;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (runOnAwake) StartCoroutine(Typing(loop, typingString));
    }

    private void OnEnable()
    {
        StartCoroutine(Typing(loop, typingString));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Typing(bool loop, string str)
    {
        do
        {
            tmp.text = nonTypingString;
            foreach (char c in str)
            {
                tmp.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        } while (loop);
    }
}
