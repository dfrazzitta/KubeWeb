using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace faApi.Controllers
{
    [Route("[controller]/[action]")]
  //  [ApiController]
    public class KubernetesSystemController : ControllerBase
    {
        KubernetesClientConfiguration _config = null;
        ILogger _logger;

        public KubernetesSystemController(ILogger<Kubernetes> logger)
        {
            _logger = logger;
#if RELEASE
            _logger.LogInformation("enter release configuration");
            //var config = KubernetesClientConfiguration.InClusterConfig();  
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"config", null, null, true);

            _logger.LogInformation(config.Host);
#else
            // this is debug and config is copied to the docker image 
            _logger.LogInformation("enter debug configuration");
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"config", null, null, true);
            _logger.LogInformation(config.Host);
#endif 
            _config = config;
        }


        //GetNameSpacesOnly

        [HttpGet]
        public string GetNameSpacesOnly() // string type)
        {
            IKubernetes client = new Kubernetes(_config);
            var clusterNodes = client.ListNamespace();
            V1NamespaceList clusterNodes1 = client.ListNamespace();
            IList<V1Namespace> items = clusterNodes1.Items;
            JArray array = new JArray();
            foreach (k8s.Models.V1Namespace i in items)
            {
                array.Add(i.Name());
            }
            /*
            JObject obj = new JObject();
            obj["NameSpaces"] = array;
            string json = obj.ToString();

            var json1 = JsonSerializer.Serialize(clusterNodes1);
            */
            var jj = array.ToString();
            return array.ToString();
            /*
            JArray array = new JArray();
            foreach (V1Namespace nl in clusterNodes.Items)
            {
                array.Add(nl.Name());

            }
            JObject obj = new JObject();
            obj["NameSpaces"] = array;
            string json = obj.ToString();
            //string clusterNodesOut = JsonConvert.SerializeObject(clusterNodes);
            return "this is data"; */
        }


        [HttpGet]
        public string GetNameSpaces() // string type)
        {
            IKubernetes client = new Kubernetes(_config);
            var clusterNodes = client.ListNamespace();
            V1NamespaceList clusterNodes1 = client.ListNamespace();
              
            return JsonSerializer.Serialize(clusterNodes1); ;
            /*
            JArray array = new JArray();
            foreach (V1Namespace nl in clusterNodes.Items)
            {
                array.Add(nl.Name());

            }
            JObject obj = new JObject();
            obj["NameSpaces"] = array;
            string json = obj.ToString();
            //string clusterNodesOut = JsonConvert.SerializeObject(clusterNodes);
            return "this is data"; */
        }

        [HttpGet]
        public string GetDaemons() // string type)
        {
            IKubernetes client = new Kubernetes(_config);
            var clusterNodes = client.ListDaemonSetForAllNamespaces();
            //V1NamespaceList clusterNodes1 = client.ListNamespace();
            //IList<V1Namespace> items = clusterNodes1.Items;
            var json1 = JsonSerializer.Serialize(clusterNodes);
            return json1;

        }


        [HttpGet]
        public string GetServices() // string type)
        {
            IKubernetes client = new Kubernetes(_config);
            var clusterNodes = client.ListServiceForAllNamespaces();
            //V1NamespaceList clusterNodes1 = client.ListNamespace();
            //IList<V1Namespace> items = clusterNodes1.Items;
            var json1 = JsonSerializer.Serialize(clusterNodes);
            return json1;

        }


        [HttpGet]
        public string GetSystemInfo() // string type)
        {
            IKubernetes client = new Kubernetes(_config);
            var clusterNodes = client.GetCode();
            //V1NamespaceList clusterNodes1 = client.ListNamespace();
            //IList<V1Namespace> items = clusterNodes1.Items;
            var json1 = JsonSerializer.Serialize(clusterNodes);
            return json1;
            
        }

        [HttpGet]
        public string GetPods() // string type)
        {
            IKubernetes client = new Kubernetes(_config);
            var clusterNodes = client.ListPodForAllNamespaces();
            //V1NamespaceList clusterNodes1 = client.ListNamespace();
            //IList<V1Namespace> items = clusterNodes1.Items;
            var json1 = JsonSerializer.Serialize(clusterNodes);
            return json1;

        }

        // GET: api/<KubernetesSystem>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<KubernetesSystem>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<KubernetesSystem>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<KubernetesSystem>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<KubernetesSystem>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
