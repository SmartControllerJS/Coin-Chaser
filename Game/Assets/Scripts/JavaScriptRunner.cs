using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Added 
using Jint;
using System;
using System.IO;
using Jint.Runtime;
using System.Linq;

public class JavaScriptRunner : MonoBehaviour
{
    private Engine engine;

    private void Execute(string fileName)
    {
        var body = "";
        try
        {
            body = Resources.Load<TextAsset>(fileName).text;
            engine.Execute(body);
        }
        catch (JavaScriptException ex)
        {
            var location = engine.GetLastSyntaxNode().Location.Start;
            var error = $"Jint runtime error {ex.Error} {fileName} (Line {location.Line}, Column {location.Column})\n{PrintBody(body)}";
            UnityEngine.Debug.LogError(error);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error: {ex.Message} in {fileName}\n{PrintBody(body)}");
        }
    }

    private static string PrintBody(string body)
    {
        if (string.IsNullOrEmpty(body)) return "";
        string[] lines = body.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        return string.Join("\n", Enumerable.Range(0, lines.Length).Select(i => $"{i + 1:D3} {lines[i]}"));
    }


    // Start is called before the first frame update
    void Start()
    {
        engine = new Engine();

        // SetValue and Execute action we perform, add the objects to the same JavaScript scope.
        // It means, that any code in index.js will have access to the log or any other objects we inject
        engine.SetValue("log", new Action<object>(msg => Debug.Log(msg)));
        engine.Execute("var window = this");
        Execute("app");

        // Get hello function from js file
        engine.Execute("hello()");
        var functionResult = engine.GetCompletionValue().AsString();
        Debug.Log("C# got function result from Javascript: " + functionResult);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



