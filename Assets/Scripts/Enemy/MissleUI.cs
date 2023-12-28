using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleUI : MonoBehaviour
{
    [SerializeField] Canvas worldSpaceCanvas;
    [SerializeField] GameObject missleTargetUI;
    private float aboveGround = 0.05f;
    private Vector3 missleTarget;
    private GameObject targetGameObject;
    
    //public MissleUI(Vector3 target)
    //{
    //    missleTarget = new Vector3(missleTarget.x,
    //                               aboveGround,
    //                               missleTarget.z);
    //    Instantiate(missleTargetUI, target, missleTargetUI.transform.rotation, worldSpaceCanvas.transform);
    //    // Put above Ground
    //    //missleTarget.transform.position = 
    //}

    public GameObject SetMissleUI(Vector3 target)
    {
        missleTarget = new Vector3(target.x, aboveGround, target.z);
        targetGameObject = Instantiate(missleTargetUI, missleTarget, missleTargetUI.transform.rotation, worldSpaceCanvas.transform);
        // Put above Ground
        //missleTarget.transform.position = 
        return targetGameObject;
    }

    public void Spawn()
    {
        Instantiate(missleTargetUI, missleTarget, missleTargetUI.transform.rotation, worldSpaceCanvas.transform);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
