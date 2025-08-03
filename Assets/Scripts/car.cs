using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class car : MonoBehaviour
{
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float steering = 80f;
    [SerializeField] private float maxSpeed = 20f;
    // [SerializeField] private float drag = 1f;
    private PlayerInput playerInput;
    private Rigidbody rb;
    public Vector2 floorVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float hp;
    [SerializeField] private Vector3 targetCarSize;
    [SerializeField] private GameObject carObject;
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private healthHandler hph;
    [SerializeField] private loopsController loops;
    [SerializeField] private speedLinesSpawner speedLineSpawner;
    public Portal playerPortal;
    public bool inPortal;
    Vector3 currentVector;
    private float clipTimer;
    private Vector3 lastPos;
    [SerializeField] private GameObject cameraPivot;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject bashObject;
    [SerializeField] private GameObject forceFieldObject;
    [SerializeField] private GameObject sparksObject;
    [SerializeField] private TMP_Text pointsCounter;
    [SerializeField] private TMP_Text portalPrompt;
    [SerializeField] private int points;
    [SerializeField] private gameManager gm;
    public float controlBlockTimer;
    private float fov;
    public float cancelTime;
    private cameraShake shake;
    private bool pb;
    private bool ppb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        lastPos = transform.position;
        rb.linearDamping = 0;
        shake = cameraObject.GetComponent<cameraShake>();
    }
    void Update()
    {
        ppb = pb;
        pb = playerInput.actions["Pause"].ReadValue<float>() == 1;
        if (pb & !ppb)
        {
            gm.pauseGame = !gm.pauseGame;
        }
        carObject.transform.localScale = Vector3.Lerp(carObject.transform.localScale,targetCarSize,Time.deltaTime*7f);
        cancelTime -= Time.deltaTime;
        gm.playerPoints = points;
        controlBlockTimer -= Time.deltaTime;
        clipTimer -= Time.deltaTime;
        if (clipTimer < 0)
        {
            clipTimer = 0.5f;
            float moveDist = (transform.position - lastPos).magnitude;
            RaycastHit hit;
            if (Physics.Raycast(lastPos, transform.position - lastPos, out hit, moveDist))
            {
                // transform.position = lastPos;
            }
        }
        if (hp <= 0)
        {
            hp = 0;
            cameraPivot.transform.parent = null;
            for (int i = 0; i < 20; i++)
            {
                GameObject g = Instantiate(explosionParticle, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            }
            ShakeCamera(10);
            Destroy(gameObject, 0);
        }
        hph.setHealthAmount(hp);
        if (transform.position.magnitude > 300 && transform.position.y < 1000)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = new Vector3(0, 0, 80);
            transform.rotation = Quaternion.identity;
        }
        float newFOV = 70 + floorVelocity.y * 15;
        fov = Mathf.Lerp(fov, newFOV, Time.deltaTime * 3);
        cameraObject.GetComponent<Camera>().fieldOfView = fov;
        speedLineSpawner.movementSpeed = floorVelocity.magnitude;
        pointsCounter.text = points.ToString() + "pts";
        if (floorVelocity.y > 1)
        {
            targetCarSize.y *= Mathf.Pow(0.4f, Time.deltaTime);
            targetCarSize.z *= Mathf.Pow(1.6f,Time.deltaTime);
        }
        targetCarSize = Vector3.Lerp(targetCarSize, Vector3.one,Time.deltaTime*4f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.useGravity && Vector3.Scale(rb.position, new Vector3(1, 0, 1)).magnitude > 120)
        {
            rb.position -= Vector3.Scale(rb.position.normalized, new Vector3(1, 0, 1)) * 0.5f;
        }
        rb.AddForce(new Vector3(0, -30, 0));
        Vector2 moveVector = Vector2.zero;  
        if (controlBlockTimer < 0)
        {
            moveVector = playerInput.actions["Move"].ReadValue<Vector2>();
        }
        bool brakeDrift = playerInput.actions["Jump"].ReadValue<float>() == 1;
        bool lookBack = playerInput.actions["Look Back"].ReadValue<float>() == 1;
        portalPrompt.enabled = false;
        if (playerPortal != null)
        {
            portalPrompt.enabled = true;
            if (brakeDrift)
            {
                loops.ResumeGameplay();
                points += playerPortal.scoreBonus;
                hp += playerPortal.healthBonus;
                maxSpeed += playerPortal.speedBonus;
                playerPortal = null;
            }
            if (lookBack)
            {
                cancelTime = 1.25f;
                playerPortal = null;
            }
        }
        loops.inPortal = inPortal;
        inPortal = false;

        if (lookBack && cancelTime < 0)
        {
            cameraPivot.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            cameraPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 3))
        {
            if (hit.collider.gameObject.tag == "GROUND")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 3, Color.green);
                Vector3 surfaceNormal = hit.normal;
                // Debug.Log(Vector3.Dot(surfaceNormal, currentVector));
                if ((Vector3.Dot(surfaceNormal, currentVector) > 0.7) || rb.useGravity)
                {
                    if (!rb.isKinematic)
                    {
                        if (rb.linearVelocity.magnitude > 35)
                        {
                            ShakeCamera(10);
                            targetCarSize.y *= 0.01f;
                            targetCarSize.x *= 2f;
                            targetCarSize.z *= 2f;
                        }
                    }
                    Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;

                    // transform.rotation = targetRotation;
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
                    transform.position = hit.point + hit.normal;
                    rb.isKinematic = true;
                    rb.useGravity = false;
                    currentVector = hit.normal;
                }
            }
        }
        else
        {
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(1000 * (transform.right * floorVelocity.x + transform.forward * floorVelocity.y));
                if (Vector3.Scale(transform.position, new Vector3(1, 0, 1)).magnitude > 125)
                {
                    rb.position += new Vector3(0, 6, 0);
                    rb.linearVelocity = Vector3.zero;
                    rb.AddForce(1000 * (Vector3.up * floorVelocity.y));
                    rb.AddForce(-1 * Vector3.Scale(transform.position, new Vector3(1, 0, 1)));
                    rb.linearVelocity = Vector3.Scale(rb.linearVelocity, new Vector3(0, 2, 0));
                    // rb.AddTorque(Quaternion.AngleAxis(1000f,transform.up).eulerAngles);
                    rb.AddTorque(rb.transform.up * 39f * Mathf.Sign(rotationSpeed), ForceMode.Impulse);
                    floorVelocity = new Vector2(0, 0);
                }
            }
            if (Vector3.Scale(transform.position, new Vector3(1, 0, 1)).magnitude > 125)
            {
                rb.linearVelocity = Vector3.Scale(rb.linearVelocity, new Vector3(0.15f, 1, 0.15f));
                rb.AddForce(-2f * Vector3.Scale(transform.position.normalized, new Vector3(1, 0, 1)));
            }
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2) && Vector3.Dot(Vector3.up,transform.up) < 0.3)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            // else
            // {
            // RaycastHit hit2;
            //     if (Physics.Raycast(transform.position, rb.linearVelocity, out hit2, 3))
            //     {
            //         Vector3 surfaceNormal = hit.normal;
            //         Debug.Log(Vector3.Dot(surfaceNormal, currentVector));
            //         if ((Vector3.Dot(surfaceNormal, currentVector) > 0.7) || rb.useGravity)
            //         {
            //             Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;

            //             // transform.rotation = targetRotation;
            //             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
            //             transform.position = hit.point + hit.normal;
            //             rb.isKinematic = true;
            //             rb.useGravity = false;
            //             currentVector = hit.normal;
            //         }
            //     }     
            // if (Physics.Raycast(transform.position+ new Vector3(0,3,0), new Vector3(0,-1,0), out hit2, 3))
            // {
            //     transform.position = hit.point + new Vector3(0, 1, 0);
            // }

            // }
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        rotationSpeed += moveVector.x * steering * 0.01f;
        rotationSpeed *= 0.95f;
        float localAngle = Quaternion.LookRotation(transform.forward, transform.up).eulerAngles.y;
        float localAngleRadians = localAngle * Mathf.Deg2Rad;
        // floorVelocity += new Vector2(Mathf.Cos(localAngleRadians) * moveVector.y * acceleration, Mathf.Sin(localAngleRadians) * moveVector.y * acceleration);
        if (rb.isKinematic)
        {
            floorVelocity += new Vector2(0, moveVector.y * acceleration);
        }
        floorVelocity *= 1-0.1f*Mathf.Pow(0.9f,maxSpeed);
        floorVelocity.x *= 0.99f;
        if (floorVelocity.y < 0)
        {
            floorVelocity *= 0.95f;
        }
        if (Mathf.Abs(floorVelocity.x) > 0.4 && Mathf.Abs(moveVector.x)>0 && Mathf.Sign(floorVelocity.x) == -Mathf.Sign(moveVector.x)&&rb.isKinematic)
        {
            sparksObject.GetComponent<ParticleSystem>().Play();
            sparksObject.transform.localPosition = new Vector3(-1.148f*Mathf.Sign(floorVelocity.x),-0.674f,-1.392f);
        }
        else
        {
            sparksObject.GetComponent<ParticleSystem>().Stop();
        }
        float ratio = Mathf.Pow(0.97f, floorVelocity.y);
        if (brakeDrift)
        {
            ratio = Mathf.Pow(0.85f, floorVelocity.y);
            if (Mathf.Abs(floorVelocity.x) > 0.7&&Mathf.Abs(moveVector.x)>0)
            {
                bashObject.SetActive(true);
                forceFieldObject.SetActive(true);
                bashObject.transform.localPosition = new Vector3(-Mathf.Sign(moveVector.x) * 1.5F, 0, 0.5f);
                forceFieldObject.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sign(moveVector.x) * 90f);

            }
         }
        else { bashObject.SetActive(false);forceFieldObject.SetActive(false); }  
        floorVelocity.x += -moveVector.x * floorVelocity.y * (1 - ratio);
        // floorVelocity.y *= ratio;
        if (floorVelocity.magnitude > maxSpeed)
        {
            floorVelocity.Normalize();
            floorVelocity *= maxSpeed;
        }
        // Debug.Log(floorVelocity.magnitude);
        transform.rotation = transform.rotation * Quaternion.AngleAxis(rotationSpeed, Vector3.up);
        transform.position += transform.right * floorVelocity.x + transform.forward * floorVelocity.y;
        // Vector3 floorForward = Vector3.ProjectOnPlane(forward, floorNormal).normalized;
        carObject.transform.localRotation = Quaternion.Slerp(carObject.transform.localRotation, Quaternion.AngleAxis(moveVector.x * 35, Vector3.up), 0.05f);
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].transform.localRotation = Quaternion.Slerp(wheels[i].transform.localRotation, Quaternion.AngleAxis(moveVector.x * 50, Vector3.up) * Quaternion.AngleAxis(90f, Vector3.forward), 0.05f);
        }
    }
    public void DamagePlayer(float value, bool explode, float shake)
    {
        hp -= value;
        ShakeCamera(shake);
        if (explode)
        {
            GameObject g = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            g.GetComponent<explodeOnContact>().hitPlayer = false;
        }
        targetCarSize *= Mathf.Pow(0.999f,value);
    }
    public void AwardPoints(int value)
    {
        points += value;
    }
    public void ShakeCamera(float amount)
    {
        shake.Shake(amount);
    }
}
