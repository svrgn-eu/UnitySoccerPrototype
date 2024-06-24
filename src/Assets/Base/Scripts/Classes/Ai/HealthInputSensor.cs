using NET.efilnukefesin.Lib.Common.Attributes.Ai;
using NET.efilnukefesin.Lib.Common.Attributes.Ai.Enums;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Enums;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.EventArgs;
using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using NET.efilnukefesin.Lib.Common.Services.Ai.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Ai
{
    [AiSense(AiSense.OwnHealth)]
    public class HealthInputSensor : IBaseObject, ISensor
    {
        #region Properties

        public string Name { get; private set; } = "HealthInputSensor";

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Add
        internal void Add(HealthInfo HealthInfo)
        {
            if (this.OnMessageReceived != null)
            {
                this.OnMessageReceived(this, new SensorEventArgs(new SensorData("HealthInput", HealthInfo)));  // TODO: remove magic string
            }
        }
        #endregion Add

        #endregion Methods

        #region Events

        public event EventHandler<SensorEventArgs> OnMessageReceived;

        #endregion Events
    }
}
