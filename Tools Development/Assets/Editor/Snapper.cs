using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Snapper
{

    const string UNDO_STR_SNAP = "Undo Snap Objects";

    [MenuItem( "Edit/Snap Selected Objects %&S", isValidateFunction:true)]
    public static bool SnapTheThingsValidate()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("Edit/Snap Selected Objects %&S")]
    public static void SnapTheThings()
    {
        // Selected Objects on the Scene
        foreach ( GameObject go in Selection.gameObjects )
        {
            Undo.RecordObject( go.transform, UNDO_STR_SNAP );
            go.transform.position = go.transform.position.Round();   
        }
    }

    // Extension Method (Should be static class and function)
  

}
