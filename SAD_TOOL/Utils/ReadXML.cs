using SAD_TOOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SAD_TOOL.Utils
{
    public class ReadXML
    {
        /// <summary>
        /// Leer XML de getinfo
        /// </summary>
        /// <param name="pChallenge"></param>
        /// <returns></returns>
        public static List<string> readInfo(Challenge pChallenge)
        {
            List<string> lstlevel = new List<string>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(pChallenge.InfoXML);

            XmlNode root = document.FirstChild;

            if (root.HasChildNodes)
            {
                List<ParamList> paramList = new List<ParamList>();

                // get all nodes with tag name "Level"
                foreach (XmlNode node in root.ChildNodes)
                {
                    foreach (XmlNode node1 in node.ChildNodes)
                    {
                        if (node.Name.Contains(Param.XML_INFO_PLUGIN_NAME))
                        {
                            pChallenge.Name = node1.Value;
                        }
                        else if (node.Name.Contains(Param.XML_INFO_DESCRIPTION))
                        {
                            pChallenge.Description = node1.Value;
                        }
                        else if (node.Name.Contains(Param.XML_INFO_RELEASE))
                        {
                            pChallenge.Version = node1.Value;
                        }
                        else if (node.Name.Contains(Param.XML_INFO_PARAM_LIST))
                        {
                            ParamList param = new ParamList();

                            XmlAttributeCollection attr = node1.Attributes;
                            foreach (XmlAttribute paramAttr in attr)
                            {
                                if (paramAttr.Name == Param.XML_INFO_ATTR_type)
                                {
                                    param.type = paramAttr.Value;
                                }
                                else if (paramAttr.Name == Param.XML_INFO_ATTR_name)
                                {
                                    param.name = paramAttr.Value;
                                }
                            }

                            param.value = node1.Value;

                            paramList.Add(param);
                        }
                    }
                }

                pChallenge.ParamList = paramList;
            }

            return lstlevel;
        }

        public static int getSizeFromSad(string sadFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sadFile);
            XmlNodeList parentNode = xmlDoc.GetElementsByTagName(Param.XML_FILE_SAD_HEADER);
            foreach (XmlNode childrenNode in parentNode)
            {
                foreach (XmlNode paramNode in childrenNode)
                {
                    XmlAttributeCollection paramAttrCollec = paramNode.Attributes;

                    foreach (XmlAttribute paramAttr in paramAttrCollec)
                    {
                        if (paramAttr.Value == Param.XML_FILE_ATTR_file_size)
                        {
                            int size = Convert.ToInt32(paramNode.InnerText);
                            return size;
                        }
                    }
                }
            }
            return 0;
        }

        public static string getExtFromSad(string sadFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sadFile);
            XmlNodeList parentNode = xmlDoc.GetElementsByTagName(Param.XML_FILE_CIPHEREDFILE);
            foreach (XmlNode childrenNode in parentNode)
            {
                XmlAttributeCollection paramAttrCollec = childrenNode.Attributes;

                foreach (XmlAttribute paramAttr in paramAttrCollec)
                {
                    if (paramAttr.Name == Param.XML_FILE_ATTR_type)
                    {
                        string result = paramAttr.Value;
                        return result;
                    }
                }
            }

            return "";
        }

        public static string getNameFromSad(string sadFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sadFile);
            XmlNodeList parentNode = xmlDoc.GetElementsByTagName(Param.XML_FILE_CIPHEREDFILE);
            foreach (XmlNode childrenNode in parentNode)
            {
                XmlAttributeCollection paramAttrCollec = childrenNode.Attributes;

                foreach (XmlAttribute paramAttr in paramAttrCollec)
                {
                    if (paramAttr.Name == Param.XML_FILE_ATTR_name)
                    {
                        string result = paramAttr.Value;
                        return result;
                    }
                }
            }

            return "";
        }

        public static string getExtFromSadOld(string sadFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sadFile);
            XmlNodeList parentNode = xmlDoc.GetElementsByTagName(Param.XML_FILE_SAD_HEADER);
            foreach (XmlNode childrenNode in parentNode)
            {
                foreach (XmlNode paramNode in childrenNode)
                {
                    XmlAttributeCollection paramAttrCollec = paramNode.Attributes;

                    foreach (XmlAttribute paramAttr in paramAttrCollec)
                    {
                        if (paramAttr.Value == Param.XML_FILE_ATTR_ext)
                        {
                            string ext = paramNode.InnerText;
                            return ext;
                        }
                    }
                }
            }
            return "";
        }

        public static string getBase64CipheredFile(string sadFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sadFile);
            XmlNodeList parentNode = xmlDoc.GetElementsByTagName(Param.XML_FILE_CIPHEREDFILE);
            foreach (XmlNode childrenNode in parentNode)
            {
                string cipheredFile = childrenNode.InnerText;
                return cipheredFile;
            }
            return "";
        }

        public static List<Challenge> getChallengesFromSad(string sadFile)
        {
            List<Challenge> list = new List<Challenge>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sadFile);

            XmlNodeList parentNode = xmlDoc.GetElementsByTagName(Param.XML_FILE_CHALLENGE);
            foreach (XmlNode childrenNode in parentNode)
            {
                Challenge cha = new Challenge();

                XmlAttributeCollection childAttrCollec = childrenNode.Attributes;
                foreach (XmlAttribute childrenAttr in childAttrCollec)
                {
                    if (childrenAttr.Name == Param.XML_FILE_ATTR_name)
                    {
                        cha.Type = childrenAttr.Value;
                        cha.ExecuteXml = childrenNode.OuterXml;

                        list.Add(cha);
                    }
                }
            }

            return list;
        }
    }
}
