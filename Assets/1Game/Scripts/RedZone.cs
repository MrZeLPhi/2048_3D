
using UnityEngine;

public class RedZone : MonoBehaviour
{
    public UISceneManagement _uiSceneManagement;
    
    private void OnTriggerStay (Collider other) {
        Cube cube = other.GetComponent <Cube> () ;
        if (cube != null) {
            if (!cube.isMainCube && cube.CubeRigidbody.linearVelocity.magnitude < .1f)
            {
                _uiSceneManagement.GoToScene("2");
            }
        }
    }
}
