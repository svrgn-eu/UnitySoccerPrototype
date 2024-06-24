using NET.efilnukefesin.Unity.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public abstract class BaseViewmodel : BaseBehaviour, IViewModel
    {
        #region Properties

        public GameObject PixelCamera;

        [Tooltip("Optional: if set, the current Fps will be shown")]
        public TMPro.TMP_Text FpsText;
        [Tooltip("Optional: if set, the current Fps will be shown")]
        public bool DoShowFps = false;
        #endregion Properties

        #region Construction

        public BaseViewmodel()
        {
            
        }
        #endregion Construction

        #region Methods

        #region Start
        protected override void Start()
        {
            base.Start();
            this.ActivatePixelCamera();            
        }
        #endregion Start

        #region ActivatePixelCamera
        private void ActivatePixelCamera()
        {
            if (this.PixelCamera != null)
            {
                if (this.configurationService.Get<bool>("UsePixelledLook")) //TODO: get rid of magic strings
                {
                    this.PixelCamera.SetActive(true);
                    this.logService.Info("BaseViewmodel", "ActivatePixelCamera", $"PixelCamera is being used!");
                }
                else
                {
                    this.PixelCamera.SetActive(false);
                    this.logService.Info("BaseViewmodel", "ActivatePixelCamera", $"PixelCamera is not being used as not configured to be used!");
                }
            }
            else
            {
                this.logService.Warning("BaseViewmodel", "ActivatePixelCamera", $"PixelCamera Property is null, Pixel Camera is not being used!");
            }
        }
        #endregion ActivatePixelCamera

        #region CalculateFps
        private int CalculateFps()
        { 
            int result = (int)(1f / Time.unscaledDeltaTime);
            //TODO: do the average for the last x measurements? So that it is easier readable
            return result;
        }
        #endregion CalculateFps

        #region Update
        protected virtual void Update()
        {
            if (this.FpsText != null)
            {
                if (this.DoShowFps)
                {
                    this.FpsText.text = this.CalculateFps().ToString("D3");
                }
                else
                {
                    this.FpsText.text = string.Empty;
                }
            }
        }
        #endregion Update

        #endregion Methods

        #region Events

        #endregion Events
    }
}
