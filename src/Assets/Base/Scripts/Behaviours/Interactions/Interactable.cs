using NET.efilnukefesin.Lib.Common.Interfaces;
using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.Interactions
{
    public class Interactable : BaseBehaviour, IName
    {
        #region Properties

        public string Name { get; } = "DemoInteractable";
        public string Verb = "interact";

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region OnInteraction
        public void Interact()
        {
            this.logService.Info("Interactable", "OnInteraction", $"OnInteraction has been triggered!");
            this.OnInteraction?.Invoke(this, new EventArgs());
        }
        #endregion OnInteraction

        #endregion Methods

        #region Events

        public event EventHandler OnInteraction;

        #endregion Events
    }
}
