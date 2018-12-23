using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using AutoyaFramework.Settings.Connection;
using UnityEngine.Networking;


//This script is based on Autoya HTTP.cs

/**
	implementation of HTTP connection with timeout.
*/
namespace AutoyaFramework.Connections.HTTP
{
    public class BackyardSettings
    {
        /*
			maintenance settings.
		*/
        public const int MAINTENANCE_CODE = 599;

        /*
			http settings.
		*/
        public const double HTTP_TIMEOUT_SEC = 5.0;
        public const int HTTP_TIMEOUT_CODE = 408;

        public const string HTTP_CODE_ERROR_SUFFIX = "httpResponseCodeError:";
        public const string HTTP_TIMEOUT_MESSAGE = "timeout. sec:";
    }

    public class ConnectionSettings
    {
        public const bool useChunkedTransfer = false;
    }
    
    
    public class HTTPConnection
    {

        // response by string
        public IEnumerator Get(string connectionId, Dictionary<string, string> requestHeader, string url, Action<string, int, Dictionary<string, string>, string> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Get(url))
            {
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = Encoding.UTF8.GetString(request.downloadHandler.data);
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator Post(string connectionId, Dictionary<string, string> requestHeader, string url, string data, Action<string, int, Dictionary<string, string>, string> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Post(url, data))
            {
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;
                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = Encoding.UTF8.GetString(request.downloadHandler.data);
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator Put(string connectionId, Dictionary<string, string> requestHeader, string url, string data, Action<string, int, Dictionary<string, string>, string> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Put(url, data))
            {
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = Encoding.UTF8.GetString(request.downloadHandler.data);
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator Delete(string connectionId, Dictionary<string, string> requestHeader, string url, Action<string, int, Dictionary<string, string>, string> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Delete(url))
            {
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = Encoding.UTF8.GetString(request.downloadHandler.data);
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        // response by byte[]
        public IEnumerator GetByBytes(string connectionId, Dictionary<string, string> requestHeader, string url, Action<string, int, Dictionary<string, string>, byte[]> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Get(url))
            {
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = request.downloadHandler.data;
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator PostByBytes(string connectionId, Dictionary<string, string> requestHeader, string url, string data, Action<string, int, Dictionary<string, string>, byte[]> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Post(url, data))
            {
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;
                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = request.downloadHandler.data;
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator PutByBytes(string connectionId, Dictionary<string, string> requestHeader, string url, string data, Action<string, int, Dictionary<string, string>, byte[]> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Put(url, data))
            {
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = request.downloadHandler.data;
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator DeleteByBytes(string connectionId, Dictionary<string, string> requestHeader, string url, Action<string, int, Dictionary<string, string>, byte[]> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Delete(url))
            {
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = request.downloadHandler.data;
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        // request & response by byte[]
        public IEnumerator Post(string connectionId, Dictionary<string, string> requestHeader, string url, byte[] data, Action<string, int, Dictionary<string, string>, byte[]> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Post(url, string.Empty))
            {
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;
                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = Encoding.UTF8.GetString(request.downloadHandler.data);
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, request.downloadHandler.data);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }

        public IEnumerator Put(string connectionId, Dictionary<string, string> requestHeader, string url, byte[] data, Action<string, int, Dictionary<string, string>, string> succeeded, Action<string, int, string, Dictionary<string, string>> failed, double timeoutSec = 0)
        {
            var currentDate = DateTime.UtcNow;
            var limitTick = (TimeSpan.FromTicks(currentDate.Ticks) + TimeSpan.FromSeconds(timeoutSec)).Ticks;

            using (var request = UnityWebRequest.Put(url, data))
            {
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
                if (requestHeader != null)
                {
                    foreach (var kv in requestHeader)
                    {
                        request.SetRequestHeader(kv.Key, kv.Value);
                    }
                }
                request.chunkedTransfer = ConnectionSettings.useChunkedTransfer;

                var p = request.SendWebRequest();

                while (!p.isDone)
                {
                    yield return null;

                    // check timeout.
                    if (0 < timeoutSec && limitTick < DateTime.UtcNow.Ticks)
                    {
                        request.Abort();
                        failed(connectionId, BackyardSettings.HTTP_TIMEOUT_CODE, BackyardSettings.HTTP_TIMEOUT_MESSAGE + timeoutSec, null);
                        yield break;
                    }
                }

                var responseCode = (int)request.responseCode;
                var responseHeaders = request.GetResponseHeaders();

                if (request.isNetworkError)
                {
                    failed(connectionId, responseCode, request.error, responseHeaders);
                    yield break;
                }

                var result = Encoding.UTF8.GetString(request.downloadHandler.data);
                if (200 <= responseCode && responseCode <= 299)
                {
                    succeeded(connectionId, responseCode, responseHeaders, result);
                }
                else
                {
                    failed(connectionId, responseCode, BackyardSettings.HTTP_CODE_ERROR_SUFFIX + result, responseHeaders);
                }
            }
        }
    }
}