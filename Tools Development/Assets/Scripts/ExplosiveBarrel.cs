using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{

    public BarrelType barrelType;

    static readonly int shPropColor = Shader.PropertyToID("_Color");

    MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb {
        get
        {
            if(mpb == null)
                mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }

    void TryApplyColor()
    {
        if (barrelType == null) return;

        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mpb.SetColor( shPropColor, barrelType.color );
        rnd.SetPropertyBlock( Mpb );
    }

    // Called on Property Change
    private void OnValidate() => TryApplyColor();

    private void OnEnable()
    {
        TryApplyColor();
        ExplosiveBarrelManager.explosiveBarrels.Add(this);
    }

    private void OnDisable() => ExplosiveBarrelManager.explosiveBarrels.Remove( this );

    private void OnDrawGizmosSelected()
    {
        if (barrelType == null) return;

        Handles.color = barrelType.color;
        Handles.DrawWireDisc(transform.position, transform.up, barrelType.radius);
        Handles.color = Color.white;

    }

}
