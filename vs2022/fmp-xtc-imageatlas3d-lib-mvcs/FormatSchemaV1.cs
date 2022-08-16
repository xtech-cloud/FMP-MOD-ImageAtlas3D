namespace XTC.FMP.MOD.ImageAtlas3D.LIB.MVCS
{
    /// <summary>
    /// 文件格式的结构
    /// </summary>
    public class FormatSchemaV1
    {
        /// <summary>
        /// 音频
        /// </summary>
        public class Audio
        {
            /// <summary>
            /// 文件
            /// </summary>
            public string file { get; set; } = "";

            /// <summary>
            /// 音量
            /// </summary>
            public int volume { get; set; } = 100;
        }

        /// <summary>
        /// 全景图片
        /// </summary>
        public class Image3D
        {
            /// <summary>
            /// 文件
            /// </summary>
            public string file { get; set; } = "";

            /// <summary>
            /// Y轴旋转角度
            /// </summary>
            public float rotation_y { get; set; } = 0;
        }

        /// <summary>
        /// 自动旋转
        /// </summary>
        public class AutoRotate
        {
            /// <summary>
            /// Y轴旋转速度
            /// </summary>
            public float speed_y { get; set; } = 0;
        }

        public class Actions
        {
            /// <summary>
            /// 自动旋转
            /// </summary>
            public AutoRotate auto_rotate { get; set; } = new AutoRotate();
        }

        public class Block
        {
            /// <summary>
            /// 图片
            /// </summary>
            public Image3D image { get; set; } = new Image3D();
            /// <summary>
            /// 背景音乐
            /// </summary>
            public Audio bgm { get; set; } = new Audio();

            /// <summary>
            /// 语音
            /// </summary>
            public Audio voice { get; set; } = new Audio();
        }

        /// <summary>
        /// 文件格式的版本
        /// </summary>
        public string version { get; set; } = "";
        /// <summary>
        /// 背景音乐
        /// </summary>
        public Audio bgm { get; set; } = new Audio();

        /// <summary>
        /// 块列表
        /// </summary>
        public Block[] blocks{ get; set; } = new Block[0];
        /// <summary>
        /// 行为
        /// </summary>
        public Actions actions { get; set; } = new Actions();
    }
}


