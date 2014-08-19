using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSimpleXml();
            ReadSimpleXml();
            ReadThruLinqToXml();
            CreateTypeInfo();
            Transformation();
        }

        private static void Transformation()
        {
            XDocument doc = XDocument.Load("Employees.xml");
            XDocument transformedDoc = new XDocument(
                new XElement("Employees",
                    new XElement("Developers",doc.Descendants("Employee")
                        .Where(e => e.Attribute("Type").Value == "Developer")
                        .Select(e => new XElement("Employee",e.Value))),
                    new XElement("Sales",doc.Descendants("Employee")
                        .Where(e => e.Attribute("Type").Value == "Sales")
                        .Select(e => new XElement("Employee", e.Value)))));
            transformedDoc.Save("EmployeesGroupedByDept.xml");
        }

        private static void CreateTypeInfo()
        {
            XNamespace ns = "http://budigi.com/Assemblies";
            XDocument doc = new XDocument(
                new XElement(ns+"Types",
                    Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                    .Select(name => Assembly.Load(name))
                    .SelectMany(assembly => assembly.GetTypes())
                    .Select(type => new XElement(ns+"Type", type.FullName,
                        new XAttribute("IsPublic", type.IsPublic)))));
        }

        private static void ReadThruLinqToXml()
        {
            XDocument doc = XDocument.Load("Employees.xml");
            var query=doc.Descendants("Employee")
                .Where(e => e.Attribute("Type").Value == "Developer")
                .OrderBy(e => e.Value)
                .Select(e => e.Value);
                
        }

        private static void ReadSimpleXml()
        {
            XDocument doc = XDocument.Load("Modules.xml");
            XElement root = doc.Root;
            
            var elements = root.Descendants();
            foreach (var element in elements)
            {
                string value = element.Value;
            }
        }

        private static void CreateSimpleXml()
        {
            XNamespace ns = "http://LearnLinq/Modules";
            XNamespace ext = "http://LearnLinq/Modules/Ext";

            XDocument doc = new XDocument(
                new XElement(ns+"Modules",
                    new XAttribute(XNamespace.Xmlns+"ext",ext),
                    new XElement(ns+"Module", "Introduction to XML"),
                    new XElement(ns+"Module", "Introduction to Linq"),
                    new XElement(ext+"Module", "Introduction to Silverlight"),
                    new XElement(ext+"Module", "Introduction to MVC")
                    ));
            doc.Save("Modules.xml");
        }
    }
}
