using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnitySimpleLiquid;

public class MilkLiquid : MonoBehaviour
{
    //public LiquidContainer liquidContainer;
    [SerializeField]
    private float bottleneckRadius = 0.1f;
    public float BottleneckRadiusWorld { get; private set; }

    [Tooltip("How fast liquid split from container")]
    public float splitSpeed = 2f;
    [Tooltip("Number number of objects the liquid will hit off and continue flowing")]
    public int maxEdgeDrops = 4;
    private int currentDrop;

    public ParticleSystem particlesPrefab;
    public Component bone;

    #region Particles
    private ParticleSystem particles;
    public GameObject pivot;
    private bool isOpen = false;
    private int cycleControllerTimeChecker = 0;

    public ParticleSystem Particles

    {
        get
        {
            if (!particlesPrefab)
                return null;

            if (!particles)
                particles = Instantiate(particlesPrefab, pivot.transform);
            return particles;
        }
    }

    public void StartEffect()
    {
        var particlesInst = Particles;
        if (!particlesInst)
            return;

        var mainModule = particlesInst.main;


        particlesInst.transform.localPosition = pivot.transform.localPosition;
        particlesInst.transform.rotation = pivot.transform.rotation;
        particlesInst.Play();
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(bone.transform.position.y);

        if (bone.transform.position.y > 0.5427284) isOpen = true;
        if (bone.transform.position.y < 0.508686 && isOpen == true)
        {
            CicleController();       
        }
        else
        {
            cycleControllerTimeChecker = 0;
        }
       
    }

    void CicleController()
    {
        if (cycleControllerTimeChecker <= 15)
        {
            StartEffect();
            cycleControllerTimeChecker++;
        }
        else
        {
            isOpen = false;
        }
    }
}
