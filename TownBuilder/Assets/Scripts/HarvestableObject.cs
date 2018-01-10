using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObject : MonoBehaviour
{
    public Recourses rC;

    public void Harvest()
    {
        if (rC.workNeeded != 0)
        {
            rC.workNeeded -= 10;
        }
        else
        {
            JobsAndNeedsManager.toCollect.Add(Instantiate(rC.ingameFormRecourse, gameObject.transform.position, Quaternion.identity));
            Destroy(gameObject);
        }
    }
}
