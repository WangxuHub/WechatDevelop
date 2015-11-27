using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DBObjectQuery
{
	internal class SyntaxAnalyzer
    {
        /// <summary>
        /// 由对象查询语句解析为Sql语句
        /// </summary>
        /// <param name="oql"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public static string ParseSql(string oql,IMap map)
        {
			if(oql==string.Empty)
			    return string.Empty;
            SyntaxNode syntaxNode=new SyntaxNode();
            syntaxNode.Sentence = oql;
            if (syntaxNode.Map == null)
                syntaxNode.Map = map;
            string sql = syntaxNode.GetSql();
            return sql;
        }
    }
    class SyntaxNode
    {
        private static List<string> keywords=new List<string>();
        private static List<string> funKeywords = new List<string>();
        /// <summary>
        /// 关键字列表
        /// </summary>
        static SyntaxNode()
        {
            keywords.Add("in");
            keywords.Add("and");
            keywords.Add("or");
            keywords.Add("like");
            keywords.Add("not");
            keywords.Add("between");
            keywords.Add("to");
            keywords.Add("as");
            keywords.Add("is");
			keywords.Add("null");
            keywords.Add("tinyint");
            keywords.Add("smallint");
            keywords.Add("int");
            keywords.Add("bigint");
            keywords.Add("decimal");
            keywords.Add("numeric");
            keywords.Add("smallmoney");
            keywords.Add("money");
            keywords.Add("float");
            keywords.Add("real");
            keywords.Add("char");
            keywords.Add("text");
            keywords.Add("varchar");
            keywords.Add("nchar");
            keywords.Add("ntext");
            keywords.Add("nvarchar");
            keywords.Add("desc");
            keywords.Add("asc");
            funKeywords.Add("dateadd");
            funKeywords.Add("datediff");
            funKeywords.Add("datename");
            funKeywords.Add("datepart");
        }
        private IMap map;
        /// <summary>
        /// 属性和字段间的映射
        /// </summary>
        public IMap Map
        {
            get { return map; }
            set { map = value; }
        }
        private string sentence;
        public string Sentence
        {
            get { return sentence; }
            set
            {
                sentence = value;
                if (this.sentenceType == SentenceType.Segment || this.sentenceType == SentenceType.InBracket)
                    Parse();
            }
        }
        private SentenceType sentenceType=SentenceType.Segment;
        /// <summary>
        /// 语句类型
        /// </summary>
        public SentenceType SentenceType
        {
            get { return sentenceType; }
            set 
            { 
                sentenceType = value;
            }
        }
        private SyntaxNode parentNode;
        /// <summary>
        /// 父节点
        /// </summary>
        public SyntaxNode ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        private List<SyntaxNode> nodes=new List<SyntaxNode>();
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<SyntaxNode> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
        /// <summary>
        /// 在碰到“(”后，取出与之对应的“)”之间的内容
        /// </summary>
        /// <param name="charArray">字符数组</param>
        /// <param name="index">当前字符位置</param>
        private void ParseInBracket(ref Char[] charArray, ref int index)
        {
            int bracketDepth=1;//括号深度，“（”加1，“）”减1
            bool inQuotation = false;//是否在引号内
            string inBracketString=string.Empty;
            index++;
            //遍历当前字符串到倒数第二个字符
            for (; index < charArray.Length - 1; index++)
            {
                char c = charArray[index];
                if (inQuotation)                         //如果在引号内，后面有两个连续的“'”则当成普通字符，一个“'”则结束字符串
                {
                    if (c == '\'')                       //出现一个引号后再出现了另一个引号
                    {
                        if (charArray[index + 1] == '\'')//如果“'”后面的还是“'”，SQL解析为一个“'”
                        {
                            inBracketString += "''";
                            index++;                     //跳过第二个“'”
                        }
                        else
                        {
                            inQuotation = false;
                            inBracketString += "'";
                        }
                    }
                    else                                 //除“'”号之外的其它字符则加到字符串中
                    {
                        inBracketString += c.ToString(); 
                    }
                }
                else                                     //不在引号内
                {
                    if (c == '\'')                       //出现了第一个引号，设置inQuotation为true
                    {
                        inQuotation = true;
                        inBracketString += "'";
                    }
                    else if (c == '(')                   //出现了“(”则括号计数加1
                    {
                        inBracketString += "(";
                        bracketDepth++;
                    }
                    else if (c == ')')                   //出现了“)”则括号计数减1，如果计数为0则表示此次整个括号的内容结束
                    {
                        bracketDepth--;
                        if (bracketDepth == 0)
                        {
                            SyntaxNode node = new SyntaxNode();
                            node.SentenceType = SentenceType.InBracket;
                            node.Sentence = inBracketString;
                            node.parentNode = this;
                            nodes.Add(node);
                            return;
                        }
                        else
                        {
                            inBracketString += ")";
                        }
                    }
                    else
                    {
                        inBracketString += c.ToString();
                    }
                }
            }
            //处理最后一个字符
            char lastChar = charArray[charArray.Length - 1];
            if (lastChar == ')' && bracketDepth==1)
            {
                SyntaxNode node = new SyntaxNode(); 
                node.parentNode = this;
                node.SentenceType = SentenceType.InBracket;
                node.Sentence = inBracketString;
                nodes.Add(node);
            }
            else
            {
                throw new Exception("缺少“)”");
            }
        }
        /// <summary>
        /// 在碰到“'”后，取出整个字符串的内容
        /// </summary>
        /// <param name="charArray">字符数组</param>
        /// <param name="index">当前字符位置</param>
        private void ParseInQuotation(ref Char[] charArray, ref int index)
        {
            string inQuotationString = "'";
            index++;
            //遍历当前字符串到倒数第二个字符
            for (; index < charArray.Length - 1; index++)
            {
                char c = charArray[index];
                if (c == '\'')
                {
                    if (charArray[index + 1] == '\'')   //如果“'”后面的还是“'”，SQL解析为一个“'”
                    {
                        inQuotationString += "''";
                        index++;                        //跳过第二个“'”
                    }
                    else                                //出现了另一个“'”号，记录整个字符串
                    {
                        inQuotationString += "'";
                        SyntaxNode node = new SyntaxNode();
                        node.ParentNode = this;
                        node.SentenceType = SentenceType.String;
                        node.sentence = inQuotationString;
                        nodes.Add(node);
                        return;
                    }
                }
                else
                {
                    inQuotationString += c.ToString();
                }
            }
            //处理最后一个字符
            char lastChar = charArray[charArray.Length - 1];
            if (lastChar == '\'')
            {
                SyntaxNode node = new SyntaxNode();
                node.ParentNode = this;
                node.SentenceType = SentenceType.String;
                node.sentence = inQuotationString+"'";
                nodes.Add(node);
            }
            else
            {
                throw new Exception("缺少“'”");
            }
        }
        /// <summary>
        /// 解析整条语句，生成树型结构
        /// </summary>
        public void Parse()
        {
            string s = string.Empty;
            Char[] charArray = sentence.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                char c=charArray[i];
                if (c == '\'')                                      //如果碰见一个“'”，调用ParseInQuotation记录从该处开始其后的字符串
                {
                    if (s != string.Empty)
                    {
                        throw new Exception("“'”附近有语法错误");
                    }
                    ParseInQuotation(ref charArray, ref i);
                }
                else if (c == '(')                                  //如果碰见一个“（”，调用ParseInBracket记录从该处开始到与之对应的“)”之间的内容
                {
                    string funName = string.Empty;
                    if (s != string.Empty)
                    {
                        SyntaxNode node = new SyntaxNode();
                        node.ParentNode = this;
                        node.sentence = s;
                        node.SentenceType = SentenceType.Function;
                        this.nodes.Add(node);
                        funName = s.ToLower();
                        s = string.Empty;
                    }
                    ParseInBracket(ref charArray, ref i);
                    if (funKeywords.Contains(funName) && this.nodes[this.nodes.Count - 1].nodes.Count > 0)
                        this.nodes[this.nodes.Count - 1].nodes[0].SentenceType = SentenceType.Keyword;
                }
                else if (c == ' ')                                  //如果碰见一个空格，记录空格前的内容
                {
                    if (s != string.Empty)
                    {
                        SyntaxNode node = new SyntaxNode();
                        node.ParentNode = this;
                        if (nodes.Count > 0 && nodes[nodes.Count - 1].sentenceType == SentenceType.From)
                        {
                            node.sentenceType = SentenceType.Entity;
                            EntityInfo entityInfo = EntityMap.GetEntityInfo(GetName(s));
                            if (entityInfo == null)
                                throw new Exception("from后有语法错误，使用了一个不存在的实体名");
							this.map = entityInfo.PropertyMap;
                        }
                        else
                            node.SentenceType = GetSentenceType(s);
                        node.sentence = GetName(s) + " ";
                        this.nodes.Add(node);
                        s = string.Empty;
                    }
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '=' || c == '!' || c == '<' || c == '>' || c == ',')//如果碰见一个符号，记录符号前的内容及符号本身
                {
                    if (s != string.Empty)
                    {
                        SyntaxNode node = new SyntaxNode();
                        node.ParentNode = this;
                        node.SentenceType = GetSentenceType(s);
                        node.sentence = s;
                        this.nodes.Add(node);
                    }
                    SyntaxNode operatorNode = new SyntaxNode();
                    operatorNode.ParentNode = this;
                    operatorNode.SentenceType = SentenceType.Symbol;
                    operatorNode.sentence = c.ToString();
                    this.nodes.Add(operatorNode);
                    s = string.Empty;
                }
                else
                {
                    s += c.ToString();
                }
            }
            //记录最后的内容
            if (s != string.Empty)
            {
                SyntaxNode node = new SyntaxNode();
                node.parentNode = this;
                if (nodes.Count > 0 && nodes[nodes.Count - 1].sentenceType == SentenceType.From)
                {
                    node.sentenceType = SentenceType.Entity;
                    EntityInfo entityInfo = EntityMap.GetEntityInfo(GetName(s));
                    if (entityInfo == null)
                        throw new Exception("from后有语法错误，使用了一个不存在的实体名");
					this.map = entityInfo.PropertyMap;
                }
                else
                    node.SentenceType = GetSentenceType(s);
                node.sentence = GetName(s);
                this.nodes.Add(node);
            }
        }
        private string GetName(string str)
        {
            string s = str.ToLower().Trim();
            if (s.IndexOf('[') == 0 && s.LastIndexOf(']') == s.Length - 1)
                s = s.Substring(1, s.Length - 2);
            return s;
        }
        /// <summary>
        /// 获取语句的类型
        /// </summary>
        /// <param name="sentence">语句</param>
        /// <returns>语句类型</returns>
        public SentenceType GetSentenceType(string sentence)
        {
            if (IsNumeric(sentence))
                return SentenceType.Numeric;
            if (sentence.Substring(0, 1) == "@")
                return SentenceType.Parameter;
            string s = sentence.ToLower();
            if (keywords.Contains(sentence))
                return SentenceType.Keyword;
            switch (s)
            {
                case "select":return SentenceType.Select;
                case "from": return SentenceType.From;
                case "where": return SentenceType.Where;
                case "group": return SentenceType.Group;
                case "order": return SentenceType.Order;
                case "by": return SentenceType.By;
                default: return SentenceType.Property;
            }
        }
        /// <summary>
        /// 判断字符串是否是数值格式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>true:是数值格式 false:不是数值格式</returns>
        private bool IsNumeric(string str)
        {
            Regex regex = new Regex("^(-?[0-9]*[.]*[0-9]{0,3})$");
            return regex.IsMatch(str);
        }
        /// <summary>
        /// 获取解析后的Sql语句
        /// </summary>
        /// <returns></returns>
        public string GetSql()
        {
			if (this.map == null)
                this.map = this.parentNode.map;
            string sql = "";
			if (this.SentenceType == SentenceType.Segment)
            {
                foreach (SyntaxNode node in nodes)
                {
                    sql += node.GetSql();
                }
            }
            else if (this.SentenceType == SentenceType.InBracket)
            {
                foreach (SyntaxNode node in nodes)
                {
                    sql += node.GetSql();
                }
                sql = "(" + sql + ")";
            }
            else if(this.SentenceType==SentenceType.Property)
            {
                sql += this.sentence.Replace(this.sentence.Trim(), "["+this.map[this.sentence.Trim()]+"]");
            }
            else if (this.SentenceType == SentenceType.Entity)
            {
                sql += this.sentence.Replace(this.sentence.Trim(), "["+EntityMap.GetEntityInfo(this.sentence.Trim()).TableName+"]");
            }
			else if (this.SentenceType == SentenceType.String)
            {
                sql = this.sentence+" ";
            }
            else
            {
                sql = this.sentence;
            }
            return sql;
        }
        /// <summary>
        /// 获取语句中包含的参数列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetParameters()
        {
            List<string> parameters = new List<string>();
            if (this.SentenceType == SentenceType.Parameter)
            {
                parameters.Add(this.sentence.Trim());
            }
            else if (this.SentenceType == SentenceType.InBracket)
            {
                foreach (SyntaxNode node in nodes)
                {
                    List<string> childParameters = node.GetParameters();
                    foreach (string para in childParameters)
                    {
                        parameters.Add(para);
                    }
                }
            }
            return parameters;
        }
    }
    enum SentenceType { Segment, InBracket, String, Numeric, Parameter, Entity, Property, Symbol, Keyword, Function, Select, From, Where, Group, By, Order }
}