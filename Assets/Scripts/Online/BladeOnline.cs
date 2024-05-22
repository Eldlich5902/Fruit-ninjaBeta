using Photon.Pun;
using UnityEngine;

public class BladeOnline : MonoBehaviour
{
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    private Vector3 direction;
    public Vector3 Direction => direction;

    private bool slicing;
    public bool Slicing => slicing;

    public AudioSource chem;/*Blade voice*/

    public PhotonView photonView;


    private void Start()
    {
        //DontDestroyOnLoad(this);
    }

    private void Awake()/*Gán collider*/
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();/*hiệu ứng đường chém*/
       
    }

    private void OnEnable()/*Bật blade*/
    {
        StopSliceClient();
    }

    private void OnDisable()/*Tắt blade*/
    {
        StopSliceClient();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            /*Nhấp chuột bật lưỡi đao*/
            if (Input.GetMouseButtonDown(0))
            {
                StartSliceClient(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopSliceClient();
            }
            else if (slicing)
            {
                ContinueSliceClient(Input.mousePosition);
            }
            this.gameObject.SetActive(true);
            sliceTrail.startColor = Color.green;
        }
        else sliceTrail.startColor = Color.red;
    }
    
    void StartSliceClient(Vector3 mousePosition)
    {
        if (photonView.IsMine)
            photonView.RPC("StartSlice", RpcTarget.All, mousePosition);
    }
    void StopSliceClient()
    {
        if (photonView.IsMine)
            photonView.RPC("StopSlice", RpcTarget.All);
    }

    void ContinueSliceClient(Vector3 mousePosition)
    {
        if (photonView.IsMine)
            photonView.RPC("ContinueSlice", RpcTarget.All, mousePosition);
    }    

    [PunRPC]
    private void StartSlice(Vector3 mousePosition)/*bắt đầu chém*/
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;

        sliceTrail.enabled = true;
        sliceTrail.Clear();
        chem.Play();/*bật voice*/
        
    }
    [PunRPC]
    private void StopSlice()/*ngừng chém*/
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
        
    }
    [PunRPC]
    private void ContinueSlice(Vector3 mousePosition)/*Tiếp tục chém*/
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);/*chuyển đổi từ không gian màn hình sang khong gian 3d*/
        newPosition.z = 0f;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;/*Tốc độ di chuyển = khoảng cách/thời gian*/
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
        
    }

}
