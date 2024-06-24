using NET.efilnukefesin.Unity.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour : BaseBehaviour
{
    #region Properties

    [HelpBox("this is a demo property showing the mighty help box")]
    public string DemoProperty;

    #endregion Properties

    #region Construction

    #endregion Construction

    #region Methods

    #region Start
    protected override void Start()
    {
        base.Start();
    }
    #endregion Start

    #endregion Methods

    #region Events

    #endregion Events
}
