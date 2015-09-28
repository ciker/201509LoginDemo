using Blogs.Common.CustomModel;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using PanGu;
using PanGu.HighLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Helper
{
    /// <summary>
    /// 盘古分词在lucene.net中的使用帮助类
    /// 调用PanGuLuceneHelper.instance
    /// 来源：http://blog.csdn.net/pukuimin1226/article/details/17558247 基础上进行了修改
    /// </summary>
    public class PanGuLuceneHelper
    {
        private PanGuLuceneHelper() { }

        #region 单一实例
        private static PanGuLuceneHelper _instance = null;
        /// <summary>
        /// 单一实例
        /// </summary>
        public static PanGuLuceneHelper instance
        {
            get
            {
                if (_instance == null) _instance = new PanGuLuceneHelper();
                return _instance;
            }
        }
        #endregion

        #region 00一些属性和参数
        #region Lucene.Net的目录-参数
        private Lucene.Net.Store.Directory _directory_luce = null;
        /// <summary>
        /// Lucene.Net的目录-参数
        /// </summary>
        public Lucene.Net.Store.Directory directory_luce
        {
            get
            {
                if (_directory_luce == null) _directory_luce = Lucene.Net.Store.FSDirectory.Open(directory);
                return _directory_luce;
            }
        }
        #endregion

        #region 索引在硬盘上的目录
        private System.IO.DirectoryInfo _directory = null;
        /// <summary>
        /// 索引在硬盘上的目录
        /// </summary>
        public System.IO.DirectoryInfo directory
        {
            get
            {
                if (_directory == null)
                {
                    string dirPath = AppDomain.CurrentDomain.BaseDirectory + "SearchIndex";
                    if (System.IO.Directory.Exists(dirPath) == false) _directory = System.IO.Directory.CreateDirectory(dirPath);
                    else _directory = new System.IO.DirectoryInfo(dirPath);
                }
                return _directory;
            }
        }
        #endregion

        #region 分析器
        private Analyzer _analyzer = null;
        /// <summary>
        /// 分析器
        /// </summary>
        public Analyzer analyzer
        {
            get
            {
                {
                    _analyzer = new Lucene.Net.Analysis.PanGu.PanGuAnalyzer();//                   
                }
                return _analyzer;
            }
        }
        #endregion

        #region 版本号枚举类
        private static Lucene.Net.Util.Version _version = Lucene.Net.Util.Version.LUCENE_30;
        /// <summary>
        /// 版本号枚举类
        /// </summary>
        public Lucene.Net.Util.Version version
        {
            get
            {
                return _version;
            }
        }
        #endregion
        #endregion

        #region 01创建索引
        /// <summary>
        /// 创建索引(先删 后更新)
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        public bool CreateIndex(List<SearchResult> datalist)
        {
            IndexWriter writer = null;
            try
            {
                writer = new IndexWriter(directory_luce, analyzer, false, IndexWriter.MaxFieldLength.LIMITED);//false表示追加（true表示删除之前的重新写入）
            }
            catch
            {
                writer = new IndexWriter(directory_luce, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);//false表示追加（true表示删除之前的重新写入）
            }
            foreach (SearchResult data in datalist)
            {
                writer.DeleteDocuments(new Term("id", data.id.ToString()));//新增前 删除  不然会有重复数据
                CreateIndex(writer, data);
            }
            writer.Optimize();
            writer.Dispose();
            return true;
        }
        public bool CreateIndex(SearchResult data)
        {
            List<SearchResult> datalist = new List<SearchResult>();
            datalist.Add(data);
            return CreateIndex(datalist);
        }

        public bool CreateIndex(IndexWriter writer, SearchResult data)
        {
            try
            {

                if (data == null) return false;
                Document doc = new Document();
                Type type = data.GetType();//assembly.GetType("Reflect_test.PurchaseOrderHeadManageModel", true, true); //命名空间名称 + 类名    

                //创建类的实例    
                //object obj = Activator.CreateInstance(type, true);  
                //获取公共属性    
                PropertyInfo[] Propertys = type.GetProperties();
                for (int i = 0; i < Propertys.Length; i++)
                {
                    //Propertys[i].SetValue(Propertys[i], i, null); //设置值
                    PropertyInfo pi = Propertys[i];
                    string name = pi.Name;
                    object objval = pi.GetValue(data, null);
                    string value = objval == null ? "" : objval.ToString(); //值                   
                    if (name == "id" || name == "flag")//id在写入索引时必是不分词，否则是模糊搜索和删除，会出现混乱
                    {
                        doc.Add(new Field(name, value, Field.Store.YES, Field.Index.NOT_ANALYZED));//id不分词
                    }
                    else
                    {
                        doc.Add(new Field(name, value, Field.Store.YES, Field.Index.ANALYZED));
                    }
                }
                writer.AddDocument(doc);
            }
            catch (System.IO.FileNotFoundException fnfe)
            {
                throw fnfe;
            }
            return true;
        }
        #endregion

        #region 02在title和content字段中查询数据
        /// <summary>
        /// 在title和content字段中查询数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SearchResult> Search(string keyword)
        {
            string[] fileds = { "title", "content" };//查询字段           
            QueryParser parser = null;
            parser = new MultiFieldQueryParser(version, fileds, analyzer);//多个字段查询
            Query query = parser.Parse(keyword);
            int n = 1000;
            IndexSearcher searcher = new IndexSearcher(directory_luce, true);//true-表示只读
            TopDocs docs = searcher.Search(query, (Filter)null, n);
            if (docs == null || docs.TotalHits == 0)
            {
                return null;
            }
            else
            {
                List<SearchResult> list = new List<SearchResult>();
                int counter = 1;
                foreach (ScoreDoc sd in docs.ScoreDocs)//遍历搜索到的结果
                {
                    try
                    {
                        Document doc = searcher.Doc(sd.Doc);
                        int id = int.Parse(doc.Get("id"));
                        string title = doc.Get("title");
                        string content = doc.Get("content");
                        string blogTag = doc.Get("blogTag");
                        string url = doc.Get("url");
                        int flag = int.Parse(doc.Get("flag"));
                        int clickQuantity = int.Parse(doc.Get("clickQuantity"));

                        string createdate = doc.Get("createdate");
                        PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
                        PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new PanGu.Segment());
                        highlighter.FragmentSize = 50;
                        content = highlighter.GetBestFragment(keyword, content);
                        string titlehighlight = highlighter.GetBestFragment(keyword, title);
                        if (titlehighlight != "") title = titlehighlight;

                        list.Add(new SearchResult(title, content, url, blogTag, id, clickQuantity, flag));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    counter++;
                }
                return list;
            }
            //st.Stop();
            //Response.Write("查询时间：" + st.ElapsedMilliseconds + " 毫秒<br/>");

        }
        #endregion

        #region 03在不同的分类下再根据title和content字段中查询数据(分页)
        /// <summary>
        /// 在不同的类型下再根据title和content字段中查询数据(分页)
        /// </summary>
        /// <param name="_flag">分类,传空值查询全部</param>
        /// <param name="keyword"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public List<SearchResult> Search(string _flag, string keyword, int PageIndex, int PageSize)
        {
            if (PageIndex < 1) PageIndex = 1;
            Stopwatch st = Stopwatch.StartNew();
            st.Start();
            BooleanQuery bq = new BooleanQuery();
            if (_flag != "")
            {
                QueryParser qpflag = new QueryParser(version, "flag", analyzer);
                Query qflag = qpflag.Parse(_flag);
                bq.Add(qflag, Occur.MUST);//与运算
            }
            if (keyword != "")
            {
                string[] fileds = { "blogTag", "title", "content" };//查询字段
                QueryParser parser = null;// new QueryParser(version, field, analyzer);//一个字段查询
                parser = new MultiFieldQueryParser(version, fileds, analyzer);//多个字段查询
                Query queryKeyword = parser.Parse(keyword);
                bq.Add(queryKeyword, Occur.MUST);//与运算
            }

            TopScoreDocCollector collector = TopScoreDocCollector.Create(PageIndex * PageSize, false);
            IndexSearcher searcher = new IndexSearcher(directory_luce, true);//true-表示只读
            searcher.Search(bq, collector);

            if (collector == null || collector.TotalHits == 0)
            {
                //TotalCount = 0;
                return null;
            }
            else
            {
                int start = PageSize * (PageIndex - 1);
                //结束数
                int limit = PageSize;
                ScoreDoc[] hits = collector.TopDocs(start, limit).ScoreDocs;
                List<SearchResult> list = new List<SearchResult>();
                int counter = 1;
                //TotalCount = collector.TotalHits;
                st.Stop();
                //st.ElapsedMilliseconds;//毫秒
                foreach (ScoreDoc sd in hits)//遍历搜索到的结果
                {
                    try
                    {
                        Document doc = searcher.Doc(sd.Doc);
                        int id = int.Parse(doc.Get("id"));
                        string title = doc.Get("title");
                        string content = doc.Get("content");
                        string blogTag = doc.Get("blogTag");
                        string url = doc.Get("url");
                        int flag = int.Parse(doc.Get("flag"));
                        int clickQuantity = int.Parse(doc.Get("clickQuantity"));
                        content = Highlight(keyword, content);
                        //string titlehighlight = Highlight(keyword, title);
                        //if (titlehighlight != "") title = titlehighlight;
                        list.Add(new SearchResult(title, content, url, blogTag, id, clickQuantity, flag));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    counter++;
                }
                return list;
            }
        }
        #endregion

        #region 把content按照keywords进行高亮
        /// <summary>
        /// 把content按照keywords进行高亮
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string Highlight(string keywords, string content)
        {
            SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<strong>", "</strong>");
            Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 200;
            return highlighter.GetBestFragment(keywords, content);
        }
        #endregion

        #region 04删除索引
        #region 删除索引数据（根据id）
        /// <summary>
        /// 删除索引数据（根据id）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            bool IsSuccess = false;
            Term term = new Term("id", id);
            IndexWriter writer = new IndexWriter(directory_luce, analyzer, false, IndexWriter.MaxFieldLength.LIMITED);
            writer.DeleteDocuments(term); // writer.DeleteDocuments(term)或者writer.DeleteDocuments(query);            
            writer.Commit();
            IsSuccess = writer.HasDeletions();
            writer.Dispose();
            return IsSuccess;
        }

        public bool Delete(SearchResult ids)
        {
            return Delete(ids.id.ToString());
        }

        #endregion

        #region 删除全部索引数据
        /// <summary>
        /// 删除全部索引数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            bool IsSuccess = true;
            try
            {
                IndexWriter writer = new IndexWriter(directory_luce, analyzer, false, IndexWriter.MaxFieldLength.LIMITED);
                writer.DeleteAll();
                writer.Commit();
                IsSuccess = writer.HasDeletions();
                writer.Dispose();
            }
            catch
            {
                IsSuccess = false;
            }
            return IsSuccess;
        }
        #endregion
        #endregion

        #region 分词测试
        /// <summary>
        /// 分词测试
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string Token(string keyword)
        {
            string ret = "";
            System.IO.StringReader reader = new System.IO.StringReader(keyword);
            Lucene.Net.Analysis.TokenStream ts = analyzer.TokenStream(keyword, reader);
            bool hasNext = ts.IncrementToken();
            Lucene.Net.Analysis.Tokenattributes.ITermAttribute ita;
            while (hasNext)
            {
                ita = ts.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();
                ret += ita.Term + "|";
                hasNext = ts.IncrementToken();
            }
            ts.CloneAttributes();
            reader.Close();
            analyzer.Close();
            return ret;
        }
        #endregion

    }
}
