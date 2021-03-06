using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class CannonController : ComponentController {   
    public GameObject Shell;
    public float[] reload_timer;
    public const float RELOAD_TIME = 1f;

    public void Cooldown()
    {
        int num_cannons = Mathf.FloorToInt(GetComponent<SpriteRenderer>().size.x - 1);
        if (reload_timer.Length != num_cannons)
        {
            reload_timer = new float[num_cannons];
        }
        for (int i = 0; i < reload_timer.Length; i++)
        {
            reload_timer[i] -= .02f;
        }
    }

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }
        if (input >= 1 && input <= reload_timer.Length)
        {
            int barrel = Mathf.FloorToInt(input - 1);
            if (reload_timer[barrel] < 0)
            {
                reload_timer[barrel] = RELOAD_TIME;
                GameObject shell = Instantiate(
                    Shell,
                    this.transform.position,
                    this.transform.rotation,      
                    this.transform
                ) as GameObject;
                shell.transform.Translate(new Vector2((barrel + .5f) - reload_timer.Length / 2f, GetComponent<SpriteRenderer>().size.y / 2f));
                shell.transform.SetParent(GameObject.Find("World").transform);
                return 1;
            }
        }
        return 0;
    }
  
    public override string GetDescription() 
    {
        return "\n <b>Cannons</b> fire \n projectiles \n when triggered; \n\n Component(input)\n> Fire(barrel[input])";
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < reload_timer.Length; i++)
        {
            output += "\n Barrel[" + (i + 1) + "]: " + Mathf.Clamp(reload_timer[i], 0, RELOAD_TIME);  
        }
        return "\n " + this.name + "\n" + output + "\n\n" + GetDescription();
    }
}