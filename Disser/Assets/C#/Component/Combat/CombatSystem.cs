using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private HealthStats HS;
    private float Damage;
    // Start is called before the first frame update
    void Start()
    {
        HS = GetComponent<HealthStats>();
    }

    public bool Attack(HealthStats hs)
    {
        Damage = HS.HowDamage();
        hs.CalculateHealth(Damage);
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
