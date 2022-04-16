using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public interface ILaunchStage
{
    public event Action OnCompleted;
    public void Init();
    public void Deinit();
    public bool IsRequired();
}

public class StagedLauncher : MonoBehaviour
{
    private readonly Dictionary<string, Type> allStages = new Dictionary<string, Type>();
    private readonly IList<ILaunchStage> launchStages = new List<ILaunchStage>();
    private int index = 0;
    private Stopwatch stopwatch;
    
    public void Init()
    {
        allStages.Add("stage1", typeof(ExampleStage));
        allStages.Add("stage2", typeof(ExampleStage));
        allStages.Add("stage3", typeof(ExampleStage));
        
        launchStages.Clear();

        LoadSchema();
    }

    private void LoadSchema()
    {
        foreach (var pair in allStages)
        {
            launchStages.Add((ILaunchStage)gameObject.AddComponent(pair.Value));
        }

        stopwatch = new Stopwatch();
        index = 0;
        
        LaunchSequence();
    }

    private void LaunchSequence()
    {
        if (launchStages[index].IsRequired())
        {
            launchStages[index].OnCompleted += OnStageCompleted;
            
            stopwatch.Restart();
            Debug.Log("Starting stage: "+launchStages[index].GetType());
            
            launchStages[index].Init();
            
            return;
        }

        Debug.Log("Skipping stage: " + launchStages[index].GetType());
        Next();
    }

    private void OnStageCompleted()
    {
        launchStages[index].OnCompleted -= OnStageCompleted;
        
        Debug.Log("Ending stage: "+launchStages[index].GetType()+ " Elapsed time in ms: "+stopwatch.ElapsedMilliseconds);
        launchStages[index].Deinit();
        
        Next();
    }

    private void Next()
    {
        if (index >= launchStages.Count - 1)
        {
            Debug.Log("Finished.");
            return;
        }
            

        index++;
        LaunchSequence();
    }
}

public class ExampleStage: MonoBehaviour, ILaunchStage
{
    public event Action OnCompleted;
    public void Init()
    {
        //Some coroutine, callback, or other main thread action
    }

    public void Deinit()
    {
        //some destruction, unsub, or blank action
    }

    public bool IsRequired()
    {
        //can be default true, or runtime dependent based on other app state
        return true;
    }
}