using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System;

public class ThreadQueuer : MonoBehaviour
{
    static List<Action> functionsToRunInMainThread;

    void Start()
    {
        functionsToRunInMainThread = new List<Action>();
    }

    void Update()
    {
        if(functionsToRunInMainThread.Count != 0)
        {
            while (functionsToRunInMainThread.Count > 0)
            {
                Action func = functionsToRunInMainThread[0];
                functionsToRunInMainThread.RemoveAt(0);
                func();
            }
        }
    }

    public void StartThreadedFunction(Action function)
    {
        Thread t = new Thread(new ThreadStart(function));
        t.Start();
    }

    public void QueueMainThreadFunction(Action function)
    {
        functionsToRunInMainThread.Add(function);
    }
}
