using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(RewardVideo))]
public class RewardVideoEditor : Editor
{
    //RewardVideo myScript;

    override public void OnInspectorGUI()
    {
        var myScript = target as RewardVideo;

        myScript.rewardType = (RewardVideo.RewardType)EditorGUILayout.EnumPopup("Reward Type", myScript.rewardType);

        if (myScript.rewardType == RewardVideo.RewardType.fixedReward)
        {
            myScript.totalReward = EditorGUILayout.IntField("Total Reward", myScript.totalReward);
            myScript.rewardPanel = EditorGUILayout.ObjectField("Reward Panel", myScript.rewardPanel, typeof(GameObject), true);
            myScript.rewardToAssign = EditorGUILayout.ObjectField("Text Reward To Assign", myScript.rewardToAssign, typeof(Text), true);
            myScript.rewardNotAvailable = EditorGUILayout.ObjectField("Reward Not Availble", myScript.rewardNotAvailable, typeof(GameObject), true);
            myScript.rewardLost = EditorGUILayout.ObjectField("Reward Lost", myScript.rewardLost, typeof(GameObject), true);
        }
        else if (myScript.rewardType == RewardVideo.RewardType.randomReward)
        {
            myScript.minimumRandomReward = EditorGUILayout.IntField("Minimum Reward", myScript.minimumRandomReward);
            myScript.maximumRandomReward = EditorGUILayout.IntField("Maximum Reward", myScript.maximumRandomReward);
            myScript.rewardPanel = EditorGUILayout.ObjectField("Reward Panel", myScript.rewardPanel, typeof(GameObject), true);
            myScript.rewardToAssign = EditorGUILayout.ObjectField("Reward To Assign", myScript.rewardToAssign, typeof(Text), true);
            myScript.rewardNotAvailable = EditorGUILayout.ObjectField("Reward Not Availble", myScript.rewardNotAvailable, typeof(GameObject), true);
            myScript.rewardLost = EditorGUILayout.ObjectField("Reward Lost", myScript.rewardLost, typeof(GameObject), true);
        }
        else if (myScript.rewardType == RewardVideo.RewardType.doubleReward)
        {
            myScript.rewardNotAvailable = EditorGUILayout.ObjectField("Reward Not Availble", myScript.rewardNotAvailable, typeof(GameObject), true);
            myScript.rewardLost = EditorGUILayout.ObjectField("Reward Lost", myScript.rewardLost, typeof(GameObject), true);
        }
        else if (myScript.rewardType == RewardVideo.RewardType.reviveReward)
        {
            myScript.rewardNotAvailable = EditorGUILayout.ObjectField("Reward Not Availble", myScript.rewardNotAvailable, typeof(GameObject), true);
            myScript.rewardLost = EditorGUILayout.ObjectField("Reward Lost", myScript.rewardLost, typeof(GameObject), true);
        }
        else if (myScript.rewardType == RewardVideo.RewardType.freeSpinReward)
        {
            myScript.rewardNotAvailable = EditorGUILayout.ObjectField("Reward Not Availble", myScript.rewardNotAvailable, typeof(GameObject), true);
            myScript.rewardLost = EditorGUILayout.ObjectField("Reward Lost", myScript.rewardLost, typeof(GameObject), true);
        }
    }
}