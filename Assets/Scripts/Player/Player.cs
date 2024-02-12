using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private static Player instance;
    public int chances = 3;
    public float interactionRange = 1.5f;

    public float movingSpeed = 10;
    public float cameraAccelerationX = 1;
    public float cameraAccelerationY = 0.9f;

    [Header("Items")]
    [SerializeField]
    private LabeledTransform[] itemSlots;
    [SerializeField]
    private Tool leftTool;
    [SerializeField]
    private Tool rightTool;

    [Header("Input")]
    [SerializeField]
    private float mXInput;
    [SerializeField]
    private float mYInput;
    public Vector3 currentCameraRotation;

    private Vector3 input;
    private bool allowMovement;

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

    // Start is called before the first frame update
    void Start()
    {
        if(instance){
            Destroy(this.gameObject);
        } else {
            instance = this;
            rigidbody = GetComponent<Rigidbody>();
        } 
    }

    void FixedUpdate(){
        //Pitch
        transform.Rotate(0, mXInput * cameraAccelerationX, 0);
        
        //Yaw
        currentCameraRotation = head.localEulerAngles;

        float yRot = (currentCameraRotation.x + 180f) % 360f - 180f; //Fixing innacurate reading
        yRot -= cameraAccelerationY * mYInput; // Add the mouse input
        yRot = Mathf.Clamp (yRot, -85f, 85f); // Lock yaw to be between -85 and 85 degrees

        head.localEulerAngles = new Vector3 (yRot, currentCameraRotation.y, currentCameraRotation.z);

        //Movement
        if(input.magnitude>0.2f){
            rigidbody.velocity = transform.rotation * input.normalized * movingSpeed;
        }
        
    }
    
    void Update()
    {
        if(!Global.IsGamePaused()){
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            mXInput = Input.GetAxis("Mouse X");
            mYInput = Input.GetAxis("Mouse Y");

            //ScrollWheelAction(Input.GetAxis("Mouse ScrollWheel"));//Move to tool classes later;

            if(Input.GetKeyDown(KeyCode.Mouse0))
                leftTool.Use();
                //HazardTestBeam();
            if(Input.GetKeyDown(KeyCode.Mouse1))
                rightTool.Use();
                //ScanTestBeam();
            if(Input.GetKeyDown("e")){
                Interact();
            }

            PointTools();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(Input.GetKeyDown(KeyCode.F5))
            Global.ReloadScene();
        
    }

    public static void RemoveChance(){
        instance.chances--;
        instance.playerUI.SetChances(instance.chances);
        if( instance.chances <= 0 ){
            ScoreLogger.CreateSummaryScreen("Failure");
        }
    }

    public static void PlaySound(int index, float pitch = 1){
        if(0 <= index && index < instance.audioClips.Count){
            instance.audioSource.Stop();
            instance.audioSource.clip = instance.audioClips[index];
            instance.audioSource.pitch = pitch;
            instance.audioSource.Play();
        }
    }
    public static void OnMistake(string mistakeMessage, bool penalize = true,bool quiet = false){
        ScoreLogger.AddMistake(mistakeMessage);
        if(penalize)
            RemoveChance();
        if(!quiet)
            PlaySound(0);
    }
    public static void SetUI(bool active){
        instance.playerUI.gameObject.SetActive(active);
    }

    public void Interact(){
        int layerMask = 1 << 0;
        RaycastHit hit;
        if( Physics.Raycast (    
                povCamera.transform.position,
                povCamera.transform.forward,//TransformDirection(Vector3.forward),
                out hit, 
                interactionRange, 
                layerMask
        )){
            
            Interactable i = hit.transform.GetComponent<Interactable>();
            if(i)
                i.OnInteraction();
        }
    }

    //MOVE BELOW TO IT'S RESPECTIVE TOOL CLASSES
    public int debugHazardSetting = 0;
    public void HazardTestBeam(){
        int layerMask = LayerMask.GetMask("Default", "Hazard");//1 << 6;
        RaycastHit hit;
        if (Physics.Raycast(    povCamera.transform.position,
                                povCamera.transform.forward,//TransformDirection(Vector3.forward),
                                out hit, 
                                Mathf.Infinity, 
                                layerMask))
        {
            Hazard h = hit.transform.GetComponent<Hazard>();
            if(h)
                h.OnFix(debugHazardSetting);
            
            Debug.Log("Found "+hit.transform.name);
        }
    }

    public void ScanTestBeam(){
        int layerMask = LayerMask.GetMask("Default", "Hazard");//1 << 6;
        RaycastHit hit;
        if (Physics.Raycast(    povCamera.transform.position,
                                povCamera.transform.forward,//TransformDirection(Vector3.forward),
                                out hit, 
                                Mathf.Infinity, 
                                layerMask))
        {
            Hazard h = hit.transform.GetComponent<Hazard>();
            if(h)
                Debug.Log("Scan of "+hit.transform.name+":"+h.OnScan());
        }
    }

    public void ScrollWheelAction(float input){
        if(Mathf.Abs(input) > 0f){
            if(input > 0){
                if(debugHazardSetting < 9)
                    debugHazardSetting++;
            } else {
                if(debugHazardSetting > 0)
                    debugHazardSetting--;
            }

            Debug.Log("Tool set to "+debugHazardSetting);
        }
    }

    private void PointTools(){
        int layerMask = LayerMask.GetMask("Default");
        RaycastHit hit;
        if (Physics.Raycast(    povCamera.transform.position,
                                povCamera.transform.forward,//TransformDirection(Vector3.forward),
                                out hit, 
                                Mathf.Infinity, 
                                layerMask))
        {
            handL.forward = (hit.point - handL.position).normalized;
            handR.forward = (hit.point - handR.position).normalized;
        }
    }
}
