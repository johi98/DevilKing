using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    bool isDamged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack" )
        {
            //isDamged = true;
            Debug.Log("오크 맞음");
            //StartCoroutine("OrcDamaged");
        }
    }

    IEnumerator OrcDamaged()
    {
        
        Debug.Log("오크 맞음");
        //yield return new WaitForSeconds(0.19f);
        isDamged = false;
        yield return null;
    }
}
