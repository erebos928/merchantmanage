using Saxon.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MerchantManage.Models
{
    public class XslTransformer
    {
        public void SetTemplate(String path)
        {

        }
        public String Transform(String source,Merchant mer)
        {
            Processor processor = new Processor();
            XsltCompiler compiler = processor.NewXsltCompiler();
            //FileStream stream = new FileStream(@"E:\csharp\MerchantManage\MerchantManage\test\xml\gabarit3.xsl", FileMode.Open);
            MemoryStream mstream = new MemoryStream();
            if (String.IsNullOrEmpty(mer.XsltTemplate))
                return source;
            byte[] template = System.Text.Encoding.UTF8.GetBytes(mer.XsltTemplate);
            mstream.Write(template,0,template.Length);
            mstream.Position = 0;
            XsltTransformer xt = compiler.Compile(mstream).Load();
            MemoryStream instream = new MemoryStream();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(source);
            instream.Write(bs,0,bs.Length);
            instream.Position = 0;
            xt.SetInputStream(instream, new Uri("http://www.example.com/"));
            Serializer dest = new Serializer();
            MemoryStream output = new MemoryStream();
            dest.SetOutputStream(output);
            xt.Run(dest);
            String result = System.Text.Encoding.UTF8.GetString(output.ToArray());
            return result;
        }
    }
}