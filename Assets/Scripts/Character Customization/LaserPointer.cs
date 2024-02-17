using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    
    public float cameraAccelerationX = 1;
    public float cameraAccelerationY = 0.9f;
    private float mXInput;
    private float mYInput;

    private Item itemStorage;

    [Header("References")]
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    PointerBall pointer;
    [SerializeField]
    SpriteRenderer pointersDisplay;
    [SerializeField]
    CharacterCustomization characterCustomizationRoot;

    public Vector3 point;
    public Vector3 lineEnd;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.SetPosition(0,transform.position);
    }

    void FixedUpdate(){
        transform.Rotate(-cameraAccelerationY * mYInput * Time.fixedDeltaTime, mXInput * cameraAccelerationX * Time.fixedDeltaTime, 0);
        
        Vector3 currentRotation = transform.localEulerAngles;
        
        //Locking rotation
        float rot = (currentRotation.x + 180f) % 360f - 180f; //Fixing innacurate reading
        rot = Mathf.Clamp (rot, -40f, 25f); // Locking pitch
        currentRotation.x = rot;

        rot = (currentRotation.y + 180f) % 360f - 180f; //Fixing innacurate reading
        rot = Mathf.Clamp (rot, -50f, 50f); // Locking yaw
        currentRotation.y = rot;

        currentRotation.z = 0;
        transform.localEulerAngles = currentRotation;

        //Moving
        int layerMask = LayerMask.GetMask("Default", "UI");//1 << 6;
        RaycastHit hit;
        if ( Physics.Raycast(   transform.position,
                                transform.forward,
                                out hit, 
                                8, 
                                layerMask))
        {
            pointer.transform.position = hit.point;
        }

        //Points line towards the target
        if(lineRenderer.positionCount >= 2)
            lineRenderer.SetPosition(1,pointer.transform.position);
        //*/
    }

    void Update()
    {
        mXInput = Input.GetAxis("Mouse X");
        mYInput = Input.GetAxis("Mouse Y");

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            PressButton();
        }
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            ReleaseButton();
        }
    }

    void PressButton(){
        int layerMask = LayerMask.GetMask("UI");//1 << 6;
        RaycastHit hit;
        if ( Physics.Raycast(   transform.position,
                                transform.forward,
                                out hit, 
                                Mathf.Infinity, 
                                layerMask))
        {
            PhysUIElement pue = hit.collider.transform.GetComponent<PhysUIElement>();
            if(pue){
                pue.OnPress(this);
                if(HasItem())
                    characterCustomizationRoot.ShowAvailability(itemStorage.itemType);
            }
        }
    }

    void ReleaseButton(){
        int layerMask = LayerMask.GetMask("UI");//1 << 6;
        RaycastHit hit;
        if ( Physics.Raycast(   transform.position,
                                transform.forward,
                                out hit, 
                                Mathf.Infinity, 
                                layerMask))
        {
            PhysUIElement pue = hit.collider.transform.GetComponent<PhysUIElement>();
            if(pue)
                pue.OnRelease(this);
        }
        ClearItem();
        characterCustomizationRoot.HideAvailability();
    }

    public void StoreItem(Item i){
        itemStorage = i;
        pointersDisplay.sprite = i.thumbnail;
    }
    public Item ClearItem(){
        Item returnedItem = itemStorage;
        itemStorage = null;
        pointersDisplay.sprite = null;
        return returnedItem;
    }
    public bool HasItem(){ return itemStorage != null;}
    public ItemType StoredItemType(){ 
        if(itemStorage) return itemStorage.itemType;
        else return ItemType.NONE;
    }
}
