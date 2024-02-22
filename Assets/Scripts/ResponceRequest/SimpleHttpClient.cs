using System.Text;
using System.Collections.Generic;
using ResponceRequest;
using UnityEngine;
using UnityEngine.Networking;

namespace HttpRequest
{
    public class SimpleHttpClient
    {
        public static IHttpRequest Get(string uri)
        {
            //Debug.Log("uri " + uri);
            return new HttpRequestImpl(UnityWebRequest.Get(uri));
        }

        public static IHttpRequest PostJson(string uri, string json)
        {
            return new HttpRequestImpl(uri,
                Encoding.UTF8.GetBytes(json),
                "application/json");
        }

        public static IHttpRequest Put(string uri, string bodyData)
        {
            Debug.Log("Put uri " + uri + " bodyData " + bodyData);
            return new HttpRequestImpl(UnityWebRequest.Put(uri, bodyData));
        }

        public static IHttpRequest Delete(string uri)
        {
            return new HttpRequestImpl(UnityWebRequest.Delete(uri));
        }
    }
}