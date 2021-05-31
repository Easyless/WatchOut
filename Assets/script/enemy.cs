using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{

    public Transform target;
    NavMeshAgent nav;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        followplayer();
    }

    public void followplayer()
    {
        nav.SetDestination(target.position);
    }
}
