using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(T3Puzzle))]
public class LevelEditor : Editor
{


    public override void OnInspectorGUI()
    {
        //ScriptableObject target = this; 
        //SerializedObject so = new SerializedObject(target);

        T3Puzzle puzzle = (T3Puzzle)target;
        EditorGUILayout.Space();

        //T3Puzzle.PuzzleType puzzleType = T3Puzzle.PuzzleType.ordered;
        puzzle.puzzleType = (T3Puzzle.PuzzleType)EditorGUILayout.EnumPopup("Puzzle type", puzzle.puzzleType);
        //so.FindProperty("puzzleType").enumValueIndex = (int)(T3Puzzle.PuzzleType)EditorGUILayout.EnumPopup("Puzzle type", (T3Puzzle.PuzzleType)so.FindProperty("puzzleType").enumValueIndex);

        {
            EditorGUI.indentLevel = 0;

            GUIStyle tableStyle = new GUIStyle("box");
            tableStyle.padding = new RectOffset(10, 10, 10, 10);
            tableStyle.margin.left = 32;

            GUIStyle headerColumnStyle = new GUIStyle();
            headerColumnStyle.fixedWidth = 35;

            GUIStyle columnStyle = new GUIStyle();
            columnStyle.fixedWidth = 65;

            GUIStyle rowStyle = new GUIStyle();
            rowStyle.fixedHeight = 25;

            GUIStyle rowHeaderStyle = new GUIStyle();
            rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

            GUIStyle columnHeaderStyle = new GUIStyle();
            columnHeaderStyle.fixedWidth = 30;
            columnHeaderStyle.fixedHeight = 25.5f;

            GUIStyle columnLabelStyle = new GUIStyle();
            columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
            columnLabelStyle.alignment = TextAnchor.MiddleCenter;
            columnLabelStyle.fontStyle = FontStyle.Bold;

            GUIStyle cornerLabelStyle = new GUIStyle();
            cornerLabelStyle.fixedWidth = 42;
            cornerLabelStyle.alignment = TextAnchor.MiddleRight;
            cornerLabelStyle.fontStyle = FontStyle.BoldAndItalic;
            cornerLabelStyle.fontSize = 14;
            cornerLabelStyle.padding.top = -5;

            GUIStyle rowLabelStyle = new GUIStyle();
            rowLabelStyle.fixedWidth = 25;
            rowLabelStyle.alignment = TextAnchor.MiddleRight;
            rowLabelStyle.fontStyle = FontStyle.Bold;

            //GUIStyle enumStyle = new GUIStyle("popup");
            rowStyle.fixedWidth = 65;

            EditorGUILayout.BeginHorizontal(tableStyle);
            for (int x = -1; x < T3Puzzle.boardSize; x++)
            {
                EditorGUILayout.BeginVertical((x == -1) ? headerColumnStyle : columnStyle);
                for (int y = T3Puzzle.boardSize - 1; y >= -1; y--)
                {
                    if (x == -1 && y == -1)
                    {
                        EditorGUILayout.BeginVertical(rowHeaderStyle);
                        EditorGUILayout.LabelField("[X,Y]", cornerLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (x == -1)
                    {
                        EditorGUILayout.BeginVertical(columnHeaderStyle);
                        EditorGUILayout.LabelField(y.ToString(), rowLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (y == -1)
                    {
                        EditorGUILayout.BeginVertical(rowHeaderStyle);
                        EditorGUILayout.LabelField(x.ToString(), columnLabelStyle);
                        EditorGUILayout.EndHorizontal();
                    }

                    if (x >= 0 && y >= 0)
                    {
                        EditorGUILayout.BeginHorizontal(rowStyle);
                        if (puzzle.puzzleType == T3Puzzle.PuzzleType.ordered)
                        {
                            //SerializedProperty prop = so.FindProperty("orderedPuzzle");
                            //prop.GetArrayElementAtIndex(GetIndex(x, y)).intValue = EditorGUILayout.IntField(prop.GetArrayElementAtIndex(GetIndex(x, y)).intValue);
                            puzzle.orderedPuzzle[GetIndex(x, y)] = EditorGUILayout.IntField(puzzle.orderedPuzzle[GetIndex(x, y)]);
                        }
                        else
                        {
                            puzzle.shapePuzzle[GetIndex(x, y)] = EditorGUILayout.Toggle(puzzle.shapePuzzle[GetIndex(x, y)]);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

        }

        //so.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(puzzle);
        }
    }

    private int GetIndex(int x, int y)
    {
        return T3Puzzle.boardSize * y + x;
    }
}