using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NGravatar
{
    public abstract class GrofileInfo
    {
    }

    internal class GrofileInfoXml : GrofileInfo
    {
        private readonly XDocument _XDocument;

        public GrofileInfoXml(XDocument xDocument)
        {
            if (null == xDocument) throw new ArgumentNullException("xDocument");
        }
    }
}
