using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using UnityEngine;
using Zenject;

namespace NET.efilnukefesin.Unity.Base
{
    /// <summary>
    /// base behaviour, enabled to run in edit mode, e.g. for asset generating classes etc
    /// </summary>
    [ExecuteInEditMode]
    public abstract class BaseEditorEnabledBehaviour : BaseBehaviour
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Start
        protected override void Start()
        {
            base.Start();

            this.SimulateDependencyInjection();
        }
        #endregion Start

        #region SimulateDependencyInjection
        protected void SimulateDependencyInjection()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)  //double check is needed
            {
                //TODO: move into some BaseEditorBehaviour class or something?
                //only use this when in edit mode 
                //TODO: find a better way - e.g. running Zenject Installers in Edit Mode
                if (this.logService is null)
                {
                    UnityInstaller unityInstaller = new UnityInstaller();
                    DiContainer diContainer = new DiContainer();
                    unityInstaller.AddBindings(diContainer);

                    this.Construct(diContainer, diContainer.Resolve<ILogService>(), diContainer.Resolve<IMessageService>(), diContainer.Resolve<IFeatureService>(), diContainer.Resolve<IConfigurationService>(), diContainer.Resolve<IObjectService>(), diContainer.Resolve<ITimeService>(), diContainer.Resolve<ITaskService>());
                }
            }
#endif
        }
        #endregion SimulateDependencyInjection

        #region OnValidate: invokes the ValidationCallback method after 0.1 seconds as a little delay is needed in the editor
        /// <summary>
        /// invokes the ValidationCallback method after 0.1 seconds as a little delay is needed in the editor
        /// </summary>
        private void OnValidate()
        {
            this.Invoke("ValidationCallback", 0.1f);  //the invoked method may not be called in Validate
        }
        #endregion OnValidate

        #region ValidationCallback: calls the EditorValidateCallback method to be used in child scripts
        /// <summary>
        /// calls the EditorValidateCallback method to be used in child scripts
        /// </summary>
        private void ValidationCallback()
        {
            this.SimulateDependencyInjection();
            this.EditorValidateCallback();
        }
        #endregion ValidationCallback

        protected abstract void EditorValidateCallback();

        #endregion Methods
    }
}
