
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.30.1.  DO NOT EDIT!
//*************************************************************************************

using System;
using System.Threading;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Bridge;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{

    public class DesignerUiBridgeBase : IDesignerUiBridge
    {
        public LibMVCS.Logger logger { get; set; }

        public virtual void Alert(string _code, string _message, object _context)
        {
            throw new NotImplementedException();
        }


        public virtual void RefreshReadStyleSheet(IDTO _dto, object _context)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshWriteStyle(IDTO _dto, object _context)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshReadInstances(IDTO _dto, object _context)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshWriteInstances(IDTO _dto, object _context)
        {
            throw new NotImplementedException();
        }

    }
}
