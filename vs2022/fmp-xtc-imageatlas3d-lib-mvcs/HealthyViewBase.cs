
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.29.2.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Bridge;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.MVCS
{
    /// <summary>
    /// Healthy视图层基类
    /// </summary>
    public class HealthyViewBase : View
    {
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public HealthyViewBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }


        /// <summary>
        /// 刷新Echo的数据
        /// </summary>
        /// <param name="_err">错误</param>
        /// <param name="_dto">HealthyEchoResponse的数据传输对象</param>
        public virtual void RefreshProtoEcho(Error _err, HealthyEchoResponseDTO _dto, SynchronizationContext? _context)
        {
            var bridge = getFacade()?.getUiBridge() as IHealthyUiBridge; 
            if (!Error.IsOK(_err))
            {
                bridge?.Alert(string.Format("errcode_Echo_{0}", _err.getCode()), _err.getMessage(), _context);
                return;
            }
            bridge?.RefreshEcho(_dto, _context);
        }


        /// <summary>
        /// 获取直系数据层
        /// </summary>
        /// <returns>数据层</returns>
        protected HealthyModel? getModel()
        {
            if(null == model_)
                model_ = findModel(HealthyModel.NAME + "." + gid_) as HealthyModel;
            return model_;
        }

        /// <summary>
        /// 获取直系服务层
        /// </summary>
        /// <returns>服务层</returns>
        protected HealthyService? getService()
        {
            if(null == service_)
                service_ = findService(HealthyService.NAME + "." + gid_) as HealthyService;
            return service_;
        }

        /// <summary>
        /// 获取直系UI装饰层
        /// </summary>
        /// <returns>UI装饰层</returns>
        protected HealthyFacade? getFacade()
        {
            if(null == facade_)
                facade_ = findFacade(HealthyFacade.NAME + "." + gid_) as HealthyFacade;
            return facade_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// 直系数据层
        /// </summary>
        private HealthyModel? model_;

        /// <summary>
        /// 直系服务层
        /// </summary>
        private HealthyService? service_;

        /// <summary>
        /// 直系UI装饰层
        /// </summary>
        private HealthyFacade? facade_;
    }
}

