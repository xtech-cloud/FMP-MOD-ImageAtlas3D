
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.56.0.  DO NOT EDIT!
//*************************************************************************************

using System.Collections.Generic;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    /// <summary>
    /// 序列类基类
    /// </summary>
    public class SequenceBase
    {
        public System.Action OnFinish;
    } //class

    /// <summary>
    /// 计数器序列
    /// </summary>
    public class CounterSequence : SequenceBase
    {
        protected int total_ { get; set; }
        protected int ticker_ { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_total">初始化总数</param>
        public CounterSequence(int _total)
        {
            total_ = _total;
            ticker_ = 0;
        }

        /// <summary>
        /// 总数加一
        /// </summary>
        public void Dial()
        {
            total_ += 1;
        }

        /// <summary>
        /// 计数器加一
        /// </summary>
        public void Tick()
        {
            if (ticker_ >= total_)
                return;

            ticker_ += 1;
            if (ticker_ >= total_)
            {
                if (null != OnFinish)
                {
                    OnFinish();
                }
            }
        }

    } //class
} //namespace

