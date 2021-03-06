using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

//script per la gestione del player
public class Player : MonoBehaviour
{
    //parametri del player
    public float keyboardSpeed;
    public float mouseSpeed;
    public bool invertXAxis, invertYAxis;
    public bool enableParenting;
    public static float noiseLevel;
    public GameObject[] Prefabs;
    public static bool MouseLookToggle;
    public Image power;
    public Sprite[] powers;
    public int maxHealth=100;
    public int currentHealth;
    public HealthBar healthBar;
    public float maxEnergy = 50;
    public float currentEnergy;
    public EnergyBar energyBar;
    public static bool isPower;
    public static bool isPowerUsed;
    public static bool secret;
    public AudioClip walk;
    public AudioClip run;
    public AudioMixerGroup walkM;
    public AudioMixerGroup runM;

    private Transform myTransform;
    private Transform myCameraTransform;
    private CharacterController myCharacterController;
    private float xMov = 0;
    private float yMov = 0;
    private float zMov = 0;
    private float xRot, yRot;
    private GameObject currentPrefabObject;
    private FireBaseScript currentPrefabScript;
    private int currentPrefabIndex;
    private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX = 15F;
    private float sensitivityY = 15F;
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRotation;
    private string sceneN;
    private bool isMoving;
    private bool attackAnim;
    private AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FinalChapter")
        {
            keyboardSpeed = 10;
        }
        source = GetComponent<AudioSource>();
        sceneN=SceneManager.GetActiveScene().name;
        attackAnim=false;
        isMoving = false;
        secret = false;
        isPower = false;
        isPowerUsed = false;
        myTransform = GetComponent<Transform>();
        myCameraTransform = myTransform.GetChild(0);
        xRot = myCameraTransform.localRotation.eulerAngles.x;
        yRot = myTransform.rotation.eulerAngles.y;
        myCharacterController = GetComponent<CharacterController>();
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        originalRotation = myCameraTransform.localRotation;
        MouseLookToggle = true;
        noiseLevel = 0;
        StartCoroutine("noiseControl");
        if(SceneManager.GetActiveScene().name!="Tutorial")
            StartCoroutine("waitAudio");
    }

    void Update()
    {
        //esegui blocco se gioco non ? in pausa
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {
            //gestione rotazione e poteri del player
            if (invertXAxis != SettingsManager.invertX)
            {
                invertXAxis = SettingsManager.invertX;
            }

            if (invertYAxis != SettingsManager.invertY)
            {
                invertYAxis = SettingsManager.invertY;
            }

            if (mouseSpeed != SettingsManager.sensibility)
            {
                mouseSpeed = SettingsManager.sensibility;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                NextPrefab();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                PreviousPrefab();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                MouseLookToggle = !MouseLookToggle;
            }

            
                //Character Y rotation
                int xDir = 1;
                if (invertXAxis)
                {
                    xDir = -1;
                }
                yRot = yRot + xDir * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
                myTransform.rotation = Quaternion.Euler(0, yRot, 0);

                //Camera X rotation
                int yDir = -1;
                if (invertYAxis)
                {
                    yDir = 1;
                }
                xRot = xRot + yDir * Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;
                //myCameraTransform.localRotation = Quaternion.Euler(xRot, 0, 0);
                myCameraTransform.localRotation = Quaternion.Euler(Mathf.Clamp(xRot, -90, 90),0,0);
            

            UpdateEffect();

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (currentEnergy < maxEnergy)
                    ReloadEnergy();
            }

            xMov = Input.GetAxis("Horizontal");
            zMov = Input.GetAxis("Vertical");

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) &&
                    !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && !attackAnim)
            {
                isMoving = false;
                if(sceneN!="Tutorial")
                    transform.gameObject.GetComponent<Animator>().Play("idle1");
            }
            else
            {
                isMoving = true;
            }
        }
    }

    private void FixedUpdate()
    {
        //esegui blocco se gioco non ? in pausa
        if (GameManagerLogic.state != GameManagerLogic.State.pause && GameManagerLogic.state != GameManagerLogic.State.death)
        {
            //gestione del movimento e della corsa
            if (!myCharacterController.isGrounded)
            {
                yMov += Physics.gravity.y * Time.fixedDeltaTime ;
            }
            else
            {
                yMov = 0;
                if (enableParenting)
                {
                    if (Physics.Raycast(myTransform.position, Vector3.down, out RaycastHit raycastHit))
                    {
                        myTransform.parent = raycastHit.transform;
                    }
                }
            }
            if (xMov != 0 || yMov != 0 || zMov != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) && energyBar.GetEnergy() > 0)
                {
                    if (isMoving && !attackAnim && sceneN != "Tutorial")
                    {
                        source.clip = run;
                        source.outputAudioMixerGroup = runM;
                        transform.gameObject.GetComponent<Animator>().Play("Run");
                    }
                    myCharacterController.Move(myTransform.TransformDirection(new Vector3(xMov * keyboardSpeed*2, yMov, zMov * keyboardSpeed*2) * Time.fixedDeltaTime));
                    ConsumeEnergy(.2f);
                    noiseLevel = 2;
                }
                else
                {
                    if (isMoving && !attackAnim && sceneN != "Tutorial")
                    {
                        source.clip = walk;
                        source.outputAudioMixerGroup = runM;
                        transform.gameObject.GetComponent<Animator>().Play("Walk");
                    }
                    myCharacterController.Move(myTransform.TransformDirection(new Vector3(xMov * keyboardSpeed, yMov, zMov * keyboardSpeed) * Time.fixedDeltaTime));
                    noiseLevel = 1;
                }

                
            }
        }
    }

    //metodo per riprodurre passi
    IEnumerator waitAudio()
    {
        while (true)
        {
            if (isMoving)
            {
                source.Play();
                yield return new WaitWhile(() => source.isPlaying);
            }
            yield return new WaitForSeconds(1);
        }
    }

    
    //metodo che gestisce i poteri del player
        private void UpdateEffect()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                int energy = 0;
                if (currentPrefabIndex == 0)
                {
                    attackAnim = true;
                    if(sceneN!="Tutorial")
                        transform.gameObject.GetComponent<Animator>().Play("Attack1");
                    energy = 15;
                }else if (currentPrefabIndex == 1)
                {
                    attackAnim = true;
                    if (sceneN != "Tutorial")
                        transform.gameObject.GetComponent<Animator>().Play("Attack5");
                    energy = 25;
                }
                else
                {
                    attackAnim = true;
                    if (sceneN != "Tutorial")
                        transform.gameObject.GetComponent<Animator>().Play("Attack2");
                    energy = 45;
                }
                if (energyBar.GetEnergy() > energy) 
                {
                    StartCoroutine("WaitForAnimation");
                    StartCurrent();
                    ConsumeEnergy(energy);
            }
            else
            {
                attackAnim = false;
            }
            }
        }

    //metodo che attiva l'effetto del potere
        private void BeginEffect()
        {
            Vector3 pos;
            float yRot = transform.rotation.eulerAngles.y;
            Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            Vector3 up = transform.up;
            Quaternion rotation = Quaternion.identity;
            currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);
            currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

            if (currentPrefabScript == null)
            {
                // temporary effect, like a fireball
                currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
                if (currentPrefabScript.IsProjectile)
                {
                    // set the start point near the player
                    rotation = transform.rotation;
                pos = transform.position + forward +up;// + right + up;
                }
                else
                {
                // set the start point in front of the player a ways
                pos = transform.position;//+ (forwardY * 10.0f);
                }
            }
            else
            {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
                rotation = transform.rotation;
                pos.y = 0.0f;
            }

            FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
            if (projectileScript != null)
            {
                // make sure we don't collide with other fire layers
                projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FireLayer"));
            }

            currentPrefabObject.transform.position = pos;
            currentPrefabObject.transform.rotation = rotation;
            if(secret)
                isPowerUsed = true;
            else
            {
                isPowerUsed = false;
            }
        }

    //metodo che interrompe effetto precedente e avvia il nuovo
        public void StartCurrent()
        {
            StopCurrent();
            BeginEffect();
        }

    //metodo che interrompe l'effetto
        private void StopCurrent()
        {
            // if we are running a constant effect like wall of fire, stop it now
            if (currentPrefabScript != null && currentPrefabScript.Duration > 10000)
            {
                currentPrefabScript.Stop();
            }
            currentPrefabObject = null;
            currentPrefabScript = null;
        }

    //passa al prossimo effetto
    public void NextPrefab()
    {
        currentPrefabIndex++;
        if (currentPrefabIndex == Prefabs.Length)
        {
            currentPrefabIndex = 0;
        }
        power.sprite = powers[currentPrefabIndex];
        if (currentPrefabIndex == 1)
            isPower = true;
        else
        {
            isPower = false;
        }
    }

    //passa all'effetto precedente
    public void PreviousPrefab()
    {
        currentPrefabIndex--;
        if (currentPrefabIndex == -1)
        {
            currentPrefabIndex = Prefabs.Length - 1;
        }
        power.sprite = powers[currentPrefabIndex];
        if (currentPrefabIndex == 1)
            isPower = true;
        else
        {
            isPower = false;
        }
    }

    //ricarica stamina
        void ReloadEnergy()
    {
        currentEnergy += 0.2f;
        energyBar.SetEnergy(currentEnergy);
    }

    //consuma stamina
    void ConsumeEnergy(float energy)
    {
        currentEnergy -= energy;
        energyBar.SetEnergy(currentEnergy);    
    }

    //gestore del rumore
    IEnumerator noiseControl()
    {
        while(true){
            if (!Input.anyKey)
                noiseLevel=0;
            yield return new WaitForSeconds(1);
        }
    }

    //attendi conclusione animazione attacco
    IEnumerator WaitForAnimation()
    {
        if (sceneN != "Tutorial")
            yield return new WaitForSeconds(transform.GetComponent<Animator>().runtimeAnimatorController.animationClips[5].length);
        attackAnim = false;
        yield return new WaitForSeconds(1);
    }
}
