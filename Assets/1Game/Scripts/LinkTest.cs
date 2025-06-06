using UnityEngine;

public class LinkTest : MonoBehaviour
{
    [SerializeField] private string _link = "";

    public void OpenWebsite()
    {
        Application.OpenURL(_link);
    }

    [SerializeField] private string[] _linkMax ;

    public void OpenMaxWebsiteExit()
    {
        for (int i = 0; i < _linkMax.Length; i++)
            Application.OpenURL(_linkMax[i]);
    }
}
