using UnityEngine;
using TMPro;

public class loopsController : MonoBehaviour
{
    [SerializeField] float loops;
    [SerializeField] int quarterLoops;
    int totalLoops;
    [SerializeField] float targetLoops = 2;
    [SerializeField] private TMP_Text instructions;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text turn_around;
    [SerializeField] private TMP_Text lava_warning;
    [SerializeField] private GameObject lava;
    [SerializeField] private GameObject directionSprite;
    [SerializeField] private GameObject portals;
    [SerializeField] private float timerSeconds = 30;
    [SerializeField] private car Player;
    [SerializeField] private float turnAroundTimer;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private int untilBonus = 5;
    [SerializeField] private int difficulty = 5;
    private int soundProgress;
    private float soundTimer;
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private enemySpawner[] spawners;
    [SerializeField] private oscillate lavaOscillator;
    private Vector3 directionPosition;
    private bool hurrying;
    private bool toolong;
    private bool paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directionPosition = directionSprite.transform.position;
        // audioSource1 = GetComponent<audioSource1>();
        portals.SetActive(false);
        musicSource.clip = music[0];
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
            timerSeconds -= Time.deltaTime;
        if (targetLoops == loops)
        {
            float newDirection = -Mathf.Sign(targetLoops);
            targetLoops = Random.Range(1, 5) * newDirection;
            if (targetLoops == 0)
            {
                targetLoops = 1;
            }
            loops = 0;
            quarterLoops = 0;
            timerSeconds = Mathf.Abs(targetLoops) * 12 + Random.Range(-Mathf.Abs(targetLoops) * 5 - difficulty, (Mathf.Abs(targetLoops) * 5)/(1+difficulty*0.25f));
            turnAroundTimer = 3;
            Player.DamagePlayer(-10, false, 0);
            soundProgress = 0;
            soundTimer = 0;
            Player.AwardPoints((int)Mathf.Max(timerSeconds, 0));
            untilBonus -= 1;
            if (untilBonus == 0)
            {
                portals.SetActive(true);
                Player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                Player.gameObject.GetComponent<Rigidbody>().useGravity = true;
                Player.gameObject.GetComponent<Rigidbody>().position = Vector3.up * 15;
                Player.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                Player.gameObject.GetComponent<Rigidbody>().rotation = Quaternion.identity;
                Player.floorVelocity = Vector2.zero;
                Player.controlBlockTimer = 1;
                paused = true;
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
                lavaOscillator.enabled = false;
                lava.transform.position = new Vector3(0, -100, 0);
                // Iterate through the found objects and do something with them
                if (taggedObjects.Length > 0)
                {
                    foreach (GameObject obj in taggedObjects)
                    {
                        Destroy(obj);
                    }
                }
                foreach (enemySpawner spawner in spawners)
                {
                    spawner.enabled = false;
                }
                untilBonus = 5;
                musicSource.clip = music[1];
                musicSource.Play();
            }
        }
        string direction = "CLOCKWISE";
        if (targetLoops < 0)
        {
            direction = "COUNTERCLOCKWISE";
        }
        string loopText = "LOOPS";
        if (Mathf.Abs(targetLoops - loops) == 1)
        {
            loopText = "LOOP";
        }
        string hurryup = "";
        if (timerSeconds < 15)
        {
            hurryup = "\n HURRY UP!!!";
            if (!hurrying)
            {
                audioSource1.PlayOneShot(sounds[9]);
            }
            hurrying = true;
        }
        else
        {
            hurrying = false;
        }
        if (timerSeconds < 0)
        {
            hurryup = "\n YOU'RE TAKING TOO LONG!";
            toolong = true;
            audioSource1.PlayOneShot(sounds[11]);
        }
        else
        {
            toolong = false;
        }
        if (Mathf.Abs(quarterLoops) > 0 && Mathf.Sign(quarterLoops) != Mathf.Sign(targetLoops))
        {
            hurryup += "\n WRONG WAY!!!!";
        }
        instructions.text = "GO " + Mathf.Abs(targetLoops - loops).ToString() + " " + loopText + " " + direction + "!!!!!" + hurryup;
        timerText.text = (Mathf.RoundToInt(timerSeconds * 10) * 0.1f).ToString();
        if (paused)
        {
            instructions.text = "Pick a bonus!";
            turnAroundTimer = 0;
            timerText.text = "Bonus Time!";
        }
        turnAroundTimer -= Time.deltaTime;
        turn_around.enabled = turnAroundTimer > 0;

        if (timerSeconds < 0)
        {
            Player.DamagePlayer(Time.deltaTime * 15, false, 0.1f);
        }
        lava_warning.enabled = lava.transform.position.y > -65;
        directionSprite.transform.localScale = Vector3.Lerp(directionSprite.transform.localScale, new Vector3(Mathf.Sign(targetLoops), 1, 1), Time.deltaTime * 10);
        directionSprite.transform.position = directionPosition + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        soundTimer -= Time.deltaTime;
        if (soundTimer < 0)
        {
            float padding = 0.10f;
            if (soundProgress < 4)
            {
                soundProgress++;
                if (soundProgress == 1)
                {
                    audioSource1.PlayOneShot(sounds[0]);
                    soundTimer = sounds[0].length + padding;
                }
                if (soundProgress == 2)
                {
                    audioSource1.PlayOneShot(sounds[(int)Mathf.Abs(targetLoops)]);
                    soundTimer = sounds[(int)Mathf.Abs(targetLoops)].length + padding;
                }
                if (soundProgress == 3)
                {
                    if (Mathf.Abs(targetLoops) > 1)
                    {
                        audioSource1.PlayOneShot(sounds[7]);
                        soundTimer = sounds[7].length + padding;
                    }
                    else
                    {
                        audioSource1.PlayOneShot(sounds[6]);
                        soundTimer = sounds[6].length + padding;
                    }
                    soundTimer = 1.5f;
                }
                if (soundProgress == 4)
                {
                    audioSource1.PlayOneShot(sounds[9 + (int)Mathf.Sign(targetLoops)]);
                    soundTimer = sounds[9 + (int)Mathf.Sign(targetLoops)].length + padding;
                }
                audioSource1.time = 0;
            }
        }
    }
    public void registerLoop(int direction)
    {
        quarterLoops += direction;
        loops = quarterLoops / 4;
        transform.rotation = transform.rotation * Quaternion.AngleAxis(90 * direction, Vector3.up);
    }

    public void ResumeGameplay()
    {
        portals.SetActive(false);
        Player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Player.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Player.gameObject.GetComponent<Rigidbody>().position = Vector3.up * 15;
        Player.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        Player.floorVelocity = Vector2.zero;
        Player.controlBlockTimer = 0.25f;
        paused = false;
        difficulty += 1;
        if (difficulty >= 3)
        {
            lavaOscillator.enabled = true;
        }
        foreach (enemySpawner spawner in spawners)
        {
            spawner.enabled = true;
            spawner.difficulty = difficulty;
        }
        musicSource.clip = music[0];
        musicSource.Play();
    }
}
