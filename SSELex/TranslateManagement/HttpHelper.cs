using System.IO.Compression;
using System.IO;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace SSELex.TranslateManage
{
    public class HttpHelper
    {
        #region Predefined Variables 

        private Encoding Encoding = Encoding.Default;
       
        private Encoding Postencoding = Encoding.Default;
        
        private HttpWebRequest Request = null;
     
        private HttpWebResponse Response = null;
          
        private IPEndPoint _IPEndPoint = null;

        #endregion

        #region Public  

        /// <summary>  
        /// Get the corresponding page data according to the data passed in 
        /// </summary>  
        /// <param name="Item">Parameter class object</param>  
        /// <returns>Return HttpResult type</returns>  
        public HttpResult GetHtml(HttpItem item)
        {  
            HttpResult result = new HttpResult();
            try
            {
                SetRequest(item);
            }
            catch (Exception ex)
            {
                return new HttpResult() { Cookie = string.Empty, Header = null, Html = ex.Message, StatusDescription = "Error in configuring parameters：" + ex.Message };
            }
            try
            {  
                using (Response = (HttpWebResponse)Request.GetResponse())
                {
                    GetData(item, result);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (Response = (HttpWebResponse)ex.Response)
                    {
                        GetData(item, result);
                    }
                }
                else
                {
                    result.Html = ex.Message;
                }
            }
            catch (Exception ex)
            {
                result.Html = ex.Message;
            }
            if (item.IsToLower) result.Html = result.Html.ToLower();
            return result;
        }
        #endregion

        #region GetData  

        /// <summary>  
        /// Methods for obtaining and parsing data 
        /// </summary>  
        /// <param name="Item"></param>  
        /// <param name="Result"></param>  
        private void GetData(HttpItem Item, HttpResult Result)
        {
            if (Response == null)
            {
                return;
            }
            #region Base    
            Result.StatusCode = Response.StatusCode; 
            Result.StatusDescription = Response.StatusDescription;
            Result.Header = Response.Headers;
            Result.ResponseUri = Response.ResponseUri.ToString();
            if (Response.Cookies != null) Result.CookieCollection = Response.Cookies;
            if (Response.Headers["set-cookie"] != null) Result.Cookie = Response.Headers["set-cookie"];
            #endregion

            #region Byte  
            byte[] ResponseByte = GetByte();
            #endregion

            #region Html  
            if (ResponseByte != null && ResponseByte.Length > 0)
            {
                SetEncoding(Item, Result, ResponseByte);
                Result.Html = Encoding.GetString(ResponseByte);
            }
            else
            { 
                Result.Html = string.Empty;
            }
            #endregion
        }
        /// <summary>  
        /// Setting the encoding
        /// </summary>  
        /// <param name="Item">HttpItem</param>  
        /// <param name="Result">HttpResult</param>  
        /// <param name="ResponseByte">byte[]</param>  
        private void SetEncoding(HttpItem Item, HttpResult Result, byte[] ResponseByte)
        {
            if (Item.ResultType == ResultType.Byte) Result.ResultByte = ResponseByte;
  
            if (Encoding == null)
            {
                Match meta = Regex.Match(Encoding.Default.GetString(ResponseByte), "<meta[^<]*charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
                string c = string.Empty;
                if (meta != null && meta.Groups.Count > 0)
                {
                    c = meta.Groups[1].Value.ToLower().Trim();
                }
                if (c.Length > 2)
                {
                    try
                    {
                        Encoding = Encoding.GetEncoding(c.Replace("\"", string.Empty).Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim());
                    }
                    catch
                    {
                        if (string.IsNullOrEmpty(Response.CharacterSet))
                        {
                            Encoding = Encoding.UTF8;
                        }
                        else
                        {
                            Encoding = Encoding.GetEncoding(Response.CharacterSet);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Response.CharacterSet))
                    {
                        Encoding = Encoding.UTF8;
                    }
                    else
                    {
                        Encoding = Encoding.GetEncoding(Response.CharacterSet);
                    }
                }
            }
        }
 
        private byte[] GetByte()
        {
            byte[] ResponseByte = null;
            using (MemoryStream _stream = new MemoryStream())
            {
                //GZIIP  
                if (Response.ContentEncoding != null && Response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    //Start reading the stream and set the encoding 
                    new GZipStream(Response.GetResponseStream(), CompressionMode.Decompress).CopyTo(_stream, 10240);
                }
                else
                {
                    //Start reading the stream and set the encoding  
                    Response.GetResponseStream().CopyTo(_stream, 10240);
                }
                //Get Byte  
                ResponseByte = _stream.ToArray();
            }
            return ResponseByte;
        }


        #endregion

        #region SetRequest  

        /// <summary>  
        /// Prepare parameters for the request 
        /// </summary>  
        ///<param name="Item"></param>  
        private void SetRequest(HttpItem Item)
        {
            SetCer(Item);
            if (Item.IPEndPoint != null)
            {
                _IPEndPoint = Item.IPEndPoint;
                Request.ServicePoint.BindIPEndPointDelegate = new BindIPEndPoint(BindIPEndPointCallback);
            }
            if (Item.Header != null && Item.Header.Count > 0) foreach (string key in Item.Header.AllKeys)
                {
                    Request.Headers.Add(key, Item.Header[key]);
                }
            SetProxy(Item);
            if (Item.ProtocolVersion != null) Request.ProtocolVersion = Item.ProtocolVersion;
            Request.ServicePoint.Expect100Continue = Item.Expect100Continue;  
            Request.Method = Item.Method;
            Request.Timeout = Item.Timeout;
            Request.KeepAlive = Item.KeepAlive;
            Request.ReadWriteTimeout = Item.ReadWriteTimeout;
            if (!string.IsNullOrWhiteSpace(Item.Host))
            {
                Request.Host = Item.Host;
            }
            if (Item.IfModifiedSince != null) Request.IfModifiedSince = Convert.ToDateTime(Item.IfModifiedSince); 
            Request.Accept = Item.Accept; 
            Request.ContentType = Item.ContentType; 
            Request.UserAgent = Item.UserAgent;
            Encoding = Item.Encoding;
            Request.Credentials = Item.ICredentials;
            SetCookie(Item);
            Request.Referer = Item.Referer;
            Request.AllowAutoRedirect = Item.Allowautoredirect;
            if (Item.MaximumAutomaticRedirections > 0)
            {
                Request.MaximumAutomaticRedirections = Item.MaximumAutomaticRedirections;
            }  
            SetPostData(Item);  
            if (Item.Connectionlimit > 0) Request.ServicePoint.ConnectionLimit = Item.Connectionlimit;
        }
        /// <summary>  
        /// Setting up certificates  
        /// </summary>  
        /// <param name="Item"></param>  
        private void SetCer(HttpItem Item)
        {
            if (!string.IsNullOrWhiteSpace(Item.CerPath))
            {
                //This sentence must be written before creating a connection. Use the callback method to verify the certificate.  
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                Request = (HttpWebRequest)WebRequest.Create(Item.URL);
                SetCerList(Item); 
                Request.ClientCertificates.Add(new X509Certificate(Item.CerPath));
            }
            else
            {
                Request = (HttpWebRequest)WebRequest.Create(Item.URL);
                SetCerList(Item);
            }
        }
        /// <summary>  
        /// Setting up multiple certificates 
        /// </summary>  
        /// <param name="Item"></param>  
        private void SetCerList(HttpItem Item)
        {
            if (Item.ClentCertificates != null && Item.ClentCertificates.Count > 0)
            {
                foreach (X509Certificate Cert in Item.ClentCertificates)
                {
                    Request.ClientCertificates.Add(Cert);
                }
            }
        }

        private void SetCookie(HttpItem Item)
        {
            if (!string.IsNullOrEmpty(Item.Cookie)) Request.Headers.Add("cookie", Item.Cookie);
            if (Item.ResultCookieType == ResultCookieType.CookieCollection)
            {
                Request.CookieContainer = new CookieContainer();
                if (Item.CookieCollection != null && Item.CookieCollection.Count > 0)
                    Request.CookieContainer.Add(Item.CookieCollection);
            }
        }
        /// <summary>  
        /// Setting Post Data  
        /// </summary>  
        /// <param name="Item"></param>  
        private void SetPostData(HttpItem Item)
        {  
            if (!Request.Method.Trim().ToLower().Contains("get"))
            {
                if (Item.PostEncoding != null)
                {
                    Postencoding = Item.PostEncoding;
                }
                byte[] Buffer = null;
                if (Item.PostDataType == PostDataType.Byte && Item.PostdataByte != null && Item.PostdataByte.Length > 0)
                { 
                    Buffer = Item.PostdataByte;
                } 
                else if (Item.PostDataType == PostDataType.FilePath && !string.IsNullOrWhiteSpace(Item.Postdata))
                {
                    StreamReader Reader = new StreamReader(Item.Postdata, Postencoding);
                    Buffer = Postencoding.GetBytes(Reader.ReadToEnd());
                    Reader.Close();
                }  
                else if (!string.IsNullOrWhiteSpace(Item.Postdata))
                {
                    Buffer = Postencoding.GetBytes(Item.Postdata);
                }
                if (Buffer != null)
                {
                    Request.ContentLength = Buffer.Length;
                    Request.GetRequestStream().Write(Buffer, 0, Buffer.Length);
                }
            }
        }
        /// <summary>  
        /// Setting up the proxy
        /// </summary>  
        /// <param name="Item"></param>  
        private void SetProxy(HttpItem Item)
        {
            bool IsIeProxy = false;
            if (!string.IsNullOrWhiteSpace(Item.ProxyIp))
            {
                IsIeProxy = Item.ProxyIp.ToLower().Contains("ieproxy");
            }
            if (!string.IsNullOrWhiteSpace(Item.ProxyIp) && !IsIeProxy)
            { 
                if (Item.ProxyIp.Contains(":"))
                {
                    string[] ProxyList = Item.ProxyIp.Split(':');
                    WebProxy MyProxy = new WebProxy(ProxyList[0].Trim(), Convert.ToInt32(ProxyList[1].Trim()));
                    MyProxy.Credentials = new NetworkCredential(Item.ProxyUserName, Item.ProxyPwd);
                    Request.Proxy = MyProxy;
                }
                else
                {
                    WebProxy MyProxy = new WebProxy(Item.ProxyIp, false);
                    MyProxy.Credentials = new NetworkCredential(Item.ProxyUserName, Item.ProxyPwd);  
                    Request.Proxy = MyProxy;
                }
            }
            else if (IsIeProxy)
            {
                 
            }
            else
            {
                Request.Proxy = Item.WebProxy;
            }
        }


        #endregion

        #region Private main  
        /// <summary>  
        /// Callback verification certificate problem  
        /// </summary>  
        /// <param name="Sender"></param>  
        /// <param name="Certificate"></param>  
        /// <param name="Chain"></param>  
        /// <param name="Errors"></param>  
        /// <returns>bool</returns>  
        private bool CheckValidationResult(object Sender, X509Certificate Certificate, X509Chain Chain, SslPolicyErrors Errors) { return true; }

        /// <summary>  
        /// By setting this property, you can bind the IP address used by the client to make the connection when making a connection.   
        /// </summary>  
        /// <param name="ServicePoint"></param>  
        /// <param name="RemoteEndPoint"></param>  
        /// <param name="RetryCount"></param>  
        /// <returns></returns>  
        private IPEndPoint BindIPEndPointCallback(ServicePoint ServicePoint, IPEndPoint RemoteEndPoint, int RetryCount)
        {
            return _IPEndPoint;  
        }
        #endregion
    }

    #region Public calss  

    public class HttpItem
    {
        /// <summary>  
        /// Request URL is required
        /// </summary>  
        public string URL { get; set; }
        string _Method = "GET";
        /// <summary>  
        /// The default request method is GET. When it is POST, you must set the value of Postdata. 
        /// </summary>  
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        int _Timeout = 100000;
        /// <summary>  
        /// Default request timeout  
        /// </summary>  
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }
        int _ReadWriteTimeout = 30000;
        /// <summary>  
        /// Default timeout for writing Post data  
        /// </summary>  
        public int ReadWriteTimeout
        {
            get { return _ReadWriteTimeout; }
            set { _ReadWriteTimeout = value; }
        }
        /// <summary>  
        /// Set the Host header information  
        /// </summary>  
        public string Host { get; set; }
        Boolean _KeepAlive = true;
        /// <summary>  
        /// Gets or sets a value indicating whether to establish a persistent connection with the Internet resource. The default is true.  
        /// </summary>  
        public Boolean KeepAlive
        {
            get { return _KeepAlive; }
            set { _KeepAlive = value; }
        }
        string _Accept = "text/html, application/xhtml+xml, */*";
        /// <summary>  
        /// Request header value defaults to text/html, application/xhtml+xml, */*  
        /// </summary>  
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }
        string _ContentType = "text/html";
        /// <summary>  
        /// The default request return type is text/html 
        /// </summary>  
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }
        string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        /// <summary>  
        /// Client access information default Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)  
        /// </summary>  
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        /// <summary>  
        /// The default encoding of the returned data is NUll, which can be automatically identified, usually utf-8, gbk, gb2312 
        /// </summary>  
        public Encoding Encoding { get; set; }
        private PostDataType _PostDataType = PostDataType.String;
  
        public PostDataType PostDataType
        {
            get { return _PostDataType; }
            set { _PostDataType = value; }
        }
        /// <summary>  
        /// Post data string to be sent when posting a request  
        /// </summary>  
        public string Postdata { get; set; }
        /// <summary>  
        /// Post data of Bytes type to be sent during Post request  
        /// </summary>  
        public byte[] PostdataByte { get; set; }
  
        public CookieCollection CookieCollection { get; set; }
 
        public string Cookie { get; set; }

        public string Referer { get; set; }

        public string CerPath { get; set; }

        public WebProxy WebProxy { get; set; }
        private Boolean isToLower = false;

        public Boolean IsToLower
        {
            get { return isToLower; }
            set { isToLower = value; }
        }
        private Boolean allowautoredirect = false;

        public Boolean Allowautoredirect
        {
            get { return allowautoredirect; }
            set { allowautoredirect = value; }
        }
        private int connectionlimit = 1024;
        /// <summary>  
        /// Maximum number of connections  
        /// </summary>  
        public int Connectionlimit
        {
            get { return connectionlimit; }
            set { connectionlimit = value; }
        }

        public string ProxyUserName { get; set; }

        public string ProxyPwd { get; set; }
 
        public string ProxyIp { get; set; }
        private ResultType resulttype = ResultType.String;

        public ResultType ResultType
        {
            get { return resulttype; }
            set { resulttype = value; }
        }
        private WebHeaderCollection header = new WebHeaderCollection();

        public WebHeaderCollection Header
        {
            get { return header; }
            set { header = value; }
        }
        /// <summary>  
        //  Gets or sets the HTTP version used for the request. Returns: The HTTP version used for the request. The default is System.Net.HttpVersion.Version11.
        /// </summary>  
        public Version ProtocolVersion { get; set; }
        private Boolean _expect100continue = false;
 
        public Boolean Expect100Continue
        {
            get { return _expect100continue; }
            set { _expect100continue = value; }
        }

        public X509CertificateCollection ClentCertificates { get; set; }

        public Encoding PostEncoding { get; set; }
        private ResultCookieType _ResultCookieType = ResultCookieType.String;

        public ResultCookieType ResultCookieType
        {
            get { return _ResultCookieType; }
            set { _ResultCookieType = value; }
        }
        private ICredentials _ICredentials = CredentialCache.DefaultCredentials;

        public ICredentials ICredentials
        {
            get { return _ICredentials; }
            set { _ICredentials = value; }
        }

        public int MaximumAutomaticRedirections { get; set; }
        private DateTime? _IfModifiedSince = null;

        public DateTime? IfModifiedSince
        {
            get { return _IfModifiedSince; }
            set { _IfModifiedSince = value; }
        }
        #region ip-port  
        private IPEndPoint _IPEndPoint = null;

        public IPEndPoint IPEndPoint
        {
            get { return _IPEndPoint; }
            set { _IPEndPoint = value; }
        }
        #endregion
    }
 
    public class HttpResult
    {
        /// <summary>  
        /// Cookies returned by HTTP requests  
        /// </summary>  
        public string Cookie { get; set; }
        /// <summary>  
        /// Cookie object collection  
        /// </summary>  
        public CookieCollection CookieCollection { get; set; }
        private string _html = string.Empty;
        /// <summary>  
        /// Returned String type data Data is returned only when ResultType.String is used, otherwise it is empty  
        /// </summary>  
        public string Html
        {
            get { return _html; }
            set { _html = value; }
        }
        /// <summary>  
        /// The returned Byte array returns data only when ResultType.Byte is used, otherwise it is empty.  
        /// </summary>  
        public byte[] ResultByte { get; set; }

        public WebHeaderCollection Header { get; set; }

        public string StatusDescription { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string ResponseUri { get; set; }
        /// <summary>  
        /// Get the redirect URL  
        /// </summary>  
        public string RedirectUrl
        {
            get
            {
                try
                {
                    if (Header != null && Header.Count > 0)
                    {
                        if (Header.AllKeys.Any(k => k.ToLower().Contains("location")))
                        {
                            string baseurl = Header["location"].ToString().Trim();
                            string locationurl = baseurl.ToLower();
                            if (!string.IsNullOrWhiteSpace(locationurl))
                            {
                                bool b = locationurl.StartsWith("http://") || locationurl.StartsWith("https://");
                                if (!b)
                                {
                                    baseurl = new Uri(new Uri(ResponseUri), baseurl).AbsoluteUri;
                                }
                            }
                            return baseurl;
                        }
                    }
                }
                catch { }
                return string.Empty;
            }
        }
    }

    public enum ResultType
    {
        String,
        Byte
    }
    public enum PostDataType
    { 
        String,
        Byte,
        FilePath
    } 
    public enum ResultCookieType
    {
        String,
        CookieCollection
    }

    #endregion
}
