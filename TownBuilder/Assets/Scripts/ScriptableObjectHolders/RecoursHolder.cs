using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoursHolder : MonoBehaviour {

    public Recourses rC;

    private void OnDestroy()
    {
        JobsAndNeedsManager.toCollect.Remove(gameObject);
    }
}
