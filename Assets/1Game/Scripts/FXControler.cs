using System;
using UnityEngine;

public class FXControler : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX ;
    [SerializeField] private ParticleSystem csplashFX ;

    ParticleSystem.MainModule cubeExplosionFXMainModule ;

    //singleton class
    public static FXControler Instance ;

    private void Awake () {
        Instance = this ;
    }

    private void Start () {
        cubeExplosionFXMainModule = cubeExplosionFX.main ;
    }

    [Obsolete("Obsolete")]
    public void PlayCubeExplosionFX (Vector3 position, Color color) {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient (color) ;
        csplashFX.startColor = cubeExplosionFXMainModule.startColor.color;
        cubeExplosionFX.transform.position = position ;
        cubeExplosionFX.Play();
    }
    
}
