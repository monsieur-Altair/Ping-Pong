using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenScpript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void WinScreenAppear()
    {
        animator.SetBool("isAppear", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
