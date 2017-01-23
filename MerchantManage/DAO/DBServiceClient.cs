using MerchantManage.Models;
using Saxon.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace MerchantManage.DAO
{
    public class DBServiceClient : ServiceClient
    {
        public string SendRequest(Merchant mer,String currentNode)
        {
            Processor processor = new Processor();
            DocumentBuilder docBuilder = processor.NewDocumentBuilder();
            string path = @"E:\csharp\MerchantManage\MerchantManage\test\xml\datastore.xml";
            XmlDocument dok = new XmlDocument();
            dok.Load(path);
            XdmNode doc = docBuilder.Wrap(dok);
            XPathCompiler xcompiler = processor.NewXPathCompiler();
            /* List<String> lst = mer.parts;
             String element = "";
             if (lst.Count > 0)
              element = lst.Last();*/
            String catid = currentNode;//Categories.GetCode(element);
            String xpathexpression = String.Format("descendant-or-self::Category[@id=\"{0}\"]", catid);
            XPathExecutable xe = xcompiler.Compile(xpathexpression);
            XPathSelector selec = xe.Load();
            selec.ContextItem = doc;
            XdmItem targetnode = selec.EvaluateSingle();

            if (targetnode != null)
            {
                XmlDocument dock = new XmlDocument();
                XPathExecutable xee = xcompiler.Compile("descendant-or-self::*");
                XPathSelector selecc = xee.Load();
                //selecc.ContextItem = targetnode;
                //XdmItem cibleitem = selecc.EvaluateSingle();
                


                xee = xcompiler.Compile("ancestor::*");
                selecc = xee.Load();
                selecc.ContextItem = targetnode;
                XdmValue ances = selecc.Evaluate();
                
                XmlNode xn = null;
                XmlNode target = dock;
                foreach (XdmItem s in ances)
                {
                    XmlElement ele = (XmlElement)((XdmNode)s).getUnderlyingXmlNode();
                    xn = dock.ImportNode(ele,false);
                    target.AppendChild(xn);
                    target = xn;

                }

                if (target != null)
                {
                    XmlElement eli = (XmlElement)((XdmNode)targetnode).getUnderlyingXmlNode();
                    XmlNode xm = dock.ImportNode(eli, true);
                    
                    XmlNode w = xm.CloneNode(false);
                    target.AppendChild(w);
                    
                    XmlNodeList children =  xm.ChildNodes;
                    
                    foreach (XmlNode c in children)
                    {
                        XmlNode p = c.CloneNode((c is XmlElement) && ((XmlElement)c).Name.Equals("Item"));
                        target.AppendChild(p);
                    }
                }
                return dock.OuterXml;
                /*XmlDocument dock = new XmlDocument();
                dock.Load(@"E:\csharp\MerchantManage\MerchantManage\test\xml\Division.xml");

                DocumentBuilder buil = processor.NewDocumentBuilder();
                XdmNode nd = buil.Wrap(dock);
                XPathExecutable xx = xcompiler.Compile("//Catalogue//Zone//Division");
                XPathSelector selector = xx.Load();
                selector.ContextItem = nd;
                XdmItem it = selector.EvaluateSingle();
                XmlNode xmlnode = ((XdmNode)it).getUnderlyingXmlNode();
                foreach (Object item in results)
                {
                    XdmNode nde = (XdmNode)item;
                    XmlNode xr = nde.getUnderlyingXmlNode();
                    XmlNode xn = dock.ImportNode(xr, true);
                    xmlnode.AppendChild(xn);
                }
                return dock.InnerXml;*/
            }
            return "";
        }

    }
}