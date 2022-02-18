using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(BarrelType))]
public class ExplosiveBarrelEditor : Editor
{

    public enum Type
    {
        A, B, C
    }

    Type type;

    SerializedObject so;
    SerializedProperty propRadius;
    SerializedProperty propDamage;
    SerializedProperty propColor;

    private void OnEnable()
    {
        so = serializedObject;
        propRadius = so.FindProperty( "radius" );
        propDamage = so.FindProperty( "damage" );
        propColor = so.FindProperty( "color" );
    }

    public override void OnInspectorGUI()
    {
       
        so.Update();
        EditorGUILayout.PropertyField( propRadius );
        EditorGUILayout.PropertyField( propDamage );
        EditorGUILayout.PropertyField( propColor );
        if( so.ApplyModifiedProperties() )
        {
            ExplosiveBarrelManager.UpdateAllBarrelColors();
        }


    }

    public void BasicInspectorGUI()
    {
        //@Basic Inspector UI \\
        // base.OnInspectorGUI();

        BarrelType barrel = target as BarrelType;



        GUILayout.Label( "radius: " + barrel.radius );

        float newRadius = EditorGUILayout.FloatField( "radius", barrel.radius );
        if ( newRadius != barrel.radius )
        {
            Undo.RecordObject( barrel, "Undo Barrel Radius Change" );
            barrel.radius = newRadius;
        }
        barrel.damage = EditorGUILayout.FloatField( "Damage", barrel.damage );
        barrel.color = EditorGUILayout.ColorField( "Color", barrel.color );
    }
    public void CustomInspector()
    {

        // First Type
        GUILayout.BeginHorizontal();

        // Make Label
        GUILayout.Label( "Barrel", GUILayout.Width( 60 ) );

        // Make Button
        if ( GUILayout.Button( "Click" ) )
        {

        }

        // Make Dropdown
        type = ( Type ) EditorGUILayout.EnumPopup( type );


        GUILayout.EndHorizontal();

        GUILayout.Label( "Barrel", GUI.skin.button );
        GUILayout.Space( 40 );
        GUILayout.Label( "Category", EditorStyles.boldLabel );

        EditorGUILayout.ObjectField( "Assign Here", null, typeof( Transform ), true );

        // Second Type
        using ( new GUILayout.HorizontalScope( EditorStyles.helpBox ) )
        {
            // Make Label
            GUILayout.Label( "Barrel", GUILayout.Width( 60 ) );

            // Make Button
            if ( GUILayout.Button( "Click" ) )
            {

            }

            // Make Dropdown
            type = ( Type ) EditorGUILayout.EnumPopup( type );
        }

        // Explicit Positioning
        // GUI => both in game and editor
        // EditorGUI =>

        // Implicit Positioning / Auto Layout
        // GUILayout =>
        // EditorGUILayout =>


    }

}
