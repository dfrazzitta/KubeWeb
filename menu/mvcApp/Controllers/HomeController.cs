using k8s;
using Microsoft.AspNetCore.Mvc;
using mvcApp.classes.tree;
using mvcApp.Models;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Diagnostics;

namespace mvcApp.Controllers
{
    public class Stockmodel

    {

        public int stockId { get; set; }

        public string StockName { get; set; }

        public decimal StockPrice { get; set; }

    }
    public class UserData
    {
        public List<UserData1> pets { get; set; }
       // public string age { get; set; }
    }
    public class UserData1
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string age { get; set; }
        // public string age { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            //add parent nodes
            nodes.Add(new TreeViewNode { id = "1", parent = "#", text = "Rajasthan" });
            nodes.Add(new TreeViewNode { id = "2", parent = "#", text = "Gujrat" });


            //add child nodes for first parent
            nodes.Add(new TreeViewNode { id = "11", parent = "1", text = "Jaipur" });
            nodes.Add(new TreeViewNode { id = "12", parent = "1", text = "Ajmer" });

            //add child nodes for second parent
            nodes.Add(new TreeViewNode { id = "21", parent = "2", text = "Baroda" });
            nodes.Add(new TreeViewNode { id = "22", parent = "2", text = "Surat" });


            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }


        public IActionResult Privacy1()
        {
            return View();

        }



        public IActionResult DoIt()
        {
            return View();

        }

        public string GetInfo()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            nodes.Add(new TreeViewNode { id = "1", parent = "#", text = "NameSpaces", checkbox_disabled = true });

            // var k8SClientConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile("/app/config");
            var k8SClientConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            IKubernetes client = new Kubernetes(k8SClientConfig);

            var list = client.CoreV1.ListNamespace();
            int ctr = 10;
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
                nodes.Add(new TreeViewNode { id = ctr.ToString(), parent = "1", text = item.Metadata.Name });
                ctr++;
            }

            var rtn = JsonConvert.SerializeObject(nodes);
            return rtn;
        }


        [HttpGet]
        public string GetPodnameSpaceGet(string udata)
        {
            var kk = this.HttpContext;
            var jj = this.HttpContext.Request.QueryString.ToString();
            return "";
        }





            [HttpPost]
        public string GetPodnameSpace(string udata)
        {
           // var  MyFinalObject = JsonConvert.DeserializeObject(id);
           // var bb = MyFinalObject: id; //.ToString();
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            /*
            nodes.Add(new TreeViewNode { id = "1", parent = "#", text = "NameSpaces", checkbox_disabled = true });

            // var k8SClientConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile("/app/config");
            var k8SClientConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            IKubernetes client = new Kubernetes(k8SClientConfig);

            var list = client.CoreV1.ListNamespacedPod("default");
            int ctr = 10;
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
                nodes.Add(new TreeViewNode { id = ctr.ToString(), parent = "1", text = item.Metadata.Name });
                ctr++;
            }

           // List<TreeViewNode> nodes = new List<TreeViewNode>();

            //add parent nodes
 
           // nodes.Add(new TreeViewNode { id = "2", parent = "#", text = "Gujrat" });


            //add child nodes for first parent
            //nodes.Add(new TreeViewNode { id = "11", parent = "1", text = "Default" });
            //nodes.Add(new TreeViewNode { id = "12", parent = "1", text = "Kube-System" });
           // nodes.Add(new TreeViewNode { id = "13", parent = "1", text = "Metallb-System" });
           // nodes.Add(new TreeViewNode { id = "14", parent = "1", text = "Monitoring" });

            //add child nodes for second parent
            //nodes.Add(new TreeViewNode { id = "21", parent = "2", text = "Baroda" });
           // nodes.Add(new TreeViewNode { id = "22", parent = "2", text = "Surat" });


            //Serialize to JSON string.
            //ViewBag.Json = JsonConvert.SerializeObject(nodes);
            var rtn = JsonConvert.SerializeObject(nodes);

            */
            return null; // rtn;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}