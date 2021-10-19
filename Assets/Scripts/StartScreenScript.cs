using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DecreaseStartScreen());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DecreaseStartScreen()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDecrease", true);
    }
}
