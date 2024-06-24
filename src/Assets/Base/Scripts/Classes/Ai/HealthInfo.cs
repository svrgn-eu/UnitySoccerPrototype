using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Ai
{
    public class HealthInfo : IBaseObject
    {
        #region Properties

        public int Health { get; private set; }

        #endregion Properties

        #region Construction

        public HealthInfo(int Health)
        {
            this.Health = Health;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
