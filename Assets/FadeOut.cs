using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    private PlayerInput input;
    private float transparency = 2;
    [SerializeField] private Image targetImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transparency >= 0)
        {
            transparency = Mathf.Lerp(transparency, 0, Time.deltaTime*3f);
        }
        Color currentColor = targetImage.color;
        currentColor.a = transparency;
        targetImage.color = currentColor;
    }
}
