
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushFlorce;
    [SerializeField] private float cubeMaxPosX;
    
    [Space] [SerializeField] private TouchSlider _touchSlider;
    
    private Cube mainCube;

    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    private void Start()
    {
        SpawnCobe();
        canMove = true;
        // Listen to slider events:
        _touchSlider.OnPointerDownEvent += OnPointerDown;
        _touchSlider.OnPointerDragEvent += OnPointerDrag;
        _touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            mainCube.transform.position  = Vector3.Lerp(
            mainCube.transform.position,
            cubePos,
            moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }
    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
            
            // Push the cube:
             mainCube.CubeRigidbody.AddForce(Vector3.forward*pushFlorce,ForceMode.Impulse);
             
             Invoke("SpawnNewCube",0.3f);
        }
    }

    private void SpawnNewCube()
    {
        mainCube.isMainCube = false;
        canMove = true;
        SpawnCobe();
    }

    private void SpawnCobe()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.isMainCube = true;
        
        // reset cubePos variable
        cubePos = mainCube.transform.position;
    }

    private void OnDestroy()
    {
        // remove Listeners
        _touchSlider.OnPointerDownEvent -= OnPointerDown;
        _touchSlider.OnPointerDragEvent -= OnPointerDrag;
        _touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
