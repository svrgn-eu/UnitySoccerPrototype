using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Objects;
using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Base.Scripts.Classes.Ai
{
    public class VisualTargetSensorDebugData : IBaseObject, IVisualSensorDebugData
    {
        #region Properties

        public string Name { get; private set; }

        #endregion Properties

        #region Construction

        public VisualTargetSensorDebugData(string Name)
        {
            this.Name = Name;
            //TODO: add other interesting facts as
            //target position etc
        }

        #endregion Construction

        #region Methods

        public void Draw()
        {
            throw new NotImplementedException();
        }

        #endregion Methods

        #region Events

        #endregion Events
    }
}
