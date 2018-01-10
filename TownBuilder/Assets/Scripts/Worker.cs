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
        if(currenyJob == Job.Worker)
        {
            if(activity == State.Idel)
            {
                activity = State.Searching;
            }

            if (activity == State.Searching)
            {
                if(JobsAndNeedsManager.toCollect.Count != 0)
                {
                    ChangeTarget(2);
                    nma.destination = target.transform.position;
                }
                else if(JobsAndNeedsManager.toHarvest.Count != 0)
                {
                    ChangeTarget(1);
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
                        StoringRecourses();
                    }
                    else
                    {
                        activity = State.Idel;
                    }
                }
            }

            if (activity == State.storing)
            {
                if(wood + stone + metal + food != carryWeight)
                {
                    activity = State.Idel;
                }
            }

            //secundair
            if (activity == State.needs)
            {

            }
        }
    }

    public void StoringRecourses()
    {
        print("kk store");
    }

    public void Harvest()
    {
        if(target != null)
        {
            print("kkfam");
            HarvestableObject harvObj = target.GetComponent<HarvestableObject>();
            harvObj.Harvest();
            StartCoroutine(Harvesting());
        }
        
    }

    public void Collect()
    {
        if(target != null)
        {
            RecoursHolder recHold = target.GetComponent<RecoursHolder>();
            recHold.CollectRecources(this);
        }
    }

    public void Build()
    {

    }

    public void BuilderActivity()
    {
        if (currenyJob == Job.Builder)
        {

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
        else if(1 == 3)
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
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(currenyJob == Job.Worker)
        {
            if (activity == State.Searching && collision.gameObject == target && target.GetComponent<HarvestableObject>())
            {
                StartCoroutine(Harvesting());
                activity = State.Harvesting;
            }
            if (activity == State.Searching && collision.gameObject == target && target.GetComponent<RecoursHolder>())
            {
                StartCoroutine(Collecting());
                activity = State.Harvesting;
            }
        }
        
        if(currenyJob == Job.Builder)
        {

        }
    }

    public IEnumerator Harvesting()
    {
        yield return new WaitForSeconds(1);
        if (target != null)
        {
            print("ok");
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
