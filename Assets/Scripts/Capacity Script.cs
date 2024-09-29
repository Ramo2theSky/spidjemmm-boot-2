using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CapacityScript : MonoBehaviour
{
    [SerializeField] int maxCapacity = 3;
    [SerializeField] int capacityFilled = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fillTrash(int weight) {
        capacityFilled += weight;
    }
}
