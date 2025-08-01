using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class healthHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image image;
    private float hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = hp / 100;
        hpText.text = Mathf.RoundToInt(hp).ToString() + "%";
        hpText.GetComponent<shakyText>().setShakiness(Mathf.Max(8 * (1 - hp * 0.01f),0));
    }
    public void setHealthAmount(float amount)
    {
        hp = amount;
    }
}
