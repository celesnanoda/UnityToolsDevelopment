using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExplosiveBarrelManager : MonoBehaviour
{
    public static List<ExplosiveBarrel> explosiveBarrels = new List<ExplosiveBarrel>();

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // default => CompareFunction.Always
        Handles.zTest = CompareFunction.LessEqual;

        foreach( var barrel in explosiveBarrels )
        {

            if (barrel.barrelType == null) continue ;

            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfHeight = (managerPos.y - barrelPos.y) * .5f;
            Vector3 tangentOffset = Vector3.up * halfHeight;

            Handles.DrawBezier( managerPos, 
                                barrelPos, 
                                managerPos - tangentOffset, 
                                barrelPos + tangentOffset,
                                barrel.barrelType.color, 
                                EditorGUIUtility.whiteTexture,
                                1f);
           
        }
        Handles.color = Color.white;

    }
#endif
}
