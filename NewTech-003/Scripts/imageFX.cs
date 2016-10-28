using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
//using ValentijnsAssets.CocainStash;

public class imageFX : MonoBehaviour {

    internal Vortex vortexEffect;
    internal Fisheye fisheyeEffect;
    internal DepthOfField dof;
    internal Bloom bloom;

    // Random values for oscillation of the property
    internal float randomRange1;
    internal float randomRange2;
    internal float randomRange3;
    internal int randomRange4;
    internal float randomRange5;
    internal float randomRange6;

    internal float dofFocalLenght = 3.38f;
    internal float dofFocalSize = 2f;
    internal float dofAperture = 37.56f;

    internal float bloomThreshold = 0.59f;

    // -------------------------------------<<<- <<<- <<<- FUNCTIONEZ EL LOCOS

    void Start ()
    {
        randomRange1 = Random.Range(-5, 5);
        randomRange2 = Random.Range(0, 0.2f);
        randomRange3 = Random.Range(-0.4f, 0.4f);
        randomRange4 = Random.Range(-5, 5);
        randomRange5 = Random.Range(-0.4f, 0.4f);
        randomRange6 = Random.Range(-0.1f, 0.1f);

        vortexEffect = GetComponent<Vortex>();
        fisheyeEffect = GetComponent<Fisheye>();
        dof = GetComponent<DepthOfField>();

        dofFocalLenght = dof.focalLength;
        dofFocalSize = dof.focalSize;
        dofAperture = dof.aperture;

        bloom = GetComponent<Bloom>();
        bloom.bloomIntensity = 0.09f; ;
        bloom.bloomThreshold = -0.06f; ;

    }

    void Update ()
    {
        vortexEffect.angle = Mathf.PingPong(Time.time * 20, randomRange1 * 10.0f) + 0.01f ;
        fisheyeEffect.strengthX = Mathf.PingPong(Time.time * 40, randomRange2);
        fisheyeEffect.strengthY = Mathf.PingPong(Time.time * 40, randomRange3);

        dof.focalLength = Mathf.PingPong(Time.time * 20, dofFocalLenght + randomRange4 * 10.0f); ;
        dof.focalSize = Mathf.PingPong(Time.time * 20, dofFocalSize + randomRange4 * 10.0f); ;
        dof.aperture = Mathf.PingPong(Time.time * 20, dofAperture + randomRange4 * 10.0f); ;

        bloom.bloomThreshold = Mathf.PingPong(Time.time * 0.5f, randomRange5 * 1.2f) + 0.01f;
        bloom.bloomIntensity = Mathf.PingPong(Time.time * 0.5f, randomRange6 * 1.42f) + 0.01f;

        if (float.IsNaN(vortexEffect.angle) || float.IsNaN(bloom.bloomIntensity)) 
            // Check if isNaN! Which happens randomly!
            // Set new randomRange values and 
        {
            randomRange1 = Random.Range(-5, 5);
            randomRange2 = Random.Range(0, 0.2f);
            randomRange3 = Random.Range(-0.4f, 0.4f);
            randomRange4 = Random.Range(-5, 5);
        }
    }

    public void DestroyValentijn()
    {
    /*
        if(currentplayer.name == "Valentijn")
        {
              Application.Quit();
        }
    */
    }

}
