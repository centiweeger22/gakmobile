using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject restartText;
    [SerializeField] private TMP_Text restartTextTMP;
    [SerializeField] private GameObject otherUI;
    [SerializeField] private GameObject pauseUI;
    public bool pauseGame;
    private float transparency = -1;
    [SerializeField] private Image targetImage;
    private PlayerInput playerInput;
    public float playerPoints;
    private bool lastFrameButton;
    private bool currentFrameButton;
    private bool showUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartText.SetActive(false);
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showUI)
        {
            otherUI.SetActive(true);
        }
        if (pauseGame)
        {
            setUIEnabled(false);
            pauseUI.SetActive(true);
        }
        else
        {
            // otherUI.SetActive(false);
            pauseUI.SetActive(false);
        }

        Time.timeScale = 1;
        if (pauseGame)
        {
            Time.timeScale = 0;
        }
        lastFrameButton = currentFrameButton;
        currentFrameButton = playerInput.actions["Jump"].ReadValue<float>() == 1;
        if (playerObject == null)
        {
            restartTextTMP.text = "You got " + playerPoints.ToString() + " Points!\nPress Space to Restart";
            otherUI.SetActive(false);
            restartText.SetActive(true);
            if (currentFrameButton && !lastFrameButton)
            {
                transparency = 0;
            }

            if (transparency > 0.999)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (transparency >= 0)
        {
            Color currentColor = targetImage.color;
            currentColor.a = transparency;
            targetImage.color = currentColor;
            transparency += Time.deltaTime;
        }
    }
    public void setUIEnabled(bool value)
    {
        otherUI.SetActive(value);
        showUI = showUI || value;
    }
}