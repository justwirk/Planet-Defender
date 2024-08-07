using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransferPoint))] //Editoru classina bagladi
public class TransferPointEditor : Editor
{
    TransferPoint TransferPoint => target as TransferPoint; // property olustu

    private void OnSceneGUI()
    {
        for (int i = 0; i < TransferPoint.Points.Length; i++)
        {

            EditorGUI.BeginChangeCheck();

            Handles.color = Color.red;
            // handles olustu
            Vector3 currentTransferPoint = TransferPoint.CurrentPosition + TransferPoint.Points[i];
            Vector3 newTransferPointPoint = Handles.FreeMoveHandle(currentTransferPoint, 0.7f,
              new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            //Text olustur
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.yellow;
            Vector3 textLine = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(TransferPoint.CurrentPosition + TransferPoint.Points[i] + textLine,  $"{i + 1}", textStyle) ;

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck()) //transferpointi check edicek ve update edicek 
            {
                Undo.RecordObject(target, "Free Move Handle");
                TransferPoint.Points[i] = newTransferPointPoint - TransferPoint.CurrentPosition;
            }
        }
    }
}
