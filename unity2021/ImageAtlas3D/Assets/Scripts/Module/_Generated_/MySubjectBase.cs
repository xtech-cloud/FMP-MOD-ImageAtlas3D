
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.30.1.  DO NOT EDIT!
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
        /// 关闭
        /// </summary>
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
