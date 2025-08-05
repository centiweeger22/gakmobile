using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class fullscreenIndicator : MonoBehaviour
{
    private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.enabled = Screen.width < 1000;
    }
}
