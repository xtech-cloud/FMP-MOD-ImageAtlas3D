
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.58.0.  DO NOT EDIT!
//*************************************************************************************

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    public class MySubjectBase
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["style"] = "default";
        /// model.Publish(/XTC/ImageAtlas3D/Create, data);
        /// </example>
        public const string Create = "/XTC/ImageAtlas3D/Create";

        /// <summary>
        /// 打开
        /// </summary>
        /// <remarks>
        /// 先加载资源，然后显示
        /// </remarks>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["source"] = "file";
        /// data["uri"] = "";
        /// data["delay"] = 0f;
        /// model.Publish(/XTC/ImageAtlas3D/Open, data);
        /// </example>
        public const string Open = "/XTC/ImageAtlas3D/Open";

        /// <summary>
        /// 显示
        /// </summary>
        /// <remarks>
        /// 仅显示，不执行其他任何操作
        /// </remarks>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["delay"] = 0f;
        /// model.Publish(/XTC/ImageAtlas3D/Show, data);
        /// </example>
        public const string Show = "/XTC/ImageAtlas3D/Show";

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <remarks>
        /// 仅隐藏，不执行其他任何操作
        /// </remarks>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["delay"] = 0f;
        /// model.Publish(/XTC/ImageAtlas3D/Hide, data);
        /// </example>
        public const string Hide = "/XTC/ImageAtlas3D/Hide";

        /// <summary>
        /// 关闭
        /// </summary>
        /// <remarks>
        /// 先隐藏，然后释放资源
        /// </remarks>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// data["delay"] = 0f;
        /// model.Publish(/XTC/ImageAtlas3D/Close, data);
        /// </example>
        public const string Close = "/XTC/ImageAtlas3D/Close";

        /// <summary>
        /// 销毁
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// model.Publish(/XTC/ImageAtlas3D/Close, data);
        /// </example>
        public const string Delete = "/XTC/ImageAtlas3D/Delete";
    }
}
