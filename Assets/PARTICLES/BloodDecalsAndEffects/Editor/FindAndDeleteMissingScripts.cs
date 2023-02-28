using UnityEngine;
using UnityEditor;
public class FindAndDeleteMissingScripts : EditorWindow
{
    private static int gameObjectCount = 0;
    private static int componentsCount = 0;
    private static int missingCount = 0;

    [MenuItem("Window/Find Or Delete Missing Scripts Recursively")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(FindAndDeleteMissingScripts));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
        {
            FindInSelected();
        }
        if (GUILayout.Button("Delete Missing Scripts in selected GameObjects"))
        {
            DeleteMissingScripts();
        }
    }
    private static void FindInSelected()
    {
        GameObject[] go = Selection.gameObjects;
        gameObjectCount = 0;
        componentsCount = 0;
        missingCount = 0;
        foreach (GameObject g in go)
        {
            FindMissingScripts(g);
        }
        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", gameObjectCount, componentsCount, missingCount));
    }
    private static void FindMissingScripts(GameObject g)
    {
        gameObjectCount++;
        Component[] components = g.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            componentsCount++;
            if (components[i] == null)
            {
                missingCount++;
                string s = g.name;
                Transform t = g.transform;
                while (t.parent != null)
                {
                    s = t.parent.name + "/" + s;
                    t = t.parent;
                }
                Debug.Log(s + " has an empty script attached in position: " + i, g);
            }
        }
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindMissingScripts(childT.gameObject);
        }
    }
    private static void DeleteMissingScripts()
    {
        gameObjectCount = 0;
        componentsCount = 0;
        missingCount = 0;

        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            gameObjectCount++;

            var gameObject = Selection.gameObjects[i];

            // We must use the GetComponents array to actually detect missing components
            var components = gameObject.GetComponents<Component>();

            // Create a serialized object so that we can edit the component list
            var serializedObject = new SerializedObject(gameObject);
            // Find the component list property
            var prop = serializedObject.FindProperty("m_Component");

            // Track how many components we've removed
            int r = 0;

            // Iterate over all components
            for (int j = 0; j < components.Length; j++)
            {
                componentsCount++;

                // Check if the ref is null
                if (components[j] == null)
                {
                    missingCount++;
                    // If so, remove from the serialized component array
                    prop.DeleteArrayElementAtIndex(j - r);
                    // Increment removed count
                    r++;
                }
            }

            // Apply our changes to the game object
            serializedObject.ApplyModifiedProperties();
        }

        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found and deleted {2} missing", gameObjectCount, componentsCount, missingCount));
    }
}