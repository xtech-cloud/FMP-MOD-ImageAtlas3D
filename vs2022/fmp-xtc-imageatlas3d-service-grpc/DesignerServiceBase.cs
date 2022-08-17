
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.18.0.  DO NOT EDIT!
//*************************************************************************************

using Grpc.Core;
using System.Threading.Tasks;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Proto;

namespace XTC.FMP.MOD.ImageAtlas3D.App.Service
{
    /// <summary>
    /// Designer基类
    /// </summary>
    public class DesignerServiceBase : LIB.Proto.Designer.DesignerBase
    {
    

        public override async Task<DesignerReadStylesResponse> ReadStyleSheet(ScopeRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new DesignerReadStylesResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        public override async Task<BlankResponse> WriteStyle(DesignerWriteStylesRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BlankResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        public override async Task<DesignerReadInstancesResponse> ReadInstances(ScopeRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new DesignerReadInstancesResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        public override async Task<BlankResponse> WriteInstances(DesignerWriteInstancesRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BlankResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

    }
}

