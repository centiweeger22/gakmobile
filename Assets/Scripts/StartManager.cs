using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private PlayerInput input;
    private float transparency = -1;
    [SerializeField] private Image targetImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.actions["Jump"].ReadValue<float>() == 1)
        {
            transparency = 0;
        }
        if (transparency >= 0)
        {
            transparency = Mathf.Lerp(transparency, 1, Time.deltaTime*10f);
        }
        Color currentColor = targetImage.color;
        currentColor.a = transparency;
        targetImage.color = currentColor;
        if (transparency > 0.999)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
