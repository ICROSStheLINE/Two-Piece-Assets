using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSOBasicAttack : MonoBehaviour
{
    [SerializeField] GameObject theHitbox;
    
	Animator anim;
    void Start()
    {
         anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
