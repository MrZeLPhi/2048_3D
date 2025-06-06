
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject _obj1Rote;
    public GameObject _obj2Rote;

    [Tooltip("Швидкість обертання об'єкта в градусах за секунду.")]
    public float _Obj1rotationSpeed = 50f;
    public float _Obj2rotationSpeed = 10f;


    [Tooltip("Вісь, навколо якої буде обертатися об'єкт.")]
    public Vector3 rotationAxis = Vector3.up; // Vector3.up - це (0, 1, 0)

    void Update()
    {
        _obj1Rote.transform.Rotate(rotationAxis * _Obj1rotationSpeed * Time.deltaTime);
        _obj2Rote.transform.Rotate(rotationAxis * _Obj2rotationSpeed * Time.deltaTime);
        
        if (_obj2Rote.transform.rotation.y >= 380f)
        {
            _obj2Rote.transform.localEulerAngles = Vector3.zero;
        }
        if (_obj1Rote.transform.rotation.y >= 380f)
        {
            _obj1Rote.transform.localEulerAngles = Vector3.zero;
        }
        
    }
}
