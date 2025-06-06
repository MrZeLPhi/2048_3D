
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private static int staticID = 0;
    [SerializeField] private TMP_Text[] numbersText;

     public int CubeID;
     public Color CubeColor;
     public int CubeNumber;
     public Rigidbody CubeRigidbody;
     public bool isMainCube;

    private MeshRenderer cubeMeshRenderer;
    
    private void Awake()
    {
        CubeID = staticID++;
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        CubeColor = color;
        cubeMeshRenderer.material.color = color;
    }

    public void SetNumber(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < numbersText.Length; i++)
        {
            numbersText[i].text = number.ToString();
        }
    }

    
}
