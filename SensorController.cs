using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class SensorController : ComponentController {   //RangeFinder == 1D, Scanner == 2D?
    // public float max_step = 25f;
    public bool organicSensor;
    private float distance = 0, distance_min = 0, distance_max = 999, scan_range = 0;

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }
        
        LayerMask mask = LayerMask.GetMask("Default"); //Larger sensors can detect more layers of detail (see individual components, shells, etc.)
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 2, Vector2.up * 999,999, mask, -999, 999);
        if (hit != null && hit.collider != null)
        {
            distance = hit.distance;
            Debug.DrawRay(transform.position, Vector2.up * distance, Color.green, .02f, false);
            print (hit.collider.gameObject.name);
            return distance;
        }
        else 
            distance = distance_max;
            // Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward), Color.green, .001f, false);
            return distance_max;
    }
    public override string GetDescription() 
    {
        return "\n <b>Sensors</b> measure \n the range to \n the closest \n object in the \n line of sight;\n\n Component() \n> return Raycast().length";
    }
    public override Vector2 GetMinimumSize ()
    {
        return new Vector2(2, 2);
    }

    public override string ToString()
    {
        // string output = "";
        // float[] spectrum = new float[64];
        // var listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        // AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        // for (int i = 0; i < 64; i++) {
        //     for (float j = 0; j < spectrum[i]; j += .01f)
        //     {
        //         output += "█";
        //     }
        //     output += "\n";
        // }
        return "\n " + this.name + "\n\n Range: " + distance + "\n\n" + GetDescription();
    }
}