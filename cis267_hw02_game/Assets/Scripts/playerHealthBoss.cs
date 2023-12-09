using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthBoss : MonoBehaviour
{
    public Image healthbar;
    private Combatant combatant;
    public GameObject thing;
    // Start is called before the first frame update
    void Start()
    {
        combatant = thing.GetComponent<Combatant>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp((float)combatant.curHealth() / combatant.healthMax(), 0, 1);
        
    }
}
