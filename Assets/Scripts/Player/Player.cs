using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private static Player instance;
    public int chances = 3;
    public float interactionRange = 1.5f;//Range of the Interactable activation

    public float movingSpeed = 10;
    public float breakingRate = 2;
    public float cameraAccelerationX = 1;
    public float cameraAccelerationY = 0.9f;

    [Header("Items")]
    [SerializeField]
    private PlayerSlot[] itemSlots;//References to all player slots
    [SerializeField]
    private PlayerSlot[] hiddenDuringGameplay;//Items hidden during gameplay to not obscure player vision
    public Tool leftTool;//Left tool reference
    public Tool rightTool;//Right tool reference

    [Header("Input")]
    private float mXInput;//Horizontal mouse input
    private float mYInput;//Vertical mouse input
    private Vector3 currentCameraRotation;

    private Vector3 input;//Movement input

    [Header("References")]
    [SerializeField]
    private PlayerUI playerUI;
    [SerializeField]
    private Camera povCamera;
    private new Rigidbody rigidbody; 
    [SerializeField]
    private List<AudioClip> audioClips;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Transform handL;
    [SerializeField]
    private Transform handR;
    [SerializeField]
    private Transform head;

    public void Start()
    {
        if(instance && this != instance){
            Destroy(this.gameObject);
        } 
        if(instance == null) {
            instance = this;
            rigidbody = GetComponent<Rigidbody>();
        } 
    }

    void FixedUpdate(){
        
        if(!Global.IsGamePaused()){
            //Player Pitch
            transform.Rotate(0, mXInput * cameraAccelerationX * Time.fixedDeltaTime, 0);//Rotates whole body
            
            //Camera Yaw
            currentCameraRotation = head.localEulerAngles;//Rotates only the head

            float yRot = (currentCameraRotation.x + 180f) % 360f - 180f; //Fixing innacurate reading
            yRot -= cameraAccelerationY * mYInput * Time.fixedDeltaTime; // Add the mouse input
            yRot = Mathf.Clamp (yRot, -85f, 85f); // Locking yaw between -85 and 85 degrees

            head.localEulerAngles = new Vector3 (yRot, currentCameraRotation.y, currentCameraRotation.z);

            //Movement
            if(input.magnitude>0.2f){
                rigidbody.velocity = transform.rotation * input.normalized * Time.fixedDeltaTime * movingSpeed;
            } else {
                //Braking
                if(rigidbody.velocity.magnitude > 0.5)
                    rigidbody.velocity = rigidbody.velocity/(1 + Mathf.Abs(breakingRate)*Time.fixedDeltaTime);
            }
        }
    }
    
    void Update()
    {
        if(!Global.IsGamePaused()){
            //Movement
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            mXInput = Input.GetAxis("Mouse X");
            mYInput = Input.GetAxis("Mouse Y");

            //Interactions
            if(Input.GetKeyDown(KeyCode.Mouse0) && leftTool)
                leftTool.Use();
            if(Input.GetKeyDown(KeyCode.Mouse1) && rightTool)
                rightTool.Use();
            if(Input.GetKeyDown("e")){
                Interact();
            }

            PointTools();
        }

        //Misc controls
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(Input.GetKeyDown(KeyCode.F5))
            Global.ReloadScene();
    }

    //Simple raycase targeting Interactable family of objects
    private void Interact(){
        int layerMask = 1 << 0;
        RaycastHit hit;
        if( Physics.Raycast (    
                povCamera.transform.position,
                povCamera.transform.forward,
                out hit, 
                interactionRange, 
                layerMask
        )){
            Interactable i = hit.transform.GetComponent<Interactable>();
            if(i)
                i.OnInteraction();
        }
    }

    //Rotates tools in the looking direction
    private void PointTools(){
        int layerMask = LayerMask.GetMask("Default");
        RaycastHit hit;
        if (Physics.Raycast(    povCamera.transform.position,
                                povCamera.transform.forward,
                                out hit, 
                                Mathf.Infinity, 
                                layerMask))
        {
            handL.forward = (hit.point - handL.position).normalized;
            handR.forward = (hit.point - handR.position).normalized;
        }
    }

    //Statics
    private void RemoveChance(){
        chances--;
        playerUI.SetChances(instance.chances);
        if( chances <= 0 ){
            ScoreLogger.CreateSummaryScreen("Failure");
        }
    }

    //General sound player
    public static void PlaySound(int index, float pitch = 1){
        if(0 <= index && index < instance.audioClips.Count){
            instance.audioSource.Stop();
            instance.audioSource.clip = instance.audioClips[index];
            instance.audioSource.pitch = pitch;
            instance.audioSource.Play();
        }
    }

    //Invoked whenever player makes a mistake
    public static void OnMistake(string mistakeMessage, bool penalize = true,bool quiet = false){
        ScoreLogger.AddMistake(mistakeMessage);
        if(penalize)
            instance.RemoveChance();
        if(!quiet)
            PlaySound(0);
    }

    //Enables/disables the UI
    public static void SetUI(bool active){
        instance.playerUI.gameObject.SetActive(active);
    }

    //Enables/disables the attached camera
    public static void SetCamera(bool enabled){
        instance.povCamera.enabled = enabled;
    }

    //Hides (not destroys) items created by PlayerSlots assigned to hiddenDuringGameplay array. 
    public static void HideObscuringEquipment(bool hide){
        foreach(PlayerSlot ps in instance.hiddenDuringGameplay){
            ps.SetVisibility(!hide);
        }
    }

    public static Player GetPlayer(){ return instance; }
}
