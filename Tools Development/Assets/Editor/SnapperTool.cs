using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SnapperTool : EditorWindow
{

    [MenuItem( "Tools/Snapper" )]
    public static void OpenSnapperWindow() => GetWindow<SnapperTool>( "Snapper" );

    const string UNDO_STR_SNAP = "Undo Snap Objects";

    [Range(0, 16)]
    public float gridSize = 1f;

    SerializedObject so;
    SerializedProperty propGridSize;

    void OnEnable()
    {

        so = new SerializedObject( this );
        propGridSize = so.FindProperty( "gridSize" );

        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DuringSceneGUI;

    }

    void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    void DuringSceneGUI( SceneView sceneView )
    {

        if( Event.current.type == EventType.Repaint )
        {
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
            DrawGrid();
        }
       
        //sceneView.Repaint();

    }

    private void DrawGrid()
    {



        const float gridDrawExtent = 16;

        int lineCount = Mathf.RoundToInt(( gridDrawExtent * 2 ) / gridSize );
        if(lineCount % 2 == 0)
        {
            lineCount++;
        }
        int halfLineCount = lineCount / 2;

        for( int i = 0; i < lineCount; i++ )
        {

            float intOffset = i - halfLineCount;
            float xCoord = intOffset * gridSize;
            float zCoord0 = halfLineCount * gridSize;
            float zCoord1 = -halfLineCount * gridSize;

            Vector3 p0 = new Vector3( xCoord, 0f, zCoord0 );
            Vector3 p1 = new Vector3( xCoord, 0f, zCoord1 );
            Handles.DrawAAPolyLine( p0, p1 );

            p0 = new Vector3( zCoord0, 0f, xCoord );
            p1 = new Vector3( zCoord1, 0f, xCoord );
            Handles.DrawAAPolyLine( p0, p1 );

        }

    }

    private void OnGUI()
    {
        
        so.Update();
        EditorGUILayout.PropertyField( propGridSize );
        so.ApplyModifiedProperties();

        using ( new EditorGUI.DisabledScope( Selection.gameObjects.Length == 0 ))
        {
            if ( GUILayout.Button( "Snap Selection" ) )
            {
                SnapSelection();
            }
        }

       

    }

    void SnapSelection()
    {
        foreach ( GameObject go in Selection.gameObjects )
        {
            Undo.RecordObject( go.transform, UNDO_STR_SNAP );
            go.transform.position = go.transform.position.Round( gridSize );
        }
    }

}
