using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;

namespace WebTest.Controllers
{
    public class Kubernetes : Controller
    {
        // GET: Kubernetes
        public ActionResult Index()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListNamespacedPod("default");
            foreach (var item in list.Items)
            {
                Console.WriteLine(item.Metadata.Name);
            }

            if (list.Items.Count == 0)
            {
                Console.WriteLine("Empty!");
            }
            return View();
        }

         

        public async Task<string> GetNodesMetrics()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = await client.GetKubernetesNodesMetricsAsync().ConfigureAwait(false); 

            var Json = JsonConvert.SerializeObject(list);

            return Json;
            //return View();
        }

        public async Task<string> GetLogs()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config");
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListNamespacedPod("default");
            if (list.Items.Count == 0)
            {
                return "";
            }
            var pod = list.Items[1];
            var response = await client.CoreV1
                .ReadNamespacedPodLogWithHttpMessagesAsync(
                pod.Metadata.Name,
                pod.Metadata.NamespaceProperty, container: pod.Spec.Containers[0].Name, follow: true).ConfigureAwait(false);
            var stream = response.Body;
            /////////////////////////////////////////////////////////////////
            var standardOutput = new StreamReader(stream, Encoding.UTF8);
            StringBuilder sb = new StringBuilder();
            try
            {
                while (standardOutput.Peek() >= 0)
                {
                    string line = standardOutput.ReadLine();
                    sb.Append(line);
                    sb.Append("\n");
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                var ty = ex.Message;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            string bb = sb.ToString();
            //Regex.Replace(bb, @"[^0-9a-zA-Z]+", "");
            //Regex.Replace(Regex.Replace(bb, "\x1B(?:[@-Z\-_]|[[0 -?][-/][@-~])", ""),"\a","");
            //string jjbb = Regex.Replace(bb, @"[^\u0000-\u007f]+", string.Empty);
            //bb = Regex.Replace(bb, @"[^\u0020-\u007E]", string.Empty);
            //var Json = JsonConvert.SerializeObject(response.Body);

            return bb;
            //return View();
        }

        public string GetLimitRange()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListLimitRangeForAllNamespaces();
 
            var Json = JsonConvert.SerializeObject(list);

            return Json;
            //return View();
        }

        public string Getpvc()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListPersistentVolumeClaimForAllNamespaces();
 
            var Json = JsonConvert.SerializeObject(list);
            var js = JsonConvert.SerializeObject(list.Items);

            return Json;
            //return View();
        }



        public string Getpv()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListPersistentVolume();
 
            var Json = JsonConvert.SerializeObject(list);

            return Json;
            //return View();
        }



        public IEnumerable<string> Getns()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListNamespace();
 
            var Json = JsonConvert.SerializeObject(list);
            var Js = JsonConvert.SerializeObject(list.Items);
            V1Pod[] vp = JsonConvert.DeserializeObject<V1Pod[]>(Js);
            IEnumerable<string> str55 = from name in list.Items
                                        select name.Metadata.Name;

            return str55;
            //return View();
        }
        public string GetPods()
        {
            string path = Environment.CurrentDirectory;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(
                  path + "/config"); //.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListNamespacedPod("default");
 
            var Json = JsonConvert.SerializeObject(list);

            return Json;
            //return View();
        }




        // GET: Kubernetes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kubernetes/Create
        public ActionResult Create()
        {
            var config = KubernetesClientConfiguration.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListNamespacedPod("default");
 

            return View();
        }

        // POST: Kubernetes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Kubernetes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kubernetes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Kubernetes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kubernetes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
