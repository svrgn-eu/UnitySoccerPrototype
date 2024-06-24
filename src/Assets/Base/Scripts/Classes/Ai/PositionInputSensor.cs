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
    [AiSense(AiSense.Position)]
    public class PositionInputSensor : IBaseObject, ISensor
    {
        #region Properties

        public string Name { get; } = "PositionInputSensor";

        #endregion Properties

        #region Methods

        #region Add
        internal void Add(PositionInfo PositionInfo)
        {
            if (this.OnMessageReceived != null)
            {
                this.OnMessageReceived(this, new SensorEventArgs(new SensorData("PositionInput", PositionInfo)));  // TODO: remove magic string
            }
        }
        #endregion Add

        #endregion Methods

        #region Events

        public event EventHandler<SensorEventArgs> OnMessageReceived;

        #endregion Events
    }
}
