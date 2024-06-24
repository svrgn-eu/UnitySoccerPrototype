using NET.efilnukefesin.Lib.Common.Extensions;
using NET.efilnukefesin.Lib.Common.Interfaces;
using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services;
using NET.efilnukefesin.Lib.Common.Services.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Base.Scripts.Services
{
    public class UnityObjectService : BaseService, IObjectService, IInitialize
    {
        #region Properties

        public bool IsInitialized { get; private set; } = false;

        private DiContainer diContainer;

        private List<IObjectInfo> objects;

        #endregion Properties

        #region Construction

        public UnityObjectService(IServiceRegistry ServiceRegistry, ILogService LogService, DiContainer Container)
            : base(ServiceRegistry, LogService)
        {
            this.diContainer = Container;

            this.Initialize();
        }

        #endregion Construction

        #region Methods

        #region Initialize
        public void Initialize()
        {
            if (!this.IsInitialized)
            {
                this.objects = new List<IObjectInfo>();
                this.IsInitialized = true;
            }
        }
        #endregion Initialize

        #region Create
        public T Create<T>(params object[] Parameters) where T : class, IBaseObject
        {
            T result = default;
            this.logService.Info("UnityObjectService", "Create", $"Attempting to create an object of type '{typeof(T)}'");

            result = this.CreateWithoutStoring<T>(Parameters);

            if (result != null)
            {
                ObjectInfo newObjectInfo = this.CreateWithoutStoring<ObjectInfo>(result, typeof(T));
                if (newObjectInfo != null)
                {
                    this.objects.Add(newObjectInfo);

                    if (result.DoesImplementInterface<IInitialize>())
                    {
                        this.logService.Info("UnityObjectService", "Create", $"Attempting to initialize a new object of type '{typeof(T)}'");
                        ((IInitialize)result).Initialize();
                    }
                }
                else
                {
                    //objectInfo could not be created
                    this.logService.Warning("UnityObjectService", "Create", $"Could not create Object Info for new object of type '{typeof(T)}'");
                }
            }
            else
            {
                //objectInfo could not be created
                this.logService.Error("UnityObjectService", "Create", $"Could not create Object for new object of type '{typeof(T)}' with parameter(s) '{string.Join("; ", Parameters)}'");
            }

            this.logService.Info("UnityObjectService", "Create", $"Ended creation of a new object of type '{typeof(T)}'");

            return result;
        }
        #endregion Create

        #region CreateWithTypeName
        public T CreateWithTypeName<T>(string TypeName, params object[] Parameters) where T : class, IBaseObject
        {
            T result = default;
            this.logService.Info("UnityObjectService", "CreateWithTypeName", $"Attempting to create an object of type '{typeof(T)}'");
            result = this.CreateWithoutStoringWithText<T>(TypeName, Parameters);

            if (result != null)
            {
                ObjectInfo newObjectInfo = this.CreateWithoutStoring<ObjectInfo>(result, typeof(T));
                if (newObjectInfo != null)
                {
                    lock (this.objects)
                    {
                        this.objects.Add(newObjectInfo);
                    }
                    if (result.DoesImplementInterface<IInitialize>())
                    {
                        this.logService.Info("UnityObjectService", "CreateWithTypeName", $"Attempting to initialize a new object of type '{typeof(T)}'");
                        ((IInitialize)result).Initialize();
                    }
                }
                else
                {
                    //objectInfo could not be created
                    this.logService.Warning("UnityObjectService", "CreateWithTypeName", $"Could not create Object Info for new object of type '{typeof(T)}'");
                }
            }
            else
            {
                //objectInfo could not be created
                this.logService.Error("UnityObjectService", "CreateWithTypeName", $"Could not create Object for new object of type '{typeof(T)}' with parameter(s) '{string.Join("; ", Parameters)}'");
            }

            this.logService.Info("UnityObjectService", "CreateWithTypeName", $"Ended creation of a new object of type '{typeof(T)}'");

            return result;
        }
        #endregion CreateWithTypeName

        #region CreateWithoutStoring
        private T CreateWithoutStoring<T>(params object[] Parameters) where T : class, IBaseObject
        {
            T result = default;
            bool isInterface = typeof(T).IsInterface;
            bool wasSuccessful = false;

            this.logService.Debug("UnityObjectService", "CreateWithoutStoring", $"Attempting to create an object of type '{typeof(T)}'; isInterface '{isInterface}'; Parameter Types '{Parameters.GetTypeNames()}'");

            // if T is an interface look up registered type
            if (isInterface)
            {
                //TODO: how to pass parameters???
                if (this.diContainer.HasBinding<T>())
                {
                    //look up types
                    //this.diContainer.AllContracts.ToList().ForEach(contract => contract.
                    try
                    {
                        result = this.diContainer.Resolve<T>();
                        wasSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        this.logService.Error("UnityObjectService", "CreateWithoutStoring", $"Failed when resolving the object of type '{typeof(T)}'; isInterface '{isInterface}'; Parameter Types '{Parameters.GetTypeNames()}'", ex);
                    }
                }
                else
                {
                    //report error
                    this.logService.Error("UnityObjectService", "CreateWithoutStoring", $"Container has no binding to interface '{typeof(T)}'");
                }
            }
            else
            {
                result = this.InstantiateSafe<T>(Parameters);  // TODO: consider this a workaround (instatiate so long until we receive a result or abort after  attempts
                if (result != null)
                { 
                    wasSuccessful = true;
                }
            }

            if (wasSuccessful)
            {
                this.logService.Info("ObjectService", "CreateWithoutStoring", $"Successfully created the object '{result.GetType()}'");
            }

            return result;
        }
        #endregion CreateWithoutStoring

        #region InstantiateSafe
        private T InstantiateSafe<T>(params object[] Parameters) where T : class, IBaseObject
        {
            T result = default;
            bool wasSuccessful = false;
            bool isAborted = false;
            int numberOfTries = 0;
            int numberOfMaxTries = 5;
            while (!wasSuccessful && !isAborted)
            {
                numberOfTries++;
                try
                {
                    this.logService.Debug("UnityObjectService", "InstantiateSafe", $"Starting to instantiate the object of type '{typeof(T)}'; Try Number '{numberOfTries}'");
                    result = this.diContainer.Instantiate<T>(Parameters);
                    this.logService.Debug("UnityObjectService", "InstantiateSafe", $"Succeeded when instantiating the object of type '{typeof(T)}'; Try Number '{numberOfTries}'");
                    wasSuccessful = true;
                }
                catch (Exception ex)
                {
                    string typeName = typeof(T).Name;
                    this.logService.Warning("UnityObjectService", "InstantiateSafe", $"Failed when instantiating the object of type '{typeof(T)}'; Try Number '{numberOfTries}'", ex);
                }

                if (numberOfTries >= numberOfMaxTries)
                {
                    isAborted = true;
                    this.logService.Error("UnityObjectService", "InstantiateSafe", $"Needed to abort instantiation of '{typeof(T)}'; After '{numberOfTries}' tries");
                }
            }

            return result;
        }
        #endregion InstantiateSafe

        #region CreateWithoutStoringWithText
        public T CreateWithoutStoringWithText<T>(string TypeName, params object[] Parameters) where T : class, IBaseObject
        {
            T result = default;
            this.logService.Info("UnityObjectService", "CreateWithoutStoringWithText", $"Attempting to create an object of type '{typeof(T)}'");
            try
            {
                Type type = TypeName.ToType(true);
                result = (T)this.diContainer.Instantiate(type, Parameters);
                this.logService.Info("UnityObjectService", "CreateWithoutStoringWithText", $"Successfully created the object");
            }
            catch (Exception ex)
            {
                this.logService.Error("UnityObjectService", "CreateWithoutStoringWithText", $"Failed when creating the object", ex);
            }
            return result; 
        }
        #endregion CreateWithoutStoringWithText

        #region Count
        public int Count()
        {
            int result = -1;

            if (this.IsInitialized)
            {
                result = this.objects.Count;
            }

            return result;
        }
        #endregion Count

        #region Instantiate
        internal GameObject Instantiate(GameObject Prefab, Transform Parent)
        {
            GameObject result = default;

            result = this.diContainer.InstantiatePrefab(Prefab, Parent);

            result.SetActive(true);  //activate just in case the object in the prefab is deactivated
            this.RemoveCloneFromName(result);

            return result;
        }
        #endregion Instantiate

        #region Instantiate
        internal GameObject Instantiate(GameObject Prefab, Transform Parent, Vector3 Position)
        {
            GameObject result = default;

            result = this.diContainer.InstantiatePrefab(Prefab, Position, Quaternion.identity, Parent);

            result.SetActive(true);  //activate just in case the object in the prefab is deactivated
            this.RemoveCloneFromName(result);

            return result;
        }
        #endregion Instantiate

        #region RemoveCloneFromName
        private void RemoveCloneFromName(GameObject gameObject)
        {
            gameObject.name = gameObject.name.Replace("(Clone)", string.Empty).Trim();  //clean up the name
        }
        #endregion RemoveCloneFromName

        #endregion Methods

        #region Events

        #endregion Events
    }
}
