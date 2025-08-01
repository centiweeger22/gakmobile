using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject restartText;
    [SerializeField] private TMP_Text restartTextTMP;
    [SerializeField] private GameObject otherUI;
    private PlayerInput playerInput;
    public float playerPoints;
    private bool lastFrameButton;
    private bool currentFrameButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartText.SetActive(false);
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        lastFrameButton = currentFrameButton;
        currentFrameButton = playerInput.actions["Jump"].ReadValue<float>() == 1;
        if (playerObject == null)
        {
            restartTextTMP.text = "You got "+playerPoints.ToString()+" Points!\nPress Space to Restart";
            otherUI.SetActive(false);
            restartText.SetActive(true);
            if (currentFrameButton && !lastFrameButton)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
