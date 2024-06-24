using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HelpBoxAttribute : PropertyAttribute
    {
        #region Properties

        public string Text { get; }
        public string DocsUrl { get; }
        public HelpBoxType Type { get; }

        #endregion Properties

        #region Construction

        public HelpBoxAttribute(string text, string docsUrl = null, HelpBoxType type = HelpBoxType.Info)
        {
            this.Text = text;
            this.DocsUrl = docsUrl;
            this.Type = type;
        }

        #endregion Construction
    }
}
