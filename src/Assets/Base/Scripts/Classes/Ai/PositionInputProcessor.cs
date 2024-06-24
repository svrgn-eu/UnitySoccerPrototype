using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services.Ai.Objects;
using NET.efilnukefesin.Lib.Common.Services.Ai.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Ai
{
    public class PositionInputProcessor : BaseProcessor, IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Construction

        public PositionInputProcessor(IObjectService objectService)
            : base(objectService)
        {
            this.Name = "PositionInputProcessor";
            this.SensorDataToProcess = "PositionInput";
            this.DefaultWeight = 0.8f;  //threshold for deletion is 0.9f, this can be reorganized
        }

        #endregion Construction

        #region Methods

        #region CanHandle
        public override bool CanHandle(ISensorData data)
        {
            bool result = false;

            if (data.Name.Equals(this.SensorDataToProcess) && data.DataType.Equals(typeof(PositionInfo)))
            {
                result = true;
            }

            return result;
        }
        #endregion CanHandle

        #region Process
        public override IEnumerable<IInformation> Process(ISensorData data)
        {
            IInformation newInfo = this.ObjectService.Create<Information>(this.Name, data.Data, data.DataType, this.DefaultWeight);
            yield return newInfo;
        }
        #endregion Process

        #endregion Methods

        #region Events

        #endregion Events
    }
}
