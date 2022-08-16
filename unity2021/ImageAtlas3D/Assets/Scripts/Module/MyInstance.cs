

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.ImageAtlas3D.LIB.Proto;
using XTC.FMP.MOD.ImageAtlas3D.LIB.MVCS;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;

namespace XTC.FMP.MOD.ImageAtlas3D.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private Coroutine coroutineLoadAudio_;
        private Coroutine coroutineLoadTexture_;

        private Material originSkybox_;

        public MyInstance(string _uid, string _style, MyConfig _config, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 应用样式
        /// </summary>
        public void ApplyStyle()
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        public void HandleCreated()
        {
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        public void HandleOpened(string _source, string _uri)
        {
            var assetFullpath = combineAssetPath(_source, _uri);
            if (_source.StartsWith("file://"))
            {
                openAssetFromFile(assetFullpath);
            }
            rootUI.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
        }

        private void openAssetFromFile(string _assetFullpath)
        {
            logger_.Debug("ready to open {0}", _assetFullpath);
            //优先使用文件夹
            if (Directory.Exists(_assetFullpath + ".#"))
            {
                logger_.Debug("open {0}", _assetFullpath + ".#");
                openAssetFromFolder(_assetFullpath + ".#");
                return;
            }
            if (File.Exists(_assetFullpath))
            {
                logger_.Debug("open {0}", _assetFullpath);
                return;
            }
            logger_.Error("{0} not found", _assetFullpath);
        }

        private void openAssetFromFolder(string _dir)
        {
            string formatJson = Path.Combine(_dir, "format.json");
            FormatSchemaV1 schema;
            try
            {
                string json = File.ReadAllText(formatJson);
                schema = JsonConvert.DeserializeObject<FormatSchemaV1>(json);
            }
            catch (Exception ex)
            {
                logger_.Exception(ex);
                return;
            }


            string bgmPath = Path.Combine(_dir, schema.bgm.file);
            playAudio("bgm", bgmPath, schema.bgm.volume);

            foreach (var block in schema.blocks)
            {
                string imagePath = Path.Combine(_dir, block.image.file);
                renderImage(imagePath, style_.renderer);
                //TODO 
                break;
            }

        }

        private void playAudio(string _audioSource, string _file, int _volume)
        {
            if (null != coroutineLoadAudio_)
            {
                mono_.StopCoroutine(coroutineLoadAudio_);
                coroutineLoadAudio_ = null;
            }

            var audioSource = rootUI.transform.Find(_audioSource).GetComponent<AudioSource>();
            audioSource.volume = _volume / 100.0f;
            coroutineLoadAudio_ = mono_.StartCoroutine(loadAudioClip(_file, (_audioClip) =>
            {
                audioSource.clip = _audioClip;
                audioSource.Play();
            }));
        }

        private void renderImage(string _file, string _renderer)
        {
            if (null != coroutineLoadTexture_)
            {
                mono_.StopCoroutine(coroutineLoadTexture_);
                coroutineLoadTexture_ = null;
            }

            coroutineLoadTexture_ = mono_.StartCoroutine(loadTexture(_file, (_texture) =>
            {
                if (_renderer.Equals("skybox"))
                {
                    originSkybox_ = RenderSettings.skybox;
                    var material = rootAttachments.transform.Find("SkyboxRenderer").GetComponent<MeshRenderer>().material;
                    var materialClone = GameObject.Instantiate(material);
                    materialClone.mainTexture = _texture;
                    RenderSettings.skybox = materialClone;
                }
            }));

        }

        private IEnumerator loadAudioClip(string _file, Action<AudioClip> _onFinish)
        {
            using (var uwr = UnityWebRequestMultimedia.GetAudioClip(_file, AudioType.MPEG))
            {
                yield return uwr.SendWebRequest();
                if (uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    logger_.Error(uwr.error);
                    yield break;
                }
                AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                _onFinish(clip);
            }
        }

        private IEnumerator loadTexture(string _file, Action<Texture> _onFinish)
        {
            using (var uwr = new UnityWebRequest(_file))
            {
                DownloadHandlerTexture handler = new DownloadHandlerTexture(true);
                uwr.downloadHandler = handler;
                yield return uwr.SendWebRequest();
                if (uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    logger_.Error(uwr.error);
                    yield break;
                }
                Texture2D texture = handler.texture;
                _onFinish(texture);
            }
        }
    }
}
