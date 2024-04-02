using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject spellPrefab;
   
    
    public void SpellProjectile()
    {

      GameObject spell = Instantiate(spellPrefab,launchPoint.position,spellPrefab.transform.rotation);

        Vector3 origScale = spell.transform.localScale;

        //Face the spell the same direction of the plater
        spell.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x >0 ? 1 : -1,
            origScale.y,
            origScale.z
           );
    }


}
