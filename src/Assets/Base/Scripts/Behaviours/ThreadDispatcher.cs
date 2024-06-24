using NET.efilnukefesin.Unity.Base;
using NET.efilnukefesin.Unity.Base.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadDispatcher : BaseBehaviour
{
    #region Properties

    static volatile bool isAnythingQueued = false;
    static List<Action> backlogActions = new List<Action>(8);
    static List<Action> actionsForExecution = new List<Action>(8);

    #endregion Properties

    #region Methods

    #region Awake
    private void Awake()
    {
        this.taskService.SetDispatcher(this);  //set the dispatcher object of the very specific UnityTaskService to this class
    }
    #endregion Awake

    #region Update
    private void Update()
    {
        if (ThreadDispatcher.isAnythingQueued)
        {
            lock (backlogActions)
            {
                var tmp = ThreadDispatcher.actionsForExecution;
                ThreadDispatcher.actionsForExecution = ThreadDispatcher.backlogActions;
                ThreadDispatcher.backlogActions = tmp;
                ThreadDispatcher.isAnythingQueued = false;
            }

            foreach (var action in ThreadDispatcher.actionsForExecution)
            {
                action();
            }

            ThreadDispatcher.actionsForExecution.Clear();
        }
    }
    #endregion Update

    #region AddToBacklog
    public void AddToBacklog(Action NewAction)
    {
        lock (ThreadDispatcher.backlogActions)
        {
            ThreadDispatcher.backlogActions.Add(NewAction);
            ThreadDispatcher.isAnythingQueued = true;
        }
    }
    #endregion AddToBacklog

    #endregion Methods
}
