using UnityEditor;
using UnityEngine;

public class QuestionEditor : EditorWindow
{
    private string inputText;

    [MenuItem("Window/Question Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestionEditor>("Question Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Enter your text below:", EditorStyles.boldLabel);

        inputText = EditorGUILayout.TextArea(inputText, GUILayout.Height(position.height - 100));

        if (GUILayout.Button("Create QuestionSO"))
        {
            CreateQuestionSO();
        }
    }

    private void CreateQuestionSO()
    {
        string[] blocks = inputText.Split(new[] { "\n\n" }, System.StringSplitOptions.None);

        foreach (var block in blocks)
        {
            string[] lines = block.Split(new[] { "\n" }, System.StringSplitOptions.None);

            string question = lines[0].Substring("Question: ".Length);
            string[] answers = new string[4];
            for (int i = 0; i < 4; i++)
            {
                answers[i] = lines[i + 1].Substring(3); // Skip "a. ", "b. ", etc.
            }

            int correctAnswer = lines[6][8] - 'a'; // 'a' => 0, 'b' => 1, etc.
            string factoid = lines[8].Substring("Fact: ".Length);

            var questionSO = ScriptableObject.CreateInstance<QuestionSO>();
            questionSO.question = question;
            questionSO.answers = answers;
            questionSO.correctAnswer = correctAnswer;
            questionSO.factoid = factoid;

#if UNITY_EDITOR
            var questionCount = System.IO.Directory.GetFiles("Assets/Questions").Length / 2;
            var filename = "Question" + (questionCount + 1);
            AssetDatabase.CreateAsset(questionSO, $"Assets/Questions/{filename.Replace(" ", "_").Replace("?", "")}.asset");
            AssetDatabase.SaveAssets();
#endif
        }
    }
}
