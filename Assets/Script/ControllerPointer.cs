using UnityEngine;
using UnityEngine.EventSystems;
using static OVRInput;

[RequireComponent(typeof(LineRenderer))]
public class ControllerPointer : MonoBehaviour
{
    [SerializeField]
    private SetUITransformRay uiRays;
    private LineRenderer pointerLine;
    private GameObject tempPointerVals;
    public Transform gazePointerIcon;
    public GameObject HitObject;
    public Transform AppCamera;
    private GameObject DragObject;
    public GameObject Pointer;

    public GameObject PreviousHitObject = null;

    public Color InitLineColor;
    public Color TiggerColor;

    private LineRenderer lr;
    private int layer_mask;
    private void Start()
    {

        tempPointerVals = new GameObject();
        tempPointerVals.transform.parent = transform;
        tempPointerVals.name = "tempPointerVals";
        pointerLine = gameObject.GetComponent<LineRenderer>();
        pointerLine.useWorldSpace = true;

        ControllerInfo.CONTROLLER_DATA_FOR_RAYS = tempPointerVals;
        uiRays.SetUIRays();
        lr = GetComponent<LineRenderer>();

    }

    private void LateUpdate()
    {

        Quaternion rotation = GetLocalControllerRotation(ControllerInfo.CONTROLLER);
        Vector3 position = GetLocalControllerPosition(ControllerInfo.CONTROLLER);
        Vector3 pointerOrigin = ControllerInfo.TRACKING_SPACE.position + position;
        Vector3 pointerProjectedOrientation = ControllerInfo.TRACKING_SPACE.position + (rotation * ControllerInfo.TRACKING_SPACE.forward); //(rotation * Vector3.forward);
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        Vector3 pointerDrawStart = pointerOrigin - pointerProjectedOrientation * 0.05f;
        Vector3 pointerDrawEnd = pointerDrawStart + (gazePointerIcon.transform.position - pointerDrawStart).normalized * 1.5f; //pointerOrigin + pointerProjectedOrientation * 500.0f; //gazePointerIcon.transform.position;//
        pointerLine.SetPosition(0, pointerDrawStart);
        pointerLine.SetPosition(1, pointerDrawEnd);

        tempPointerVals.transform.position = pointerDrawStart;
        tempPointerVals.transform.rotation = rotation;
    }
}