using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour {

    public int stone;
    public int wood;
    public int metal;
    public int food;
    public int carryWeight;

    public NavMeshAgent nma;
    public JobsAndNeedsManager jm;
    public GameObject target;

    public enum State
    {
        Idel,
        Searching,
        Harvesting,
        Collecting,
        storing,
        building,
        needs
    }

    public enum Job
    {
        Worker,
        Builder
    }
    public Job currenyJob;
    public State activity;

    void Start()
    {
        nma = gameObject.GetComponent<NavMeshAgent>();
        jm = GameObject.FindObjectOfType<JobsAndNeedsManager>();
    }

    void Update()
    {
        WorkerActivity();
        BuilderActivity();
    }

    public void ChangeJob(int i)
    {
        if(i == 1)
        {
            currenyJob = Job.Worker;
        }
        if(i == 2)
        {
            currenyJob = Job.Builder;
        }
    }

    public void WorkerActivity()
    {
        //print(activity + " Worker");
        if(currenyJob == Job.Worker)
        {
            if(activity == State.Idel)
            {
                activity = State.Searching;
            }

            if (activity == State.Searching)
            {
                if(JobsAndNeedsManager.toHarvest.Count != 0)
                {
                    ChangeTarget(1);
                    nma.destination = target.transform.position;
                }
                else if (JobsAndNeedsManager.toCollect.Count != 0 && wood + stone + metal + food != carryWeight)
                {
                    ChangeTarget(2);
                    nma.destination = target.transform.position;
                }
                else if (wood + stone + metal + food >= 0 && JobsAndNeedsManager.Storage.Count != 0)
                {
                    ChangeTarget(4);
                    nma.destination = target.transform.position;
                }
            }

            if (activity == State.Harvesting)
            {
                if(target == null)
                {
                    activity = State.Idel;
                }
            }

            if (activity == State.Collecting)
            {
                if (target == null )
                {
                    if(stone + wood + metal + food == carryWeight)
                    {
                        activity = State.storing;
                    }
                    else
                    {
                        activity = State.Idel;
                    }
                }
            }

            if (activity == State.storing)
            {
                if(JobsAndNeedsManager.Storage.Count == 0)
                {
                    activity = State.Idel;
                }
                else
                {
                    ChangeTarget(4);
                    nma.destination = target.transform.position;
                }
            }

            //secundair
            if (activity == State.needs)
            {

            }
        }
    }

    public void BuilderActivity()
    {
        if (currenyJob == Job.Builder)
        {
            print(activity + " Builder");
            if(activity == State.Idel)
            {
                if (JobsAndNeedsManager.toBuild.Count != 0)
                {
                    if (target == null || target.GetComponent<Buildings>().health >= target.GetComponent<Buildings>().maxHealth)
                    {
                        ChangeTarget(3);
                    }
                    if (target != null)
                    {
                        nma.destination = target.transform.position;
                        activity = State.Searching;
                    }
                }
            }

            if(activity == State.building && target == null)
            {
                activity = State.Idel;
            }

            if(activity == State.Searching)
            {
                if(target == null)
                {
                    activity = State.Idel;
                }
            }
        }
    }

    public void StoringRecourses()
    {
        StorageOpen storage = target.GetComponent<StorageOpen>();
        storage.woodStorage += wood;
        storage.stoneStorage += stone;
        storage.MetalNeeded += metal;
        storage.foodStorage += food;
        wood = 0;
        stone = 0;
        metal = 0;
        food = 0;
    }

    public void Harvest()
    {
        if(target != null)
        {
            HarvestableObjectHolder hOH = target.GetComponent<HarvestableObjectHolder>();
            if (hOH.harvestProgress != hOH.hO.workNeeded)
            {
                hOH.harvestProgress += 10;
                if (hOH.harvestProgress == hOH.hO.workNeeded)
                {
                    JobsAndNeedsManager.toCollect.Add(Instantiate(hOH.hO.ingameFormRecourse, target.transform.position, Quaternion.identity));
                    Destroy(target);
                }
            }
            if(hOH.harvestProgress != hOH.hO.workNeeded)
            {
                StartCoroutine(Harvesting());
            }
        }
    }

    public void Collect()
    {
        if(target != null)
        {
            RecoursHolder recHold = target.GetComponent<RecoursHolder>();
            wood += recHold.rC.wood;
            stone += recHold.rC.stone;
            metal += recHold.rC.metal;
            food += recHold.rC.food;
            Destroy(target);
        }
    }

    public void Build()
    {
        if(activity == State.building)
        {
            Buildings gebouw = target.GetComponent<Buildings>();
            gebouw.CheckProgresOnBuilding(this);
            StartCoroutine(Building());
        }
    }

    public void ChangeTarget(int i)
    {
        if(i == 1)
        {
            float dist = Mathf.Infinity;
            foreach (GameObject g in JobsAndNeedsManager.toHarvest)
            {
                Vector3 diff = g.transform.position - transform.position;
                float curDist = diff.sqrMagnitude;
                if (curDist < dist)
                {
                    target = g;
                    dist = curDist;
                }
            }
        }
        else if(i == 2)
        {
            float dist = Mathf.Infinity;
            foreach (GameObject g in JobsAndNeedsManager.toCollect)
            {
                Vector3 diff = g.transform.position - transform.position;
                float curDist = diff.sqrMagnitude;
                if (curDist < dist)
                {
                    target = g;
                    dist = curDist;
                }
            }
        }
        else if(i == 3)
        {
            float dist = Mathf.Infinity;
            foreach (GameObject g in JobsAndNeedsManager.toBuild)
            {
                Vector3 diff = g.transform.position - transform.position;
                float curDist = diff.sqrMagnitude;
                if (curDist < dist)
                {
                    target = g;
                    dist = curDist;
                }
            }
        }
        else if (1 == 4)
        {
            float dist = Mathf.Infinity;
            foreach (GameObject g in JobsAndNeedsManager.Storage)
            {
                Vector3 diff = g.transform.position - transform.position;
                float curDist = diff.sqrMagnitude;
                if (curDist < dist)
                {
                    target = g;
                    dist = curDist;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(currenyJob == Job.Worker)
        {
            if (activity == State.Searching && other.gameObject == target && target.GetComponent<HarvestableObjectHolder>())
            {
                activity = State.Harvesting;
                StartCoroutine(Harvesting());
            }
            if (activity == State.Searching && other.gameObject == target && target.GetComponent<RecoursHolder>())
            {
                activity = State.Collecting;
                StartCoroutine(Collecting());
            }
            if (activity == State.storing && other.gameObject == target && target.GetComponent<StorageOpen>())
            {
                StoringRecourses();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currenyJob == Job.Builder)
        {
            if (activity == State.Searching && other.gameObject == target && target.GetComponent<Buildings>().health != target.GetComponent<Buildings>().maxHealth)
            {
                print("collisionWhitTarget");
                activity = State.building;
                StartCoroutine(Building());
            }
        }
    }
    public IEnumerator Harvesting()
    {
        yield return new WaitForSeconds(1);
        if (target != null)
        {
            Harvest();
        }
    }

    public IEnumerator Collecting()
    {
        yield return new WaitForSeconds(1);
        if(target != null)
        {
            Collect();
        }
    }

    public IEnumerator Building()
    {
        yield return new WaitForSeconds(1);
        if (target != null)
        {
            Build();
        }
    }
}
