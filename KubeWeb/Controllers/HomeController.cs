using AspNetCore.Unobtrusive.Ajax;
using Humanizer.Localisation;
using k8s.Models;
using KubeClasses;
using KubeWeb.classes;
using KubeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using StackExchange.Profiling.Internal;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;

namespace KubeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private IActionResult SweetAlert(string title, string message, string type)
        {
            HttpContext _hp = this.HttpContext;
            return Content($"swal ('{title}',  '{message}',  '{type}')", "text/javascript");
        }

        public async Task<IActionResult> ListNamespace()
        {

            //var httpContent = new StringContent(JsonConvert.SerializeObject("dave"), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://KubeApi/api/Kube/ListNamespace");
                //  Assert.True(response.IsSuccessStatusCode);
                response.EnsureSuccessStatusCode();
                var jk = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                ViewData["data"] = jk; // await response.Content.ReadAsStringAsync();
            }

            return View();
        }


        public async Task<string> GetNode(string jsonInput)
        {
            var str = jsonInput;
            kubeParams lp = new kubeParams();
            lp.kubenamespace = "default";
            lp.kubeaction = "GetNodesList";

            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(lp, Formatting.Indented);
                var httpContent1 = new StringContent(json, Encoding.UTF8, "application/json");
                var response1 = await client.PostAsync("http://KubeApi/api/kube/GetNodes", httpContent1);
                // var httpContent1 = new StringContent(json, Encoding.UTF8, "application/json");
                // var response1 = await client.PostAsync("http://KubeApi/kube/PostListNamespaceList/", httpContent1);
                //var httpContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");
                //var response = await client.PostAsync("http://KubeApi/api/values/", httpContent);
                //  Assert.True(response.IsSuccessStatusCode);
                response1.EnsureSuccessStatusCode();
                var jk = await response1.Content.ReadAsStringAsync().ConfigureAwait(false);
                //ViewData["data"] = jk; // await response.Content.ReadAsStringAsync();
                return jk;
            }

        }

        public async Task<string>  GetNodeList(string jsonInput)
        {
            var str = jsonInput;
            kubeParams lp = new kubeParams();
            lp.kubenamespace = "default";
            lp.kubeaction = "PostNodesList";

            using (var client = new HttpClient())
            { 
              // KubeLibrary.Account ac = new Account();
              //  ac.Email = "james@example.com";

               string json = JsonConvert.SerializeObject(lp, Formatting.Indented);


                var httpContent1 = new StringContent(json, Encoding.UTF8, "application/json");
              
                var response1 = await client.PostAsync("http://KubeApi/api/kube/PostNodesList", httpContent1);
           



                // var httpContent1 = new StringContent(json, Encoding.UTF8, "application/json");
                // var response1 = await client.PostAsync("http://KubeApi/kube/PostListNamespaceList/", httpContent1);
                //var httpContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");
                //var response = await client.PostAsync("http://KubeApi/api/values/", httpContent);
                //  Assert.True(response.IsSuccessStatusCode);
                response1.EnsureSuccessStatusCode();
                var jk = await response1.Content.ReadAsStringAsync().ConfigureAwait(false);
                //ViewData["data"] = jk; // await response.Content.ReadAsStringAsync();
                return jk;
            }

           // V1NodeList vml = new V1NodeList();
           // return vml;

        }



        public IActionResult Index(string namespacex  )
        {
            namespacex = "x";
            return View();
        }


        [HttpGet]
        [AjaxOnly]  //1111
        public IActionResult AjaxMethod1(string name)
        {
            Thread.Sleep(4000);

            return SweetAlert($"Hello boo", "Message returned from Server!", "success");
        }



        public IActionResult Grid()
        {
            return SweetAlert("Oops!", "Please select a file for upload.", "warning");
            //return View();
        }

        [HttpGet]
        public string GetPodnameSpaceGet(string udata)
        {
#region d1
            string json  = @"{
            ""apiVersion"": ""v1"",
            ""items"": [
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                        ""generateName"": ""coredns-558bd4d5db-"",
                        ""labels"": {
                            ""k8s-app"": ""kube-dns"",
                            ""pod-template-hash"": ""558bd4d5db""
                        },
                        ""name"": ""coredns-558bd4d5db-5xxwq"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""apps/v1"",
                                ""blockOwnerDeletion"": true,
                                ""controller"": true,
                                ""kind"": ""ReplicaSet"",
                                ""name"": ""coredns-558bd4d5db"",
                                ""uid"": ""0cc89c70-d135-4564-8f13-5ebdbb55a758""
                            }
                        ],
                        ""resourceVersion"": ""583"",
                        ""uid"": ""a38644dd-665f-43f0-b304-1a23bcf849bc""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""args"": [
                                    ""-conf"",
                                    ""/etc/coredns/Corefile""
                                ],
                                ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""livenessProbe"": {
                                    ""failureThreshold"": 5,
                                    ""httpGet"": {
                                        ""path"": ""/health"",
                                        ""port"": 8080,
                                        ""scheme"": ""HTTP""
                                    },
                                    ""initialDelaySeconds"": 60,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 5
                                },
                                ""name"": ""coredns"",
                                ""ports"": [
                                    {
                                        ""containerPort"": 53,
                                        ""name"": ""dns"",
                                        ""protocol"": ""UDP""
                                    },
                                    {
                                        ""containerPort"": 53,
                                        ""name"": ""dns-tcp"",
                                        ""protocol"": ""TCP""
                                    },
                                    {
                                        ""containerPort"": 9153,
                                        ""name"": ""metrics"",
                                        ""protocol"": ""TCP""
                                    }
                                ],
                                ""readinessProbe"": {
                                    ""failureThreshold"": 3,
                                    ""httpGet"": {
                                        ""path"": ""/ready"",
                                        ""port"": 8181,
                                        ""scheme"": ""HTTP""
                                    },
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 1
                                },
                                ""resources"": {
                                    ""limits"": {
                                        ""memory"": ""170Mi""
                                    },
                                    ""requests"": {
                                        ""cpu"": ""100m"",
                                        ""memory"": ""70Mi""
                                    }
                                },
                                ""securityContext"": {
                                    ""allowPrivilegeEscalation"": false,
                                    ""capabilities"": {
                                        ""add"": [
                                            ""NET_BIND_SERVICE""
                                        ],
                                        ""drop"": [
                                            ""all""
                                        ]
                                    },
                                    ""readOnlyRootFilesystem"": true
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/coredns"",
                                        ""name"": ""config-volume"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                        ""name"": ""kube-api-access-rfh87"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""Default"",
                        ""enableServiceLinks"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""nodeSelector"": {
                            ""kubernetes.io/os"": ""linux""
                        },
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000000000,
                        ""priorityClassName"": ""system-cluster-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""serviceAccount"": ""coredns"",
                        ""serviceAccountName"": ""coredns"",
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""key"": ""CriticalAddonsOnly"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node-role.kubernetes.io/master""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node-role.kubernetes.io/control-plane""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/not-ready"",
                                ""operator"": ""Exists"",
                                ""tolerationSeconds"": 300
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/unreachable"",
                                ""operator"": ""Exists"",
                                ""tolerationSeconds"": 300
                            }
                        ],
                        ""volumes"": [
                            {
                                ""configMap"": {
                                    ""defaultMode"": 420,
                                    ""items"": [
                                        {
                                            ""key"": ""Corefile"",
                                            ""path"": ""Corefile""
                                        }
                                    ],
                                    ""name"": ""coredns""
                                },
                                ""name"": ""config-volume""
                            },
                            {
                                ""name"": ""kube-api-access-rfh87"",
                                ""projected"": {
                                    ""defaultMode"": 420,
                                    ""sources"": [
                                        {
                                            ""serviceAccountToken"": {
                                                ""expirationSeconds"": 3607,
                                                ""path"": ""token""
                                            }
                                        },
                                        {
                                            ""configMap"": {
                                                ""items"": [
                                                    {
                                                        ""key"": ""ca.crt"",
                                                        ""path"": ""ca.crt""
                                                    }
                                                ],
                                                ""name"": ""kube-root-ca.crt""
                                            }
                                        },
                                        {
                                            ""downwardAPI"": {
                                                ""items"": [
                                                    {
                                                        ""fieldRef"": {
                                                            ""apiVersion"": ""v1"",
                                                            ""fieldPath"": ""metadata.namespace""
                                                        },
                                                        ""path"": ""namespace""
                                                    }
                                                ]
                                            }
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:03Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:13Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:13Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://8fbc91a70879c56a5b60c7406176e6297fcc374f082a0754d25def354aa42659"",
                                ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                                ""imageID"": ""sha256:296a6d5035e2d6919249e02709a488d680ddca91357602bd65e605eac967b899"",
                                ""lastState"": {},
                                ""name"": ""coredns"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:59:06Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""10.244.0.3"",
                        ""podIPs"": [
                            {
                                ""ip"": ""10.244.0.3""
                            }
                        ],
                        ""qosClass"": ""Burstable"",
                        ""startTime"": ""2023-08-12T13:59:03Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                        ""generateName"": ""coredns-558bd4d5db-"",
                        ""labels"": {
                            ""k8s-app"": ""kube-dns"",
                            ""pod-template-hash"": ""558bd4d5db""
                        },
                        ""name"": ""coredns-558bd4d5db-6zzkc"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""apps/v1"",
                                ""blockOwnerDeletion"": true,
                                ""controller"": true,
                                ""kind"": ""ReplicaSet"",
                                ""name"": ""coredns-558bd4d5db"",
                                ""uid"": ""0cc89c70-d135-4564-8f13-5ebdbb55a758""
                            }
                        ],
                        ""resourceVersion"": ""572"",
                        ""uid"": ""5d26f06e-28fb-4d0e-a2a5-2e4e1856c030""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""args"": [
                                    ""-conf"",
                                    ""/etc/coredns/Corefile""
                                ],
                                ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""livenessProbe"": {
                                    ""failureThreshold"": 5,
                                    ""httpGet"": {
                                        ""path"": ""/health"",
                                        ""port"": 8080,
                                        ""scheme"": ""HTTP""
                                    },
                                    ""initialDelaySeconds"": 60,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 5
                                },
                                ""name"": ""coredns"",
                                ""ports"": [
                                    {
                                        ""containerPort"": 53,
                                        ""name"": ""dns"",
                                        ""protocol"": ""UDP""
                                    },
                                    {
                                        ""containerPort"": 53,
                                        ""name"": ""dns-tcp"",
                                        ""protocol"": ""TCP""
                                    },
                                    {
                                        ""containerPort"": 9153,
                                        ""name"": ""metrics"",
                                        ""protocol"": ""TCP""
                                    }
                                ],
                                ""readinessProbe"": {
                                    ""failureThreshold"": 3,
                                    ""httpGet"": {
                                        ""path"": ""/ready"",
                                        ""port"": 8181,
                                        ""scheme"": ""HTTP""
                                    },
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 1
                                },
                                ""resources"": {
                                    ""limits"": {
                                        ""memory"": ""170Mi""
                                    },
                                    ""requests"": {
                                        ""cpu"": ""100m"",
                                        ""memory"": ""70Mi""
                                    }
                                },
                                ""securityContext"": {
                                    ""allowPrivilegeEscalation"": false,
                                    ""capabilities"": {
                                        ""add"": [
                                            ""NET_BIND_SERVICE""
                                        ],
                                        ""drop"": [
                                            ""all""
                                        ]
                                    },
                                    ""readOnlyRootFilesystem"": true
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/coredns"",
                                        ""name"": ""config-volume"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                        ""name"": ""kube-api-access-pxg2p"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""Default"",
                        ""enableServiceLinks"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""nodeSelector"": {
                            ""kubernetes.io/os"": ""linux""
                        },
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000000000,
                        ""priorityClassName"": ""system-cluster-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""serviceAccount"": ""coredns"",
                        ""serviceAccountName"": ""coredns"",
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""key"": ""CriticalAddonsOnly"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node-role.kubernetes.io/master""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node-role.kubernetes.io/control-plane""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/not-ready"",
                                ""operator"": ""Exists"",
                                ""tolerationSeconds"": 300
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/unreachable"",
                                ""operator"": ""Exists"",
                                ""tolerationSeconds"": 300
                            }
                        ],
                        ""volumes"": [
                            {
                                ""configMap"": {
                                    ""defaultMode"": 420,
                                    ""items"": [
                                        {
                                            ""key"": ""Corefile"",
                                            ""path"": ""Corefile""
                                        }
                                    ],
                                    ""name"": ""coredns""
                                },
                                ""name"": ""config-volume""
                            },
                            {
                                ""name"": ""kube-api-access-pxg2p"",
                                ""projected"": {
                                    ""defaultMode"": 420,
                                    ""sources"": [
                                        {
                                            ""serviceAccountToken"": {
                                                ""expirationSeconds"": 3607,
                                                ""path"": ""token""
                                            }
                                        },
                                        {
                                            ""configMap"": {
                                                ""items"": [
                                                    {
                                                        ""key"": ""ca.crt"",
                                                        ""path"": ""ca.crt""
                                                    }
                                                ],
                                                ""name"": ""kube-root-ca.crt""
                                            }
                                        },
                                        {
                                            ""downwardAPI"": {
                                                ""items"": [
                                                    {
                                                        ""fieldRef"": {
                                                            ""apiVersion"": ""v1"",
                                                            ""fieldPath"": ""metadata.namespace""
                                                        },
                                                        ""path"": ""namespace""
                                                    }
                                                ]
                                            }
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://c6afbe09647fde734aff8b2132c43ea0cb585464c92df74f1bd1c5d48239255d"",
                                ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                                ""imageID"": ""sha256:296a6d5035e2d6919249e02709a488d680ddca91357602bd65e605eac967b899"",
                                ""lastState"": {},
                                ""name"": ""coredns"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:59:07Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""10.244.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""10.244.0.2""
                            }
                        ],
                        ""qosClass"": ""Burstable"",
                        ""startTime"": ""2023-08-12T13:59:02Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""annotations"": {
                            ""kubeadm.kubernetes.io/etcd.advertise-client-urls"": ""https://172.18.0.2:2379"",
                            ""kubernetes.io/config.hash"": ""24ba8551bcc724a32d591bb02c423d92"",
                            ""kubernetes.io/config.mirror"": ""24ba8551bcc724a32d591bb02c423d92"",
                            ""kubernetes.io/config.seen"": ""2023-08-12T13:58:32.316956300Z"",
                            ""kubernetes.io/config.source"": ""file""
                        },
                        ""creationTimestamp"": ""2023-08-12T13:58:39Z"",
                        ""labels"": {
                            ""component"": ""etcd"",
                            ""tier"": ""control-plane""
                        },
                        ""name"": ""etcd-kind-control-plane"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""v1"",
                                ""controller"": true,
                                ""kind"": ""Node"",
                                ""name"": ""kind-control-plane"",
                                ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                            }
                        ],
                        ""resourceVersion"": ""492"",
                        ""uid"": ""c67de2af-ad0a-4255-9167-2b82749d50ed""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""command"": [
                                    ""etcd"",
                                    ""--advertise-client-urls=https://172.18.0.2:2379"",
                                    ""--cert-file=/etc/kubernetes/pki/etcd/server.crt"",
                                    ""--client-cert-auth=true"",
                                    ""--data-dir=/var/lib/etcd"",
                                    ""--initial-advertise-peer-urls=https://172.18.0.2:2380"",
                                    ""--initial-cluster=kind-control-plane=https://172.18.0.2:2380"",
                                    ""--key-file=/etc/kubernetes/pki/etcd/server.key"",
                                    ""--listen-client-urls=https://127.0.0.1:2379,https://172.18.0.2:2379"",
                                    ""--listen-metrics-urls=http://127.0.0.1:2381"",
                                    ""--listen-peer-urls=https://172.18.0.2:2380"",
                                    ""--name=kind-control-plane"",
                                    ""--peer-cert-file=/etc/kubernetes/pki/etcd/peer.crt"",
                                    ""--peer-client-cert-auth=true"",
                                    ""--peer-key-file=/etc/kubernetes/pki/etcd/peer.key"",
                                    ""--peer-trusted-ca-file=/etc/kubernetes/pki/etcd/ca.crt"",
                                    ""--snapshot-count=10000"",
                                    ""--trusted-ca-file=/etc/kubernetes/pki/etcd/ca.crt""
                                ],
                                ""image"": ""k8s.gcr.io/etcd:3.4.13-0"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""livenessProbe"": {
                                    ""failureThreshold"": 8,
                                    ""httpGet"": {
                                        ""host"": ""127.0.0.1"",
                                        ""path"": ""/health"",
                                        ""port"": 2381,
                                        ""scheme"": ""HTTP""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""name"": ""etcd"",
                                ""resources"": {
                                    ""requests"": {
                                        ""cpu"": ""100m"",
                                        ""ephemeral-storage"": ""100Mi"",
                                        ""memory"": ""100Mi""
                                    }
                                },
                                ""startupProbe"": {
                                    ""failureThreshold"": 24,
                                    ""httpGet"": {
                                        ""host"": ""127.0.0.1"",
                                        ""path"": ""/health"",
                                        ""port"": 2381,
                                        ""scheme"": ""HTTP""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/var/lib/etcd"",
                                        ""name"": ""etcd-data""
                                    },
                                    {
                                        ""mountPath"": ""/etc/kubernetes/pki/etcd"",
                                        ""name"": ""etcd-certs""
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""hostNetwork"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000001000,
                        ""priorityClassName"": ""system-node-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""effect"": ""NoExecute"",
                                ""operator"": ""Exists""
                            }
                        ],
                        ""volumes"": [
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/kubernetes/pki/etcd"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""etcd-certs""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/var/lib/etcd"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""etcd-data""
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:41Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:41Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://bfeae69c303511c026b0f98e0416d781b66c0cacc9df6613371b56912043ebee"",
                                ""image"": ""k8s.gcr.io/etcd:3.4.13-0"",
                                ""imageID"": ""sha256:0369cf4303ffdb467dc219990960a9baa8512a54b0ad9283eaf55bd6c0adb934"",
                                ""lastState"": {},
                                ""name"": ""etcd"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:58:16Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""172.18.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""172.18.0.2""
                            }
                        ],
                        ""qosClass"": ""Burstable"",
                        ""startTime"": ""2023-08-12T13:58:39Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                        ""generateName"": ""kindnet-"",
                        ""labels"": {
                            ""app"": ""kindnet"",
                            ""controller-revision-hash"": ""5b547684d9"",
                            ""k8s-app"": ""kindnet"",
                            ""pod-template-generation"": ""1"",
                            ""tier"": ""node""
                        },
                        ""name"": ""kindnet-g5glr"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""apps/v1"",
                                ""blockOwnerDeletion"": true,
                                ""controller"": true,
                                ""kind"": ""DaemonSet"",
                                ""name"": ""kindnet"",
                                ""uid"": ""4a92eb61-fdc5-44b3-8648-e0b8b25ab0a2""
                            }
                        ],
                        ""resourceVersion"": ""511"",
                        ""uid"": ""9bdc0a7b-a380-48dd-9319-d5190359f3a6""
                    },
                    ""spec"": {
                        ""affinity"": {
                            ""nodeAffinity"": {
                                ""requiredDuringSchedulingIgnoredDuringExecution"": {
                                    ""nodeSelectorTerms"": [
                                        {
                                            ""matchFields"": [
                                                {
                                                    ""key"": ""metadata.name"",
                                                    ""operator"": ""In"",
                                                    ""values"": [
                                                        ""kind-control-plane""
                                                    ]
                                                }
                                            ]
                                        }
                                    ]
                                }
                            }
                        },
                        ""containers"": [
                            {
                                ""env"": [
                                    {
                                        ""name"": ""HOST_IP"",
                                        ""valueFrom"": {
                                            ""fieldRef"": {
                                                ""apiVersion"": ""v1"",
                                                ""fieldPath"": ""status.hostIP""
                                            }
                                        }
                                    },
                                    {
                                        ""name"": ""POD_IP"",
                                        ""valueFrom"": {
                                            ""fieldRef"": {
                                                ""apiVersion"": ""v1"",
                                                ""fieldPath"": ""status.podIP""
                                            }
                                        }
                                    },
                                    {
                                        ""name"": ""POD_SUBNET"",
                                        ""value"": ""10.244.0.0/16""
                                    },
                                    {
                                        ""name"": ""CONTROL_PLANE_ENDPOINT"",
                                        ""value"": ""kind-control-plane:6443""
                                    }
                                ],
                                ""image"": ""docker.io/kindest/kindnetd:v20210326-1e038dc5"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""name"": ""kindnet-cni"",
                                ""resources"": {
                                    ""limits"": {
                                        ""cpu"": ""100m"",
                                        ""memory"": ""50Mi""
                                    },
                                    ""requests"": {
                                        ""cpu"": ""100m"",
                                        ""memory"": ""50Mi""
                                    }
                                },
                                ""securityContext"": {
                                    ""capabilities"": {
                                        ""add"": [
                                            ""NET_RAW"",
                                            ""NET_ADMIN""
                                        ]
                                    },
                                    ""privileged"": false
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/cni/net.d"",
                                        ""name"": ""cni-cfg""
                                    },
                                    {
                                        ""mountPath"": ""/run/xtables.lock"",
                                        ""name"": ""xtables-lock""
                                    },
                                    {
                                        ""mountPath"": ""/lib/modules"",
                                        ""name"": ""lib-modules"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                        ""name"": ""kube-api-access-twh2w"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""hostNetwork"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 0,
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""serviceAccount"": ""kindnet"",
                        ""serviceAccountName"": ""kindnet"",
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/not-ready"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/unreachable"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/disk-pressure"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/memory-pressure"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/pid-pressure"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/unschedulable"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/network-unavailable"",
                                ""operator"": ""Exists""
                            }
                        ],
                        ""volumes"": [
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/cni/net.d"",
                                    ""type"": """"
                                },
                                ""name"": ""cni-cfg""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/run/xtables.lock"",
                                    ""type"": ""FileOrCreate""
                                },
                                ""name"": ""xtables-lock""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/lib/modules"",
                                    ""type"": """"
                                },
                                ""name"": ""lib-modules""
                            },
                            {
                                ""name"": ""kube-api-access-twh2w"",
                                ""projected"": {
                                    ""defaultMode"": 420,
                                    ""sources"": [
                                        {
                                            ""serviceAccountToken"": {
                                                ""expirationSeconds"": 3607,
                                                ""path"": ""token""
                                            }
                                        },
                                        {
                                            ""configMap"": {
                                                ""items"": [
                                                    {
                                                        ""key"": ""ca.crt"",
                                                        ""path"": ""ca.crt""
                                                    }
                                                ],
                                                ""name"": ""kube-root-ca.crt""
                                            }
                                        },
                                        {
                                            ""downwardAPI"": {
                                                ""items"": [
                                                    {
                                                        ""fieldRef"": {
                                                            ""apiVersion"": ""v1"",
                                                            ""fieldPath"": ""metadata.namespace""
                                                        },
                                                        ""path"": ""namespace""
                                                    }
                                                ]
                                            }
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:50Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:50Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://2dd966e4925da5607b2c1bd0421e852d45f925875c0f0358f8f5f289812b9823"",
                                ""image"": ""docker.io/kindest/kindnetd:v20210326-1e038dc5"",
                                ""imageID"": ""sha256:6de166512aa223315ff9cfd49bd4f13aab1591cd8fc57e31270f0e4aa34129cb"",
                                ""lastState"": {},
                                ""name"": ""kindnet-cni"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:58:50Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""172.18.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""172.18.0.2""
                            }
                        ],
                        ""qosClass"": ""Guaranteed"",
                        ""startTime"": ""2023-08-12T13:58:40Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""annotations"": {
                            ""kubeadm.kubernetes.io/kube-apiserver.advertise-address.endpoint"": ""172.18.0.2:6443"",
                            ""kubernetes.io/config.hash"": ""bd1c21fe1f0ef615e0b5e41299f1be61"",
                            ""kubernetes.io/config.mirror"": ""bd1c21fe1f0ef615e0b5e41299f1be61"",
                            ""kubernetes.io/config.seen"": ""2023-08-12T13:57:57.965668600Z"",
                            ""kubernetes.io/config.source"": ""file""
                        },
                        ""creationTimestamp"": ""2023-08-12T13:58:25Z"",
                        ""labels"": {
                            ""component"": ""kube-apiserver"",
                            ""tier"": ""control-plane""
                        },
                        ""name"": ""kube-apiserver-kind-control-plane"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""v1"",
                                ""controller"": true,
                                ""kind"": ""Node"",
                                ""name"": ""kind-control-plane"",
                                ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                            }
                        ],
                        ""resourceVersion"": ""497"",
                        ""uid"": ""0db0758a-fb6c-4351-905e-0795d0cfc6ee""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""command"": [
                                    ""kube-apiserver"",
                                    ""--advertise-address=172.18.0.2"",
                                    ""--allow-privileged=true"",
                                    ""--authorization-mode=Node,RBAC"",
                                    ""--client-ca-file=/etc/kubernetes/pki/ca.crt"",
                                    ""--enable-admission-plugins=NodeRestriction"",
                                    ""--enable-bootstrap-token-auth=true"",
                                    ""--etcd-cafile=/etc/kubernetes/pki/etcd/ca.crt"",
                                    ""--etcd-certfile=/etc/kubernetes/pki/apiserver-etcd-client.crt"",
                                    ""--etcd-keyfile=/etc/kubernetes/pki/apiserver-etcd-client.key"",
                                    ""--etcd-servers=https://127.0.0.1:2379"",
                                    ""--insecure-port=0"",
                                    ""--kubelet-client-certificate=/etc/kubernetes/pki/apiserver-kubelet-client.crt"",
                                    ""--kubelet-client-key=/etc/kubernetes/pki/apiserver-kubelet-client.key"",
                                    ""--kubelet-preferred-address-types=InternalIP,ExternalIP,Hostname"",
                                    ""--proxy-client-cert-file=/etc/kubernetes/pki/front-proxy-client.crt"",
                                    ""--proxy-client-key-file=/etc/kubernetes/pki/front-proxy-client.key"",
                                    ""--requestheader-allowed-names=front-proxy-client"",
                                    ""--requestheader-client-ca-file=/etc/kubernetes/pki/front-proxy-ca.crt"",
                                    ""--requestheader-extra-headers-prefix=X-Remote-Extra-"",
                                    ""--requestheader-group-headers=X-Remote-Group"",
                                    ""--requestheader-username-headers=X-Remote-User"",
                                    ""--runtime-config="",
                                    ""--secure-port=6443"",
                                    ""--service-account-issuer=https://kubernetes.default.svc.cluster.local"",
                                    ""--service-account-key-file=/etc/kubernetes/pki/sa.pub"",
                                    ""--service-account-signing-key-file=/etc/kubernetes/pki/sa.key"",
                                    ""--service-cluster-ip-range=10.96.0.0/16"",
                                    ""--tls-cert-file=/etc/kubernetes/pki/apiserver.crt"",
                                    ""--tls-private-key-file=/etc/kubernetes/pki/apiserver.key""
                                ],
                                ""image"": ""k8s.gcr.io/kube-apiserver:v1.21.1"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""livenessProbe"": {
                                    ""failureThreshold"": 8,
                                    ""httpGet"": {
                                        ""host"": ""172.18.0.2"",
                                        ""path"": ""/livez"",
                                        ""port"": 6443,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""name"": ""kube-apiserver"",
                                ""readinessProbe"": {
                                    ""failureThreshold"": 3,
                                    ""httpGet"": {
                                        ""host"": ""172.18.0.2"",
                                        ""path"": ""/readyz"",
                                        ""port"": 6443,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""periodSeconds"": 1,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""resources"": {
                                    ""requests"": {
                                        ""cpu"": ""250m""
                                    }
                                },
                                ""startupProbe"": {
                                    ""failureThreshold"": 24,
                                    ""httpGet"": {
                                        ""host"": ""172.18.0.2"",
                                        ""path"": ""/livez"",
                                        ""port"": 6443,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/ssl/certs"",
                                        ""name"": ""ca-certs"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/etc/ca-certificates"",
                                        ""name"": ""etc-ca-certificates"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/etc/kubernetes/pki"",
                                        ""name"": ""k8s-certs"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/usr/local/share/ca-certificates"",
                                        ""name"": ""usr-local-share-ca-certificates"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/usr/share/ca-certificates"",
                                        ""name"": ""usr-share-ca-certificates"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""hostNetwork"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000001000,
                        ""priorityClassName"": ""system-node-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""effect"": ""NoExecute"",
                                ""operator"": ""Exists""
                            }
                        ],
                        ""volumes"": [
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/ssl/certs"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""ca-certs""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/ca-certificates"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""etc-ca-certificates""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/kubernetes/pki"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""k8s-certs""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/usr/local/share/ca-certificates"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""usr-local-share-ca-certificates""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/usr/share/ca-certificates"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""usr-share-ca-certificates""
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:45Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:45Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://274f0381afcd49a83ccf362f6240a31d2950f56f07ae3c825cabab8e58f83ea9"",
                                ""image"": ""k8s.gcr.io/kube-apiserver:v1.21.1"",
                                ""imageID"": ""sha256:94ffe308aeff9fd5602df5fe8bea97dc5b3efe3c53532bb2b0edf26c72d140e3"",
                                ""lastState"": {},
                                ""name"": ""kube-apiserver"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:58:10Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""172.18.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""172.18.0.2""
                            }
                        ],
                        ""qosClass"": ""Burstable"",
                        ""startTime"": ""2023-08-12T13:58:39Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""annotations"": {
                            ""kubernetes.io/config.hash"": ""46dac9a538838115821dfd9559149484"",
                            ""kubernetes.io/config.mirror"": ""46dac9a538838115821dfd9559149484"",
                            ""kubernetes.io/config.seen"": ""2023-08-12T13:58:32.316978800Z"",
                            ""kubernetes.io/config.source"": ""file""
                        },
                        ""creationTimestamp"": ""2023-08-12T13:58:39Z"",
                        ""labels"": {
                            ""component"": ""kube-controller-manager"",
                            ""tier"": ""control-plane""
                        },
                        ""name"": ""kube-controller-manager-kind-control-plane"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""v1"",
                                ""controller"": true,
                                ""kind"": ""Node"",
                                ""name"": ""kind-control-plane"",
                                ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                            }
                        ],
                        ""resourceVersion"": ""479"",
                        ""uid"": ""ddbd79ad-bbde-45d5-9d34-7a2faf6ec7da""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""command"": [
                                    ""kube-controller-manager"",
                                    ""--allocate-node-cidrs=true"",
                                    ""--authentication-kubeconfig=/etc/kubernetes/controller-manager.conf"",
                                    ""--authorization-kubeconfig=/etc/kubernetes/controller-manager.conf"",
                                    ""--bind-address=127.0.0.1"",
                                    ""--client-ca-file=/etc/kubernetes/pki/ca.crt"",
                                    ""--cluster-cidr=10.244.0.0/16"",
                                    ""--cluster-name=kind"",
                                    ""--cluster-signing-cert-file=/etc/kubernetes/pki/ca.crt"",
                                    ""--cluster-signing-key-file=/etc/kubernetes/pki/ca.key"",
                                    ""--controllers=*,bootstrapsigner,tokencleaner"",
                                    ""--enable-hostpath-provisioner=true"",
                                    ""--kubeconfig=/etc/kubernetes/controller-manager.conf"",
                                    ""--leader-elect=true"",
                                    ""--port=0"",
                                    ""--requestheader-client-ca-file=/etc/kubernetes/pki/front-proxy-ca.crt"",
                                    ""--root-ca-file=/etc/kubernetes/pki/ca.crt"",
                                    ""--service-account-private-key-file=/etc/kubernetes/pki/sa.key"",
                                    ""--service-cluster-ip-range=10.96.0.0/16"",
                                    ""--use-service-account-credentials=true""
                                ],
                                ""image"": ""k8s.gcr.io/kube-controller-manager:v1.21.1"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""livenessProbe"": {
                                    ""failureThreshold"": 8,
                                    ""httpGet"": {
                                        ""host"": ""127.0.0.1"",
                                        ""path"": ""/healthz"",
                                        ""port"": 10257,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""name"": ""kube-controller-manager"",
                                ""resources"": {
                                    ""requests"": {
                                        ""cpu"": ""200m""
                                    }
                                },
                                ""startupProbe"": {
                                    ""failureThreshold"": 24,
                                    ""httpGet"": {
                                        ""host"": ""127.0.0.1"",
                                        ""path"": ""/healthz"",
                                        ""port"": 10257,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/ssl/certs"",
                                        ""name"": ""ca-certs"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/etc/ca-certificates"",
                                        ""name"": ""etc-ca-certificates"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/usr/libexec/kubernetes/kubelet-plugins/volume/exec"",
                                        ""name"": ""flexvolume-dir""
                                    },
                                    {
                                        ""mountPath"": ""/etc/kubernetes/pki"",
                                        ""name"": ""k8s-certs"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/etc/kubernetes/controller-manager.conf"",
                                        ""name"": ""kubeconfig"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/usr/local/share/ca-certificates"",
                                        ""name"": ""usr-local-share-ca-certificates"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/usr/share/ca-certificates"",
                                        ""name"": ""usr-share-ca-certificates"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""hostNetwork"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000001000,
                        ""priorityClassName"": ""system-node-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""effect"": ""NoExecute"",
                                ""operator"": ""Exists""
                            }
                        ],
                        ""volumes"": [
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/ssl/certs"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""ca-certs""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/ca-certificates"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""etc-ca-certificates""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/usr/libexec/kubernetes/kubelet-plugins/volume/exec"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""flexvolume-dir""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/kubernetes/pki"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""k8s-certs""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/kubernetes/controller-manager.conf"",
                                    ""type"": ""FileOrCreate""
                                },
                                ""name"": ""kubeconfig""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/usr/local/share/ca-certificates"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""usr-local-share-ca-certificates""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/usr/share/ca-certificates"",
                                    ""type"": ""DirectoryOrCreate""
                                },
                                ""name"": ""usr-share-ca-certificates""
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://828e456fc66b0301194f1aa46d786a35d8f778333426d1385d0e44f7ffb6af65"",
                                ""image"": ""k8s.gcr.io/kube-controller-manager:v1.21.1"",
                                ""imageID"": ""sha256:96a295389d472f96d58764c2ed3e7418d0183f707765c21e6f310c2e163225a9"",
                                ""lastState"": {},
                                ""name"": ""kube-controller-manager"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:58:10Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""172.18.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""172.18.0.2""
                            }
                        ],
                        ""qosClass"": ""Burstable"",
                        ""startTime"": ""2023-08-12T13:58:39Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                        ""generateName"": ""kube-proxy-"",
                        ""labels"": {
                            ""controller-revision-hash"": ""6bc6858f58"",
                            ""k8s-app"": ""kube-proxy"",
                            ""pod-template-generation"": ""1""
                        },
                        ""name"": ""kube-proxy-dl8rb"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""apps/v1"",
                                ""blockOwnerDeletion"": true,
                                ""controller"": true,
                                ""kind"": ""DaemonSet"",
                                ""name"": ""kube-proxy"",
                                ""uid"": ""d1b0289e-9e17-408e-a1a1-93e240e86789""
                            }
                        ],
                        ""resourceVersion"": ""505"",
                        ""uid"": ""0d044578-178c-4d11-8c41-49648b5bd90b""
                    },
                    ""spec"": {
                        ""affinity"": {
                            ""nodeAffinity"": {
                                ""requiredDuringSchedulingIgnoredDuringExecution"": {
                                    ""nodeSelectorTerms"": [
                                        {
                                            ""matchFields"": [
                                                {
                                                    ""key"": ""metadata.name"",
                                                    ""operator"": ""In"",
                                                    ""values"": [
                                                        ""kind-control-plane""
                                                    ]
                                                }
                                            ]
                                        }
                                    ]
                                }
                            }
                        },
                        ""containers"": [
                            {
                                ""command"": [
                                    ""/usr/local/bin/kube-proxy"",
                                    ""--config=/var/lib/kube-proxy/config.conf"",
                                    ""--hostname-override=$(NODE_NAME)""
                                ],
                                ""env"": [
                                    {
                                        ""name"": ""NODE_NAME"",
                                        ""valueFrom"": {
                                            ""fieldRef"": {
                                                ""apiVersion"": ""v1"",
                                                ""fieldPath"": ""spec.nodeName""
                                            }
                                        }
                                    }
                                ],
                                ""image"": ""k8s.gcr.io/kube-proxy:v1.21.1"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""name"": ""kube-proxy"",
                                ""resources"": {},
                                ""securityContext"": {
                                    ""privileged"": true
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/var/lib/kube-proxy"",
                                        ""name"": ""kube-proxy""
                                    },
                                    {
                                        ""mountPath"": ""/run/xtables.lock"",
                                        ""name"": ""xtables-lock""
                                    },
                                    {
                                        ""mountPath"": ""/lib/modules"",
                                        ""name"": ""lib-modules"",
                                        ""readOnly"": true
                                    },
                                    {
                                        ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                        ""name"": ""kube-api-access-lrh4l"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""hostNetwork"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""nodeSelector"": {
                            ""kubernetes.io/os"": ""linux""
                        },
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000001000,
                        ""priorityClassName"": ""system-node-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""serviceAccount"": ""kube-proxy"",
                        ""serviceAccountName"": ""kube-proxy"",
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""key"": ""CriticalAddonsOnly"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/not-ready"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/unreachable"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/disk-pressure"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/memory-pressure"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/pid-pressure"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/unschedulable"",
                                ""operator"": ""Exists""
                            },
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node.kubernetes.io/network-unavailable"",
                                ""operator"": ""Exists""
                            }
                        ],
                        ""volumes"": [
                            {
                                ""configMap"": {
                                    ""defaultMode"": 420,
                                    ""name"": ""kube-proxy""
                                },
                                ""name"": ""kube-proxy""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/run/xtables.lock"",
                                    ""type"": ""FileOrCreate""
                                },
                                ""name"": ""xtables-lock""
                            },
                            {
                                ""hostPath"": {
                                    ""path"": ""/lib/modules"",
                                    ""type"": """"
                                },
                                ""name"": ""lib-modules""
                            },
                            {
                                ""name"": ""kube-api-access-lrh4l"",
                                ""projected"": {
                                    ""defaultMode"": 420,
                                    ""sources"": [
                                        {
                                            ""serviceAccountToken"": {
                                                ""expirationSeconds"": 3607,
                                                ""path"": ""token""
                                            }
                                        },
                                        {
                                            ""configMap"": {
                                                ""items"": [
                                                    {
                                                        ""key"": ""ca.crt"",
                                                        ""path"": ""ca.crt""
                                                    }
                                                ],
                                                ""name"": ""kube-root-ca.crt""
                                            }
                                        },
                                        {
                                            ""downwardAPI"": {
                                                ""items"": [
                                                    {
                                                        ""fieldRef"": {
                                                            ""apiVersion"": ""v1"",
                                                            ""fieldPath"": ""metadata.namespace""
                                                        },
                                                        ""path"": ""namespace""
                                                    }
                                                ]
                                            }
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:49Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:49Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://86e01c5aa12a1380dabd2dab1f93d02ddbf24a79d0a47fd3aa520de274d2ea4a"",
                                ""image"": ""k8s.gcr.io/kube-proxy:v1.21.1"",
                                ""imageID"": ""sha256:0e124fb3c695bdbf30fe3328ef81e268a7f884f670e67c016f5e45c2058b2538"",
                                ""lastState"": {},
                                ""name"": ""kube-proxy"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:58:49Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""172.18.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""172.18.0.2""
                            }
                        ],
                        ""qosClass"": ""BestEffort"",
                        ""startTime"": ""2023-08-12T13:58:40Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""annotations"": {
                            ""kubernetes.io/config.hash"": ""69dd939498054a211c3461b2a9cc8d26"",
                            ""kubernetes.io/config.mirror"": ""69dd939498054a211c3461b2a9cc8d26"",
                            ""kubernetes.io/config.seen"": ""2023-08-12T13:57:57.965672300Z"",
                            ""kubernetes.io/config.source"": ""file""
                        },
                        ""creationTimestamp"": ""2023-08-12T13:58:25Z"",
                        ""labels"": {
                            ""component"": ""kube-scheduler"",
                            ""tier"": ""control-plane""
                        },
                        ""name"": ""kube-scheduler-kind-control-plane"",
                        ""namespace"": ""kube-system"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""v1"",
                                ""controller"": true,
                                ""kind"": ""Node"",
                                ""name"": ""kind-control-plane"",
                                ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                            }
                        ],
                        ""resourceVersion"": ""487"",
                        ""uid"": ""b601b6c6-619f-4370-947b-784a0ae42780""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""command"": [
                                    ""kube-scheduler"",
                                    ""--authentication-kubeconfig=/etc/kubernetes/scheduler.conf"",
                                    ""--authorization-kubeconfig=/etc/kubernetes/scheduler.conf"",
                                    ""--bind-address=127.0.0.1"",
                                    ""--kubeconfig=/etc/kubernetes/scheduler.conf"",
                                    ""--leader-elect=true"",
                                    ""--port=0""
                                ],
                                ""image"": ""k8s.gcr.io/kube-scheduler:v1.21.1"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""livenessProbe"": {
                                    ""failureThreshold"": 8,
                                    ""httpGet"": {
                                        ""host"": ""127.0.0.1"",
                                        ""path"": ""/healthz"",
                                        ""port"": 10259,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""name"": ""kube-scheduler"",
                                ""resources"": {
                                    ""requests"": {
                                        ""cpu"": ""100m""
                                    }
                                },
                                ""startupProbe"": {
                                    ""failureThreshold"": 24,
                                    ""httpGet"": {
                                        ""host"": ""127.0.0.1"",
                                        ""path"": ""/healthz"",
                                        ""port"": 10259,
                                        ""scheme"": ""HTTPS""
                                    },
                                    ""initialDelaySeconds"": 10,
                                    ""periodSeconds"": 10,
                                    ""successThreshold"": 1,
                                    ""timeoutSeconds"": 15
                                },
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/kubernetes/scheduler.conf"",
                                        ""name"": ""kubeconfig"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""hostNetwork"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 2000001000,
                        ""priorityClassName"": ""system-node-critical"",
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""effect"": ""NoExecute"",
                                ""operator"": ""Exists""
                            }
                        ],
                        ""volumes"": [
                            {
                                ""hostPath"": {
                                    ""path"": ""/etc/kubernetes/scheduler.conf"",
                                    ""type"": ""FileOrCreate""
                                },
                                ""name"": ""kubeconfig""
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://b6c5d878c0b82ff2dca5dda235e9bc58799038912c56299b60d5dd52ea5d3988"",
                                ""image"": ""k8s.gcr.io/kube-scheduler:v1.21.1"",
                                ""imageID"": ""sha256:1248d2d503d37429342d5e994559f8559f35d80a31b224d4db5324816fff7206"",
                                ""lastState"": {},
                                ""name"": ""kube-scheduler"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:58:08Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""172.18.0.2"",
                        ""podIPs"": [
                            {
                                ""ip"": ""172.18.0.2""
                            }
                        ],
                        ""qosClass"": ""Burstable"",
                        ""startTime"": ""2023-08-12T13:58:39Z""
                    }
                },
                {
                    ""apiVersion"": ""v1"",
                    ""kind"": ""Pod"",
                    ""metadata"": {
                        ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                        ""generateName"": ""local-path-provisioner-547f784dff-"",
                        ""labels"": {
                            ""app"": ""local-path-provisioner"",
                            ""pod-template-hash"": ""547f784dff""
                        },
                        ""name"": ""local-path-provisioner-547f784dff-xr8n6"",
                        ""namespace"": ""local-path-storage"",
                        ""ownerReferences"": [
                            {
                                ""apiVersion"": ""apps/v1"",
                                ""blockOwnerDeletion"": true,
                                ""controller"": true,
                                ""kind"": ""ReplicaSet"",
                                ""name"": ""local-path-provisioner-547f784dff"",
                                ""uid"": ""38d73f40-e45b-453a-9fd5-fcaa1622bae0""
                            }
                        ],
                        ""resourceVersion"": ""568"",
                        ""uid"": ""f756b4be-e87d-48df-933e-8d05ea0055de""
                    },
                    ""spec"": {
                        ""containers"": [
                            {
                                ""command"": [
                                    ""local-path-provisioner"",
                                    ""--debug"",
                                    ""start"",
                                    ""--helper-image"",
                                    ""k8s.gcr.io/build-image/debian-base:v2.1.0"",
                                    ""--config"",
                                    ""/etc/config/config.json""
                                ],
                                ""env"": [
                                    {
                                        ""name"": ""POD_NAMESPACE"",
                                        ""valueFrom"": {
                                            ""fieldRef"": {
                                                ""apiVersion"": ""v1"",
                                                ""fieldPath"": ""metadata.namespace""
                                            }
                                        }
                                    }
                                ],
                                ""image"": ""docker.io/rancher/local-path-provisioner:v0.0.14"",
                                ""imagePullPolicy"": ""IfNotPresent"",
                                ""name"": ""local-path-provisioner"",
                                ""resources"": {},
                                ""terminationMessagePath"": ""/dev/termination-log"",
                                ""terminationMessagePolicy"": ""File"",
                                ""volumeMounts"": [
                                    {
                                        ""mountPath"": ""/etc/config/"",
                                        ""name"": ""config-volume""
                                    },
                                    {
                                        ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                        ""name"": ""kube-api-access-b7hrl"",
                                        ""readOnly"": true
                                    }
                                ]
                            }
                        ],
                        ""dnsPolicy"": ""ClusterFirst"",
                        ""enableServiceLinks"": true,
                        ""nodeName"": ""kind-control-plane"",
                        ""nodeSelector"": {
                            ""kubernetes.io/os"": ""linux""
                        },
                        ""preemptionPolicy"": ""PreemptLowerPriority"",
                        ""priority"": 0,
                        ""restartPolicy"": ""Always"",
                        ""schedulerName"": ""default-scheduler"",
                        ""securityContext"": {},
                        ""serviceAccount"": ""local-path-provisioner-service-account"",
                        ""serviceAccountName"": ""local-path-provisioner-service-account"",
                        ""terminationGracePeriodSeconds"": 30,
                        ""tolerations"": [
                            {
                                ""effect"": ""NoSchedule"",
                                ""key"": ""node-role.kubernetes.io/master"",
                                ""operator"": ""Equal""
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/not-ready"",
                                ""operator"": ""Exists"",
                                ""tolerationSeconds"": 300
                            },
                            {
                                ""effect"": ""NoExecute"",
                                ""key"": ""node.kubernetes.io/unreachable"",
                                ""operator"": ""Exists"",
                                ""tolerationSeconds"": 300
                            }
                        ],
                        ""volumes"": [
                            {
                                ""configMap"": {
                                    ""defaultMode"": 420,
                                    ""name"": ""local-path-config""
                                },
                                ""name"": ""config-volume""
                            },
                            {
                                ""name"": ""kube-api-access-b7hrl"",
                                ""projected"": {
                                    ""defaultMode"": 420,
                                    ""sources"": [
                                        {
                                            ""serviceAccountToken"": {
                                                ""expirationSeconds"": 3607,
                                                ""path"": ""token""
                                            }
                                        },
                                        {
                                            ""configMap"": {
                                                ""items"": [
                                                    {
                                                        ""key"": ""ca.crt"",
                                                        ""path"": ""ca.crt""
                                                    }
                                                ],
                                                ""name"": ""kube-root-ca.crt""
                                            }
                                        },
                                        {
                                            ""downwardAPI"": {
                                                ""items"": [
                                                    {
                                                        ""fieldRef"": {
                                                            ""apiVersion"": ""v1"",
                                                            ""fieldPath"": ""metadata.namespace""
                                                        },
                                                        ""path"": ""namespace""
                                                    }
                                                ]
                                            }
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    ""status"": {
                        ""conditions"": [
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                                ""status"": ""True"",
                                ""type"": ""Initialized""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                                ""status"": ""True"",
                                ""type"": ""Ready""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                                ""status"": ""True"",
                                ""type"": ""ContainersReady""
                            },
                            {
                                ""lastProbeTime"": null,
                                ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                                ""status"": ""True"",
                                ""type"": ""PodScheduled""
                            }
                        ],
                        ""containerStatuses"": [
                            {
                                ""containerID"": ""containerd://0b2116f7777be945370f8ca6f5f093957b0c0bc37ad223a14c4fd86e1fc1789c"",
                                ""image"": ""docker.io/rancher/local-path-provisioner:v0.0.14"",
                                ""imageID"": ""sha256:e422121c9c5f97623245b7e600eeb5e223ee623f21fa04da985ae71057d8d70b"",
                                ""lastState"": {},
                                ""name"": ""local-path-provisioner"",
                                ""ready"": true,
                                ""restartCount"": 0,
                                ""started"": true,
                                ""state"": {
                                    ""running"": {
                                        ""startedAt"": ""2023-08-12T13:59:08Z""
                                    }
                                }
                            }
                        ],
                        ""hostIP"": ""172.18.0.2"",
                        ""phase"": ""Running"",
                        ""podIP"": ""10.244.0.4"",
                        ""podIPs"": [
                            {
                                ""ip"": ""10.244.0.4""
                            }
                        ],
                        ""qosClass"": ""BestEffort"",
                        ""startTime"": ""2023-08-12T13:59:02Z""
                    }
                }
            ],
            ""kind"": ""List"",
            ""metadata"": {
                ""resourceVersion"": """"
            }
        }";

        return json;
         // return View();
        }

        public string Data()
        {
            string json = @"{
    ""apiVersion"": ""v1"",
    ""items"": [
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""coredns-558bd4d5db-"",
                ""labels"": {
                    ""k8s-app"": ""kube-dns"",
                    ""pod-template-hash"": ""558bd4d5db""
                },
                ""name"": ""coredns-558bd4d5db-5xxwq"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""coredns-558bd4d5db"",
                        ""uid"": ""0cc89c70-d135-4564-8f13-5ebdbb55a758""
                    }
                ],
                ""resourceVersion"": ""583"",
                ""uid"": ""a38644dd-665f-43f0-b304-1a23bcf849bc""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""args"": [
                            ""-conf"",
                            ""/etc/coredns/Corefile""
                        ],
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 5,
                            ""httpGet"": {
                                ""path"": ""/health"",
                                ""port"": 8080,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 60,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 5
                        },
                        ""name"": ""coredns"",
                        ""ports"": [
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns"",
                                ""protocol"": ""UDP""
                            },
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns-tcp"",
                                ""protocol"": ""TCP""
                            },
                            {
                                ""containerPort"": 9153,
                                ""name"": ""metrics"",
                                ""protocol"": ""TCP""
                            }
                        ],
                        ""readinessProbe"": {
                            ""failureThreshold"": 3,
                            ""httpGet"": {
                                ""path"": ""/ready"",
                                ""port"": 8181,
                                ""scheme"": ""HTTP""
                            },
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 1
                        },
                        ""resources"": {
                            ""limits"": {
                                ""memory"": ""170Mi""
                            },
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""70Mi""
                            }
                        },
                        ""securityContext"": {
                            ""allowPrivilegeEscalation"": false,
                            ""capabilities"": {
                                ""add"": [
                                    ""NET_BIND_SERVICE""
                                ],
                                ""drop"": [
                                    ""all""
                                ]
                            },
                            ""readOnlyRootFilesystem"": true
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/coredns"",
                                ""name"": ""config-volume"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-rfh87"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""Default"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000000000,
                ""priorityClassName"": ""system-cluster-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""coredns"",
                ""serviceAccountName"": ""coredns"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""key"": ""CriticalAddonsOnly"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/master""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/control-plane""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""items"": [
                                {
                                    ""key"": ""Corefile"",
                                    ""path"": ""Corefile""
                                }
                            ],
                            ""name"": ""coredns""
                        },
                        ""name"": ""config-volume""
                    },
                    {
                        ""name"": ""kube-api-access-rfh87"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:03Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:13Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:13Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://8fbc91a70879c56a5b60c7406176e6297fcc374f082a0754d25def354aa42659"",
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imageID"": ""sha256:296a6d5035e2d6919249e02709a488d680ddca91357602bd65e605eac967b899"",
                        ""lastState"": {},
                        ""name"": ""coredns"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:59:06Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.3"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.3""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:59:03Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""coredns-558bd4d5db-"",
                ""labels"": {
                    ""k8s-app"": ""kube-dns"",
                    ""pod-template-hash"": ""558bd4d5db""
                },
                ""name"": ""coredns-558bd4d5db-6zzkc"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""coredns-558bd4d5db"",
                        ""uid"": ""0cc89c70-d135-4564-8f13-5ebdbb55a758""
                    }
                ],
                ""resourceVersion"": ""572"",
                ""uid"": ""5d26f06e-28fb-4d0e-a2a5-2e4e1856c030""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""args"": [
                            ""-conf"",
                            ""/etc/coredns/Corefile""
                        ],
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 5,
                            ""httpGet"": {
                                ""path"": ""/health"",
                                ""port"": 8080,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 60,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 5
                        },
                        ""name"": ""coredns"",
                        ""ports"": [
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns"",
                                ""protocol"": ""UDP""
                            },
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns-tcp"",
                                ""protocol"": ""TCP""
                            },
                            {
                                ""containerPort"": 9153,
                                ""name"": ""metrics"",
                                ""protocol"": ""TCP""
                            }
                        ],
                        ""readinessProbe"": {
                            ""failureThreshold"": 3,
                            ""httpGet"": {
                                ""path"": ""/ready"",
                                ""port"": 8181,
                                ""scheme"": ""HTTP""
                            },
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 1
                        },
                        ""resources"": {
                            ""limits"": {
                                ""memory"": ""170Mi""
                            },
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""70Mi""
                            }
                        },
                        ""securityContext"": {
                            ""allowPrivilegeEscalation"": false,
                            ""capabilities"": {
                                ""add"": [
                                    ""NET_BIND_SERVICE""
                                ],
                                ""drop"": [
                                    ""all""
                                ]
                            },
                            ""readOnlyRootFilesystem"": true
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/coredns"",
                                ""name"": ""config-volume"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-pxg2p"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""Default"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000000000,
                ""priorityClassName"": ""system-cluster-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""coredns"",
                ""serviceAccountName"": ""coredns"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""key"": ""CriticalAddonsOnly"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/master""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/control-plane""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""items"": [
                                {
                                    ""key"": ""Corefile"",
                                    ""path"": ""Corefile""
                                }
                            ],
                            ""name"": ""coredns""
                        },
                        ""name"": ""config-volume""
                    },
                    {
                        ""name"": ""kube-api-access-pxg2p"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://c6afbe09647fde734aff8b2132c43ea0cb585464c92df74f1bd1c5d48239255d"",
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imageID"": ""sha256:296a6d5035e2d6919249e02709a488d680ddca91357602bd65e605eac967b899"",
                        ""lastState"": {},
                        ""name"": ""coredns"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:59:07Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:59:02Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubeadm.kubernetes.io/etcd.advertise-client-urls"": ""https://172.18.0.2:2379"",
                    ""kubernetes.io/config.hash"": ""24ba8551bcc724a32d591bb02c423d92"",
                    ""kubernetes.io/config.mirror"": ""24ba8551bcc724a32d591bb02c423d92"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:58:32.316956300Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:39Z"",
                ""labels"": {
                    ""component"": ""etcd"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""etcd-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""492"",
                ""uid"": ""c67de2af-ad0a-4255-9167-2b82749d50ed""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""etcd"",
                            ""--advertise-client-urls=https://172.18.0.2:2379"",
                            ""--cert-file=/etc/kubernetes/pki/etcd/server.crt"",
                            ""--client-cert-auth=true"",
                            ""--data-dir=/var/lib/etcd"",
                            ""--initial-advertise-peer-urls=https://172.18.0.2:2380"",
                            ""--initial-cluster=kind-control-plane=https://172.18.0.2:2380"",
                            ""--key-file=/etc/kubernetes/pki/etcd/server.key"",
                            ""--listen-client-urls=https://127.0.0.1:2379,https://172.18.0.2:2379"",
                            ""--listen-metrics-urls=http://127.0.0.1:2381"",
                            ""--listen-peer-urls=https://172.18.0.2:2380"",
                            ""--name=kind-control-plane"",
                            ""--peer-cert-file=/etc/kubernetes/pki/etcd/peer.crt"",
                            ""--peer-client-cert-auth=true"",
                            ""--peer-key-file=/etc/kubernetes/pki/etcd/peer.key"",
                            ""--peer-trusted-ca-file=/etc/kubernetes/pki/etcd/ca.crt"",
                            ""--snapshot-count=10000"",
                            ""--trusted-ca-file=/etc/kubernetes/pki/etcd/ca.crt""
                        ],
                        ""image"": ""k8s.gcr.io/etcd:3.4.13-0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/health"",
                                ""port"": 2381,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""etcd"",
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""ephemeral-storage"": ""100Mi"",
                                ""memory"": ""100Mi""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/health"",
                                ""port"": 2381,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/var/lib/etcd"",
                                ""name"": ""etcd-data""
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/pki/etcd"",
                                ""name"": ""etcd-certs""
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/pki/etcd"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etcd-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/var/lib/etcd"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etcd-data""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:41Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:41Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://bfeae69c303511c026b0f98e0416d781b66c0cacc9df6613371b56912043ebee"",
                        ""image"": ""k8s.gcr.io/etcd:3.4.13-0"",
                        ""imageID"": ""sha256:0369cf4303ffdb467dc219990960a9baa8512a54b0ad9283eaf55bd6c0adb934"",
                        ""lastState"": {},
                        ""name"": ""etcd"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:16Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""kindnet-"",
                ""labels"": {
                    ""app"": ""kindnet"",
                    ""controller-revision-hash"": ""5b547684d9"",
                    ""k8s-app"": ""kindnet"",
                    ""pod-template-generation"": ""1"",
                    ""tier"": ""node""
                },
                ""name"": ""kindnet-g5glr"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""DaemonSet"",
                        ""name"": ""kindnet"",
                        ""uid"": ""4a92eb61-fdc5-44b3-8648-e0b8b25ab0a2""
                    }
                ],
                ""resourceVersion"": ""511"",
                ""uid"": ""9bdc0a7b-a380-48dd-9319-d5190359f3a6""
            },
            ""spec"": {
                ""affinity"": {
                    ""nodeAffinity"": {
                        ""requiredDuringSchedulingIgnoredDuringExecution"": {
                            ""nodeSelectorTerms"": [
                                {
                                    ""matchFields"": [
                                        {
                                            ""key"": ""metadata.name"",
                                            ""operator"": ""In"",
                                            ""values"": [
                                                ""kind-control-plane""
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                },
                ""containers"": [
                    {
                        ""env"": [
                            {
                                ""name"": ""HOST_IP"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""status.hostIP""
                                    }
                                }
                            },
                            {
                                ""name"": ""POD_IP"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""status.podIP""
                                    }
                                }
                            },
                            {
                                ""name"": ""POD_SUBNET"",
                                ""value"": ""10.244.0.0/16""
                            },
                            {
                                ""name"": ""CONTROL_PLANE_ENDPOINT"",
                                ""value"": ""kind-control-plane:6443""
                            }
                        ],
                        ""image"": ""docker.io/kindest/kindnetd:v20210326-1e038dc5"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""kindnet-cni"",
                        ""resources"": {
                            ""limits"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""50Mi""
                            },
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""50Mi""
                            }
                        },
                        ""securityContext"": {
                            ""capabilities"": {
                                ""add"": [
                                    ""NET_RAW"",
                                    ""NET_ADMIN""
                                ]
                            },
                            ""privileged"": false
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/cni/net.d"",
                                ""name"": ""cni-cfg""
                            },
                            {
                                ""mountPath"": ""/run/xtables.lock"",
                                ""name"": ""xtables-lock""
                            },
                            {
                                ""mountPath"": ""/lib/modules"",
                                ""name"": ""lib-modules"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-twh2w"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 0,
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""kindnet"",
                ""serviceAccountName"": ""kindnet"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/disk-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/memory-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/pid-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/unschedulable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/network-unavailable"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/cni/net.d"",
                            ""type"": """"
                        },
                        ""name"": ""cni-cfg""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/run/xtables.lock"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""xtables-lock""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/lib/modules"",
                            ""type"": """"
                        },
                        ""name"": ""lib-modules""
                    },
                    {
                        ""name"": ""kube-api-access-twh2w"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:50Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:50Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://2dd966e4925da5607b2c1bd0421e852d45f925875c0f0358f8f5f289812b9823"",
                        ""image"": ""docker.io/kindest/kindnetd:v20210326-1e038dc5"",
                        ""imageID"": ""sha256:6de166512aa223315ff9cfd49bd4f13aab1591cd8fc57e31270f0e4aa34129cb"",
                        ""lastState"": {},
                        ""name"": ""kindnet-cni"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:50Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Guaranteed"",
                ""startTime"": ""2023-08-12T13:58:40Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubeadm.kubernetes.io/kube-apiserver.advertise-address.endpoint"": ""172.18.0.2:6443"",
                    ""kubernetes.io/config.hash"": ""bd1c21fe1f0ef615e0b5e41299f1be61"",
                    ""kubernetes.io/config.mirror"": ""bd1c21fe1f0ef615e0b5e41299f1be61"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:57:57.965668600Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:25Z"",
                ""labels"": {
                    ""component"": ""kube-apiserver"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""kube-apiserver-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""497"",
                ""uid"": ""0db0758a-fb6c-4351-905e-0795d0cfc6ee""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""kube-apiserver"",
                            ""--advertise-address=172.18.0.2"",
                            ""--allow-privileged=true"",
                            ""--authorization-mode=Node,RBAC"",
                            ""--client-ca-file=/etc/kubernetes/pki/ca.crt"",
                            ""--enable-admission-plugins=NodeRestriction"",
                            ""--enable-bootstrap-token-auth=true"",
                            ""--etcd-cafile=/etc/kubernetes/pki/etcd/ca.crt"",
                            ""--etcd-certfile=/etc/kubernetes/pki/apiserver-etcd-client.crt"",
                            ""--etcd-keyfile=/etc/kubernetes/pki/apiserver-etcd-client.key"",
                            ""--etcd-servers=https://127.0.0.1:2379"",
                            ""--insecure-port=0"",
                            ""--kubelet-client-certificate=/etc/kubernetes/pki/apiserver-kubelet-client.crt"",
                            ""--kubelet-client-key=/etc/kubernetes/pki/apiserver-kubelet-client.key"",
                            ""--kubelet-preferred-address-types=InternalIP,ExternalIP,Hostname"",
                            ""--proxy-client-cert-file=/etc/kubernetes/pki/front-proxy-client.crt"",
                            ""--proxy-client-key-file=/etc/kubernetes/pki/front-proxy-client.key"",
                            ""--requestheader-allowed-names=front-proxy-client"",
                            ""--requestheader-client-ca-file=/etc/kubernetes/pki/front-proxy-ca.crt"",
                            ""--requestheader-extra-headers-prefix=X-Remote-Extra-"",
                            ""--requestheader-group-headers=X-Remote-Group"",
                            ""--requestheader-username-headers=X-Remote-User"",
                            ""--runtime-config="",
                            ""--secure-port=6443"",
                            ""--service-account-issuer=https://kubernetes.default.svc.cluster.local"",
                            ""--service-account-key-file=/etc/kubernetes/pki/sa.pub"",
                            ""--service-account-signing-key-file=/etc/kubernetes/pki/sa.key"",
                            ""--service-cluster-ip-range=10.96.0.0/16"",
                            ""--tls-cert-file=/etc/kubernetes/pki/apiserver.crt"",
                            ""--tls-private-key-file=/etc/kubernetes/pki/apiserver.key""
                        ],
                        ""image"": ""k8s.gcr.io/kube-apiserver:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""172.18.0.2"",
                                ""path"": ""/livez"",
                                ""port"": 6443,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""kube-apiserver"",
                        ""readinessProbe"": {
                            ""failureThreshold"": 3,
                            ""httpGet"": {
                                ""host"": ""172.18.0.2"",
                                ""path"": ""/readyz"",
                                ""port"": 6443,
                                ""scheme"": ""HTTPS""
                            },
                            ""periodSeconds"": 1,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""250m""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""172.18.0.2"",
                                ""path"": ""/livez"",
                                ""port"": 6443,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/ssl/certs"",
                                ""name"": ""ca-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/ca-certificates"",
                                ""name"": ""etc-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/pki"",
                                ""name"": ""k8s-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/local/share/ca-certificates"",
                                ""name"": ""usr-local-share-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/share/ca-certificates"",
                                ""name"": ""usr-share-ca-certificates"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ssl/certs"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""ca-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etc-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/pki"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""k8s-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/local/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-local-share-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-share-ca-certificates""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:45Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:45Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://274f0381afcd49a83ccf362f6240a31d2950f56f07ae3c825cabab8e58f83ea9"",
                        ""image"": ""k8s.gcr.io/kube-apiserver:v1.21.1"",
                        ""imageID"": ""sha256:94ffe308aeff9fd5602df5fe8bea97dc5b3efe3c53532bb2b0edf26c72d140e3"",
                        ""lastState"": {},
                        ""name"": ""kube-apiserver"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:10Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubernetes.io/config.hash"": ""46dac9a538838115821dfd9559149484"",
                    ""kubernetes.io/config.mirror"": ""46dac9a538838115821dfd9559149484"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:58:32.316978800Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:39Z"",
                ""labels"": {
                    ""component"": ""kube-controller-manager"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""kube-controller-manager-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""479"",
                ""uid"": ""ddbd79ad-bbde-45d5-9d34-7a2faf6ec7da""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""kube-controller-manager"",
                            ""--allocate-node-cidrs=true"",
                            ""--authentication-kubeconfig=/etc/kubernetes/controller-manager.conf"",
                            ""--authorization-kubeconfig=/etc/kubernetes/controller-manager.conf"",
                            ""--bind-address=127.0.0.1"",
                            ""--client-ca-file=/etc/kubernetes/pki/ca.crt"",
                            ""--cluster-cidr=10.244.0.0/16"",
                            ""--cluster-name=kind"",
                            ""--cluster-signing-cert-file=/etc/kubernetes/pki/ca.crt"",
                            ""--cluster-signing-key-file=/etc/kubernetes/pki/ca.key"",
                            ""--controllers=*,bootstrapsigner,tokencleaner"",
                            ""--enable-hostpath-provisioner=true"",
                            ""--kubeconfig=/etc/kubernetes/controller-manager.conf"",
                            ""--leader-elect=true"",
                            ""--port=0"",
                            ""--requestheader-client-ca-file=/etc/kubernetes/pki/front-proxy-ca.crt"",
                            ""--root-ca-file=/etc/kubernetes/pki/ca.crt"",
                            ""--service-account-private-key-file=/etc/kubernetes/pki/sa.key"",
                            ""--service-cluster-ip-range=10.96.0.0/16"",
                            ""--use-service-account-credentials=true""
                        ],
                        ""image"": ""k8s.gcr.io/kube-controller-manager:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10257,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""kube-controller-manager"",
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""200m""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10257,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/ssl/certs"",
                                ""name"": ""ca-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/ca-certificates"",
                                ""name"": ""etc-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/libexec/kubernetes/kubelet-plugins/volume/exec"",
                                ""name"": ""flexvolume-dir""
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/pki"",
                                ""name"": ""k8s-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/controller-manager.conf"",
                                ""name"": ""kubeconfig"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/local/share/ca-certificates"",
                                ""name"": ""usr-local-share-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/share/ca-certificates"",
                                ""name"": ""usr-share-ca-certificates"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ssl/certs"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""ca-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etc-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/libexec/kubernetes/kubelet-plugins/volume/exec"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""flexvolume-dir""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/pki"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""k8s-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/controller-manager.conf"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""kubeconfig""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/local/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-local-share-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-share-ca-certificates""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://828e456fc66b0301194f1aa46d786a35d8f778333426d1385d0e44f7ffb6af65"",
                        ""image"": ""k8s.gcr.io/kube-controller-manager:v1.21.1"",
                        ""imageID"": ""sha256:96a295389d472f96d58764c2ed3e7418d0183f707765c21e6f310c2e163225a9"",
                        ""lastState"": {},
                        ""name"": ""kube-controller-manager"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:10Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""kube-proxy-"",
                ""labels"": {
                    ""controller-revision-hash"": ""6bc6858f58"",
                    ""k8s-app"": ""kube-proxy"",
                    ""pod-template-generation"": ""1""
                },
                ""name"": ""kube-proxy-dl8rb"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""DaemonSet"",
                        ""name"": ""kube-proxy"",
                        ""uid"": ""d1b0289e-9e17-408e-a1a1-93e240e86789""
                    }
                ],
                ""resourceVersion"": ""505"",
                ""uid"": ""0d044578-178c-4d11-8c41-49648b5bd90b""
            },
            ""spec"": {
                ""affinity"": {
                    ""nodeAffinity"": {
                        ""requiredDuringSchedulingIgnoredDuringExecution"": {
                            ""nodeSelectorTerms"": [
                                {
                                    ""matchFields"": [
                                        {
                                            ""key"": ""metadata.name"",
                                            ""operator"": ""In"",
                                            ""values"": [
                                                ""kind-control-plane""
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                },
                ""containers"": [
                    {
                        ""command"": [
                            ""/usr/local/bin/kube-proxy"",
                            ""--config=/var/lib/kube-proxy/config.conf"",
                            ""--hostname-override=$(NODE_NAME)""
                        ],
                        ""env"": [
                            {
                                ""name"": ""NODE_NAME"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""spec.nodeName""
                                    }
                                }
                            }
                        ],
                        ""image"": ""k8s.gcr.io/kube-proxy:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""kube-proxy"",
                        ""resources"": {},
                        ""securityContext"": {
                            ""privileged"": true
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/var/lib/kube-proxy"",
                                ""name"": ""kube-proxy""
                            },
                            {
                                ""mountPath"": ""/run/xtables.lock"",
                                ""name"": ""xtables-lock""
                            },
                            {
                                ""mountPath"": ""/lib/modules"",
                                ""name"": ""lib-modules"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-lrh4l"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""kube-proxy"",
                ""serviceAccountName"": ""kube-proxy"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""key"": ""CriticalAddonsOnly"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/disk-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/memory-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/pid-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/unschedulable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/network-unavailable"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""name"": ""kube-proxy""
                        },
                        ""name"": ""kube-proxy""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/run/xtables.lock"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""xtables-lock""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/lib/modules"",
                            ""type"": """"
                        },
                        ""name"": ""lib-modules""
                    },
                    {
                        ""name"": ""kube-api-access-lrh4l"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:49Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:49Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://86e01c5aa12a1380dabd2dab1f93d02ddbf24a79d0a47fd3aa520de274d2ea4a"",
                        ""image"": ""k8s.gcr.io/kube-proxy:v1.21.1"",
                        ""imageID"": ""sha256:0e124fb3c695bdbf30fe3328ef81e268a7f884f670e67c016f5e45c2058b2538"",
                        ""lastState"": {},
                        ""name"": ""kube-proxy"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:49Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""BestEffort"",
                ""startTime"": ""2023-08-12T13:58:40Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubernetes.io/config.hash"": ""69dd939498054a211c3461b2a9cc8d26"",
                    ""kubernetes.io/config.mirror"": ""69dd939498054a211c3461b2a9cc8d26"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:57:57.965672300Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:25Z"",
                ""labels"": {
                    ""component"": ""kube-scheduler"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""kube-scheduler-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""487"",
                ""uid"": ""b601b6c6-619f-4370-947b-784a0ae42780""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""kube-scheduler"",
                            ""--authentication-kubeconfig=/etc/kubernetes/scheduler.conf"",
                            ""--authorization-kubeconfig=/etc/kubernetes/scheduler.conf"",
                            ""--bind-address=127.0.0.1"",
                            ""--kubeconfig=/etc/kubernetes/scheduler.conf"",
                            ""--leader-elect=true"",
                            ""--port=0""
                        ],
                        ""image"": ""k8s.gcr.io/kube-scheduler:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10259,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""kube-scheduler"",
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""100m""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10259,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/kubernetes/scheduler.conf"",
                                ""name"": ""kubeconfig"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/scheduler.conf"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""kubeconfig""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://b6c5d878c0b82ff2dca5dda235e9bc58799038912c56299b60d5dd52ea5d3988"",
                        ""image"": ""k8s.gcr.io/kube-scheduler:v1.21.1"",
                        ""imageID"": ""sha256:1248d2d503d37429342d5e994559f8559f35d80a31b224d4db5324816fff7206"",
                        ""lastState"": {},
                        ""name"": ""kube-scheduler"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:08Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""local-path-provisioner-547f784dff-"",
                ""labels"": {
                    ""app"": ""local-path-provisioner"",
                    ""pod-template-hash"": ""547f784dff""
                },
                ""name"": ""local-path-provisioner-547f784dff-xr8n6"",
                ""namespace"": ""local-path-storage"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""local-path-provisioner-547f784dff"",
                        ""uid"": ""38d73f40-e45b-453a-9fd5-fcaa1622bae0""
                    }
                ],
                ""resourceVersion"": ""568"",
                ""uid"": ""f756b4be-e87d-48df-933e-8d05ea0055de""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""local-path-provisioner"",
                            ""--debug"",
                            ""start"",
                            ""--helper-image"",
                            ""k8s.gcr.io/build-image/debian-base:v2.1.0"",
                            ""--config"",
                            ""/etc/config/config.json""
                        ],
                        ""env"": [
                            {
                                ""name"": ""POD_NAMESPACE"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""metadata.namespace""
                                    }
                                }
                            }
                        ],
                        ""image"": ""docker.io/rancher/local-path-provisioner:v0.0.14"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""local-path-provisioner"",
                        ""resources"": {},
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/config/"",
                                ""name"": ""config-volume""
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-b7hrl"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 0,
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""local-path-provisioner-service-account"",
                ""serviceAccountName"": ""local-path-provisioner-service-account"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/master"",
                        ""operator"": ""Equal""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""name"": ""local-path-config""
                        },
                        ""name"": ""config-volume""
                    },
                    {
                        ""name"": ""kube-api-access-b7hrl"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://0b2116f7777be945370f8ca6f5f093957b0c0bc37ad223a14c4fd86e1fc1789c"",
                        ""image"": ""docker.io/rancher/local-path-provisioner:v0.0.14"",
                        ""imageID"": ""sha256:e422121c9c5f97623245b7e600eeb5e223ee623f21fa04da985ae71057d8d70b"",
                        ""lastState"": {},
                        ""name"": ""local-path-provisioner"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:59:08Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.4"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.4""
                    }
                ],
                ""qosClass"": ""BestEffort"",
                ""startTime"": ""2023-08-12T13:59:02Z""
            }
        }
    ],
    ""kind"": ""List"",
    ""metadata"": {
        ""resourceVersion"": """"
    }
}";
             
            var kk = this.HttpContext;
            var jj = this.HttpContext.Request.QueryString.ToString();

            return json;
        }



        public async Task<IActionResult> PostIt()
        { 
             
            var httpContent = new StringContent(JsonConvert.SerializeObject("dave"), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                // var response = await client.PostAsync("http://webapi/api/values", stringContent);
                var response = await client.PostAsync("http://kubeapi/api/kube", httpContent);
                //  Assert.True(response.IsSuccessStatusCode);
                response.EnsureSuccessStatusCode();
                ViewData["data"] = await response.Content.ReadAsStringAsync();
            }
            ViewData["json"] = @"{
    ""apiVersion"": ""v1"",
    ""items"": [
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""coredns-558bd4d5db-"",
                ""labels"": {
                    ""k8s-app"": ""kube-dns"",
                    ""pod-template-hash"": ""558bd4d5db""
                },
                ""name"": ""coredns-558bd4d5db-5xxwq"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""coredns-558bd4d5db"",
                        ""uid"": ""0cc89c70-d135-4564-8f13-5ebdbb55a758""
                    }
                ],
                ""resourceVersion"": ""583"",
                ""uid"": ""a38644dd-665f-43f0-b304-1a23bcf849bc""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""args"": [
                            ""-conf"",
                            ""/etc/coredns/Corefile""
                        ],
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 5,
                            ""httpGet"": {
                                ""path"": ""/health"",
                                ""port"": 8080,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 60,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 5
                        },
                        ""name"": ""coredns"",
                        ""ports"": [
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns"",
                                ""protocol"": ""UDP""
                            },
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns-tcp"",
                                ""protocol"": ""TCP""
                            },
                            {
                                ""containerPort"": 9153,
                                ""name"": ""metrics"",
                                ""protocol"": ""TCP""
                            }
                        ],
                        ""readinessProbe"": {
                            ""failureThreshold"": 3,
                            ""httpGet"": {
                                ""path"": ""/ready"",
                                ""port"": 8181,
                                ""scheme"": ""HTTP""
                            },
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 1
                        },
                        ""resources"": {
                            ""limits"": {
                                ""memory"": ""170Mi""
                            },
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""70Mi""
                            }
                        },
                        ""securityContext"": {
                            ""allowPrivilegeEscalation"": false,
                            ""capabilities"": {
                                ""add"": [
                                    ""NET_BIND_SERVICE""
                                ],
                                ""drop"": [
                                    ""all""
                                ]
                            },
                            ""readOnlyRootFilesystem"": true
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/coredns"",
                                ""name"": ""config-volume"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-rfh87"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""Default"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000000000,
                ""priorityClassName"": ""system-cluster-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""coredns"",
                ""serviceAccountName"": ""coredns"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""key"": ""CriticalAddonsOnly"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/master""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/control-plane""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""items"": [
                                {
                                    ""key"": ""Corefile"",
                                    ""path"": ""Corefile""
                                }
                            ],
                            ""name"": ""coredns""
                        },
                        ""name"": ""config-volume""
                    },
                    {
                        ""name"": ""kube-api-access-rfh87"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:03Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:13Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:13Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://8fbc91a70879c56a5b60c7406176e6297fcc374f082a0754d25def354aa42659"",
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imageID"": ""sha256:296a6d5035e2d6919249e02709a488d680ddca91357602bd65e605eac967b899"",
                        ""lastState"": {},
                        ""name"": ""coredns"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:59:06Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.3"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.3""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:59:03Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""coredns-558bd4d5db-"",
                ""labels"": {
                    ""k8s-app"": ""kube-dns"",
                    ""pod-template-hash"": ""558bd4d5db""
                },
                ""name"": ""coredns-558bd4d5db-6zzkc"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""coredns-558bd4d5db"",
                        ""uid"": ""0cc89c70-d135-4564-8f13-5ebdbb55a758""
                    }
                ],
                ""resourceVersion"": ""572"",
                ""uid"": ""5d26f06e-28fb-4d0e-a2a5-2e4e1856c030""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""args"": [
                            ""-conf"",
                            ""/etc/coredns/Corefile""
                        ],
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 5,
                            ""httpGet"": {
                                ""path"": ""/health"",
                                ""port"": 8080,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 60,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 5
                        },
                        ""name"": ""coredns"",
                        ""ports"": [
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns"",
                                ""protocol"": ""UDP""
                            },
                            {
                                ""containerPort"": 53,
                                ""name"": ""dns-tcp"",
                                ""protocol"": ""TCP""
                            },
                            {
                                ""containerPort"": 9153,
                                ""name"": ""metrics"",
                                ""protocol"": ""TCP""
                            }
                        ],
                        ""readinessProbe"": {
                            ""failureThreshold"": 3,
                            ""httpGet"": {
                                ""path"": ""/ready"",
                                ""port"": 8181,
                                ""scheme"": ""HTTP""
                            },
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 1
                        },
                        ""resources"": {
                            ""limits"": {
                                ""memory"": ""170Mi""
                            },
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""70Mi""
                            }
                        },
                        ""securityContext"": {
                            ""allowPrivilegeEscalation"": false,
                            ""capabilities"": {
                                ""add"": [
                                    ""NET_BIND_SERVICE""
                                ],
                                ""drop"": [
                                    ""all""
                                ]
                            },
                            ""readOnlyRootFilesystem"": true
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/coredns"",
                                ""name"": ""config-volume"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-pxg2p"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""Default"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000000000,
                ""priorityClassName"": ""system-cluster-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""coredns"",
                ""serviceAccountName"": ""coredns"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""key"": ""CriticalAddonsOnly"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/master""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/control-plane""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""items"": [
                                {
                                    ""key"": ""Corefile"",
                                    ""path"": ""Corefile""
                                }
                            ],
                            ""name"": ""coredns""
                        },
                        ""name"": ""config-volume""
                    },
                    {
                        ""name"": ""kube-api-access-pxg2p"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://c6afbe09647fde734aff8b2132c43ea0cb585464c92df74f1bd1c5d48239255d"",
                        ""image"": ""k8s.gcr.io/coredns/coredns:v1.8.0"",
                        ""imageID"": ""sha256:296a6d5035e2d6919249e02709a488d680ddca91357602bd65e605eac967b899"",
                        ""lastState"": {},
                        ""name"": ""coredns"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:59:07Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:59:02Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubeadm.kubernetes.io/etcd.advertise-client-urls"": ""https://172.18.0.2:2379"",
                    ""kubernetes.io/config.hash"": ""24ba8551bcc724a32d591bb02c423d92"",
                    ""kubernetes.io/config.mirror"": ""24ba8551bcc724a32d591bb02c423d92"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:58:32.316956300Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:39Z"",
                ""labels"": {
                    ""component"": ""etcd"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""etcd-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""492"",
                ""uid"": ""c67de2af-ad0a-4255-9167-2b82749d50ed""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""etcd"",
                            ""--advertise-client-urls=https://172.18.0.2:2379"",
                            ""--cert-file=/etc/kubernetes/pki/etcd/server.crt"",
                            ""--client-cert-auth=true"",
                            ""--data-dir=/var/lib/etcd"",
                            ""--initial-advertise-peer-urls=https://172.18.0.2:2380"",
                            ""--initial-cluster=kind-control-plane=https://172.18.0.2:2380"",
                            ""--key-file=/etc/kubernetes/pki/etcd/server.key"",
                            ""--listen-client-urls=https://127.0.0.1:2379,https://172.18.0.2:2379"",
                            ""--listen-metrics-urls=http://127.0.0.1:2381"",
                            ""--listen-peer-urls=https://172.18.0.2:2380"",
                            ""--name=kind-control-plane"",
                            ""--peer-cert-file=/etc/kubernetes/pki/etcd/peer.crt"",
                            ""--peer-client-cert-auth=true"",
                            ""--peer-key-file=/etc/kubernetes/pki/etcd/peer.key"",
                            ""--peer-trusted-ca-file=/etc/kubernetes/pki/etcd/ca.crt"",
                            ""--snapshot-count=10000"",
                            ""--trusted-ca-file=/etc/kubernetes/pki/etcd/ca.crt""
                        ],
                        ""image"": ""k8s.gcr.io/etcd:3.4.13-0"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/health"",
                                ""port"": 2381,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""etcd"",
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""ephemeral-storage"": ""100Mi"",
                                ""memory"": ""100Mi""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/health"",
                                ""port"": 2381,
                                ""scheme"": ""HTTP""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/var/lib/etcd"",
                                ""name"": ""etcd-data""
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/pki/etcd"",
                                ""name"": ""etcd-certs""
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/pki/etcd"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etcd-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/var/lib/etcd"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etcd-data""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:41Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:41Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://bfeae69c303511c026b0f98e0416d781b66c0cacc9df6613371b56912043ebee"",
                        ""image"": ""k8s.gcr.io/etcd:3.4.13-0"",
                        ""imageID"": ""sha256:0369cf4303ffdb467dc219990960a9baa8512a54b0ad9283eaf55bd6c0adb934"",
                        ""lastState"": {},
                        ""name"": ""etcd"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:16Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""kindnet-"",
                ""labels"": {
                    ""app"": ""kindnet"",
                    ""controller-revision-hash"": ""5b547684d9"",
                    ""k8s-app"": ""kindnet"",
                    ""pod-template-generation"": ""1"",
                    ""tier"": ""node""
                },
                ""name"": ""kindnet-g5glr"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""DaemonSet"",
                        ""name"": ""kindnet"",
                        ""uid"": ""4a92eb61-fdc5-44b3-8648-e0b8b25ab0a2""
                    }
                ],
                ""resourceVersion"": ""511"",
                ""uid"": ""9bdc0a7b-a380-48dd-9319-d5190359f3a6""
            },
            ""spec"": {
                ""affinity"": {
                    ""nodeAffinity"": {
                        ""requiredDuringSchedulingIgnoredDuringExecution"": {
                            ""nodeSelectorTerms"": [
                                {
                                    ""matchFields"": [
                                        {
                                            ""key"": ""metadata.name"",
                                            ""operator"": ""In"",
                                            ""values"": [
                                                ""kind-control-plane""
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                },
                ""containers"": [
                    {
                        ""env"": [
                            {
                                ""name"": ""HOST_IP"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""status.hostIP""
                                    }
                                }
                            },
                            {
                                ""name"": ""POD_IP"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""status.podIP""
                                    }
                                }
                            },
                            {
                                ""name"": ""POD_SUBNET"",
                                ""value"": ""10.244.0.0/16""
                            },
                            {
                                ""name"": ""CONTROL_PLANE_ENDPOINT"",
                                ""value"": ""kind-control-plane:6443""
                            }
                        ],
                        ""image"": ""docker.io/kindest/kindnetd:v20210326-1e038dc5"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""kindnet-cni"",
                        ""resources"": {
                            ""limits"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""50Mi""
                            },
                            ""requests"": {
                                ""cpu"": ""100m"",
                                ""memory"": ""50Mi""
                            }
                        },
                        ""securityContext"": {
                            ""capabilities"": {
                                ""add"": [
                                    ""NET_RAW"",
                                    ""NET_ADMIN""
                                ]
                            },
                            ""privileged"": false
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/cni/net.d"",
                                ""name"": ""cni-cfg""
                            },
                            {
                                ""mountPath"": ""/run/xtables.lock"",
                                ""name"": ""xtables-lock""
                            },
                            {
                                ""mountPath"": ""/lib/modules"",
                                ""name"": ""lib-modules"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-twh2w"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 0,
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""kindnet"",
                ""serviceAccountName"": ""kindnet"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/disk-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/memory-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/pid-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/unschedulable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/network-unavailable"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/cni/net.d"",
                            ""type"": """"
                        },
                        ""name"": ""cni-cfg""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/run/xtables.lock"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""xtables-lock""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/lib/modules"",
                            ""type"": """"
                        },
                        ""name"": ""lib-modules""
                    },
                    {
                        ""name"": ""kube-api-access-twh2w"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:50Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:50Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://2dd966e4925da5607b2c1bd0421e852d45f925875c0f0358f8f5f289812b9823"",
                        ""image"": ""docker.io/kindest/kindnetd:v20210326-1e038dc5"",
                        ""imageID"": ""sha256:6de166512aa223315ff9cfd49bd4f13aab1591cd8fc57e31270f0e4aa34129cb"",
                        ""lastState"": {},
                        ""name"": ""kindnet-cni"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:50Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Guaranteed"",
                ""startTime"": ""2023-08-12T13:58:40Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubeadm.kubernetes.io/kube-apiserver.advertise-address.endpoint"": ""172.18.0.2:6443"",
                    ""kubernetes.io/config.hash"": ""bd1c21fe1f0ef615e0b5e41299f1be61"",
                    ""kubernetes.io/config.mirror"": ""bd1c21fe1f0ef615e0b5e41299f1be61"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:57:57.965668600Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:25Z"",
                ""labels"": {
                    ""component"": ""kube-apiserver"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""kube-apiserver-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""497"",
                ""uid"": ""0db0758a-fb6c-4351-905e-0795d0cfc6ee""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""kube-apiserver"",
                            ""--advertise-address=172.18.0.2"",
                            ""--allow-privileged=true"",
                            ""--authorization-mode=Node,RBAC"",
                            ""--client-ca-file=/etc/kubernetes/pki/ca.crt"",
                            ""--enable-admission-plugins=NodeRestriction"",
                            ""--enable-bootstrap-token-auth=true"",
                            ""--etcd-cafile=/etc/kubernetes/pki/etcd/ca.crt"",
                            ""--etcd-certfile=/etc/kubernetes/pki/apiserver-etcd-client.crt"",
                            ""--etcd-keyfile=/etc/kubernetes/pki/apiserver-etcd-client.key"",
                            ""--etcd-servers=https://127.0.0.1:2379"",
                            ""--insecure-port=0"",
                            ""--kubelet-client-certificate=/etc/kubernetes/pki/apiserver-kubelet-client.crt"",
                            ""--kubelet-client-key=/etc/kubernetes/pki/apiserver-kubelet-client.key"",
                            ""--kubelet-preferred-address-types=InternalIP,ExternalIP,Hostname"",
                            ""--proxy-client-cert-file=/etc/kubernetes/pki/front-proxy-client.crt"",
                            ""--proxy-client-key-file=/etc/kubernetes/pki/front-proxy-client.key"",
                            ""--requestheader-allowed-names=front-proxy-client"",
                            ""--requestheader-client-ca-file=/etc/kubernetes/pki/front-proxy-ca.crt"",
                            ""--requestheader-extra-headers-prefix=X-Remote-Extra-"",
                            ""--requestheader-group-headers=X-Remote-Group"",
                            ""--requestheader-username-headers=X-Remote-User"",
                            ""--runtime-config="",
                            ""--secure-port=6443"",
                            ""--service-account-issuer=https://kubernetes.default.svc.cluster.local"",
                            ""--service-account-key-file=/etc/kubernetes/pki/sa.pub"",
                            ""--service-account-signing-key-file=/etc/kubernetes/pki/sa.key"",
                            ""--service-cluster-ip-range=10.96.0.0/16"",
                            ""--tls-cert-file=/etc/kubernetes/pki/apiserver.crt"",
                            ""--tls-private-key-file=/etc/kubernetes/pki/apiserver.key""
                        ],
                        ""image"": ""k8s.gcr.io/kube-apiserver:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""172.18.0.2"",
                                ""path"": ""/livez"",
                                ""port"": 6443,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""kube-apiserver"",
                        ""readinessProbe"": {
                            ""failureThreshold"": 3,
                            ""httpGet"": {
                                ""host"": ""172.18.0.2"",
                                ""path"": ""/readyz"",
                                ""port"": 6443,
                                ""scheme"": ""HTTPS""
                            },
                            ""periodSeconds"": 1,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""250m""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""172.18.0.2"",
                                ""path"": ""/livez"",
                                ""port"": 6443,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/ssl/certs"",
                                ""name"": ""ca-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/ca-certificates"",
                                ""name"": ""etc-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/pki"",
                                ""name"": ""k8s-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/local/share/ca-certificates"",
                                ""name"": ""usr-local-share-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/share/ca-certificates"",
                                ""name"": ""usr-share-ca-certificates"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ssl/certs"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""ca-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etc-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/pki"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""k8s-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/local/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-local-share-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-share-ca-certificates""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:45Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:45Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://274f0381afcd49a83ccf362f6240a31d2950f56f07ae3c825cabab8e58f83ea9"",
                        ""image"": ""k8s.gcr.io/kube-apiserver:v1.21.1"",
                        ""imageID"": ""sha256:94ffe308aeff9fd5602df5fe8bea97dc5b3efe3c53532bb2b0edf26c72d140e3"",
                        ""lastState"": {},
                        ""name"": ""kube-apiserver"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:10Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubernetes.io/config.hash"": ""46dac9a538838115821dfd9559149484"",
                    ""kubernetes.io/config.mirror"": ""46dac9a538838115821dfd9559149484"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:58:32.316978800Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:39Z"",
                ""labels"": {
                    ""component"": ""kube-controller-manager"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""kube-controller-manager-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""479"",
                ""uid"": ""ddbd79ad-bbde-45d5-9d34-7a2faf6ec7da""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""kube-controller-manager"",
                            ""--allocate-node-cidrs=true"",
                            ""--authentication-kubeconfig=/etc/kubernetes/controller-manager.conf"",
                            ""--authorization-kubeconfig=/etc/kubernetes/controller-manager.conf"",
                            ""--bind-address=127.0.0.1"",
                            ""--client-ca-file=/etc/kubernetes/pki/ca.crt"",
                            ""--cluster-cidr=10.244.0.0/16"",
                            ""--cluster-name=kind"",
                            ""--cluster-signing-cert-file=/etc/kubernetes/pki/ca.crt"",
                            ""--cluster-signing-key-file=/etc/kubernetes/pki/ca.key"",
                            ""--controllers=*,bootstrapsigner,tokencleaner"",
                            ""--enable-hostpath-provisioner=true"",
                            ""--kubeconfig=/etc/kubernetes/controller-manager.conf"",
                            ""--leader-elect=true"",
                            ""--port=0"",
                            ""--requestheader-client-ca-file=/etc/kubernetes/pki/front-proxy-ca.crt"",
                            ""--root-ca-file=/etc/kubernetes/pki/ca.crt"",
                            ""--service-account-private-key-file=/etc/kubernetes/pki/sa.key"",
                            ""--service-cluster-ip-range=10.96.0.0/16"",
                            ""--use-service-account-credentials=true""
                        ],
                        ""image"": ""k8s.gcr.io/kube-controller-manager:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10257,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""kube-controller-manager"",
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""200m""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10257,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/ssl/certs"",
                                ""name"": ""ca-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/ca-certificates"",
                                ""name"": ""etc-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/libexec/kubernetes/kubelet-plugins/volume/exec"",
                                ""name"": ""flexvolume-dir""
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/pki"",
                                ""name"": ""k8s-certs"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/etc/kubernetes/controller-manager.conf"",
                                ""name"": ""kubeconfig"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/local/share/ca-certificates"",
                                ""name"": ""usr-local-share-ca-certificates"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/usr/share/ca-certificates"",
                                ""name"": ""usr-share-ca-certificates"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ssl/certs"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""ca-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""etc-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/libexec/kubernetes/kubelet-plugins/volume/exec"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""flexvolume-dir""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/pki"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""k8s-certs""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/controller-manager.conf"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""kubeconfig""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/local/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-local-share-ca-certificates""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/usr/share/ca-certificates"",
                            ""type"": ""DirectoryOrCreate""
                        },
                        ""name"": ""usr-share-ca-certificates""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://828e456fc66b0301194f1aa46d786a35d8f778333426d1385d0e44f7ffb6af65"",
                        ""image"": ""k8s.gcr.io/kube-controller-manager:v1.21.1"",
                        ""imageID"": ""sha256:96a295389d472f96d58764c2ed3e7418d0183f707765c21e6f310c2e163225a9"",
                        ""lastState"": {},
                        ""name"": ""kube-controller-manager"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:10Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""kube-proxy-"",
                ""labels"": {
                    ""controller-revision-hash"": ""6bc6858f58"",
                    ""k8s-app"": ""kube-proxy"",
                    ""pod-template-generation"": ""1""
                },
                ""name"": ""kube-proxy-dl8rb"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""DaemonSet"",
                        ""name"": ""kube-proxy"",
                        ""uid"": ""d1b0289e-9e17-408e-a1a1-93e240e86789""
                    }
                ],
                ""resourceVersion"": ""505"",
                ""uid"": ""0d044578-178c-4d11-8c41-49648b5bd90b""
            },
            ""spec"": {
                ""affinity"": {
                    ""nodeAffinity"": {
                        ""requiredDuringSchedulingIgnoredDuringExecution"": {
                            ""nodeSelectorTerms"": [
                                {
                                    ""matchFields"": [
                                        {
                                            ""key"": ""metadata.name"",
                                            ""operator"": ""In"",
                                            ""values"": [
                                                ""kind-control-plane""
                                            ]
                                        }
                                    ]
                                }
                            ]
                        }
                    }
                },
                ""containers"": [
                    {
                        ""command"": [
                            ""/usr/local/bin/kube-proxy"",
                            ""--config=/var/lib/kube-proxy/config.conf"",
                            ""--hostname-override=$(NODE_NAME)""
                        ],
                        ""env"": [
                            {
                                ""name"": ""NODE_NAME"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""spec.nodeName""
                                    }
                                }
                            }
                        ],
                        ""image"": ""k8s.gcr.io/kube-proxy:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""kube-proxy"",
                        ""resources"": {},
                        ""securityContext"": {
                            ""privileged"": true
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/var/lib/kube-proxy"",
                                ""name"": ""kube-proxy""
                            },
                            {
                                ""mountPath"": ""/run/xtables.lock"",
                                ""name"": ""xtables-lock""
                            },
                            {
                                ""mountPath"": ""/lib/modules"",
                                ""name"": ""lib-modules"",
                                ""readOnly"": true
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-lrh4l"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""kube-proxy"",
                ""serviceAccountName"": ""kube-proxy"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""key"": ""CriticalAddonsOnly"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/disk-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/memory-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/pid-pressure"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/unschedulable"",
                        ""operator"": ""Exists""
                    },
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node.kubernetes.io/network-unavailable"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""name"": ""kube-proxy""
                        },
                        ""name"": ""kube-proxy""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/run/xtables.lock"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""xtables-lock""
                    },
                    {
                        ""hostPath"": {
                            ""path"": ""/lib/modules"",
                            ""type"": """"
                        },
                        ""name"": ""lib-modules""
                    },
                    {
                        ""name"": ""kube-api-access-lrh4l"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:49Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:49Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://86e01c5aa12a1380dabd2dab1f93d02ddbf24a79d0a47fd3aa520de274d2ea4a"",
                        ""image"": ""k8s.gcr.io/kube-proxy:v1.21.1"",
                        ""imageID"": ""sha256:0e124fb3c695bdbf30fe3328ef81e268a7f884f670e67c016f5e45c2058b2538"",
                        ""lastState"": {},
                        ""name"": ""kube-proxy"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:49Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""BestEffort"",
                ""startTime"": ""2023-08-12T13:58:40Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""annotations"": {
                    ""kubernetes.io/config.hash"": ""69dd939498054a211c3461b2a9cc8d26"",
                    ""kubernetes.io/config.mirror"": ""69dd939498054a211c3461b2a9cc8d26"",
                    ""kubernetes.io/config.seen"": ""2023-08-12T13:57:57.965672300Z"",
                    ""kubernetes.io/config.source"": ""file""
                },
                ""creationTimestamp"": ""2023-08-12T13:58:25Z"",
                ""labels"": {
                    ""component"": ""kube-scheduler"",
                    ""tier"": ""control-plane""
                },
                ""name"": ""kube-scheduler-kind-control-plane"",
                ""namespace"": ""kube-system"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""v1"",
                        ""controller"": true,
                        ""kind"": ""Node"",
                        ""name"": ""kind-control-plane"",
                        ""uid"": ""987bcf5d-ef54-4d60-90ff-e8d62ff889d7""
                    }
                ],
                ""resourceVersion"": ""487"",
                ""uid"": ""b601b6c6-619f-4370-947b-784a0ae42780""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""kube-scheduler"",
                            ""--authentication-kubeconfig=/etc/kubernetes/scheduler.conf"",
                            ""--authorization-kubeconfig=/etc/kubernetes/scheduler.conf"",
                            ""--bind-address=127.0.0.1"",
                            ""--kubeconfig=/etc/kubernetes/scheduler.conf"",
                            ""--leader-elect=true"",
                            ""--port=0""
                        ],
                        ""image"": ""k8s.gcr.io/kube-scheduler:v1.21.1"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""livenessProbe"": {
                            ""failureThreshold"": 8,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10259,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""name"": ""kube-scheduler"",
                        ""resources"": {
                            ""requests"": {
                                ""cpu"": ""100m""
                            }
                        },
                        ""startupProbe"": {
                            ""failureThreshold"": 24,
                            ""httpGet"": {
                                ""host"": ""127.0.0.1"",
                                ""path"": ""/healthz"",
                                ""port"": 10259,
                                ""scheme"": ""HTTPS""
                            },
                            ""initialDelaySeconds"": 10,
                            ""periodSeconds"": 10,
                            ""successThreshold"": 1,
                            ""timeoutSeconds"": 15
                        },
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/kubernetes/scheduler.conf"",
                                ""name"": ""kubeconfig"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""hostNetwork"": true,
                ""nodeName"": ""kind-control-plane"",
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 2000001000,
                ""priorityClassName"": ""system-node-critical"",
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoExecute"",
                        ""operator"": ""Exists""
                    }
                ],
                ""volumes"": [
                    {
                        ""hostPath"": {
                            ""path"": ""/etc/kubernetes/scheduler.conf"",
                            ""type"": ""FileOrCreate""
                        },
                        ""name"": ""kubeconfig""
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:40Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:58:39Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://b6c5d878c0b82ff2dca5dda235e9bc58799038912c56299b60d5dd52ea5d3988"",
                        ""image"": ""k8s.gcr.io/kube-scheduler:v1.21.1"",
                        ""imageID"": ""sha256:1248d2d503d37429342d5e994559f8559f35d80a31b224d4db5324816fff7206"",
                        ""lastState"": {},
                        ""name"": ""kube-scheduler"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:58:08Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""172.18.0.2"",
                ""podIPs"": [
                    {
                        ""ip"": ""172.18.0.2""
                    }
                ],
                ""qosClass"": ""Burstable"",
                ""startTime"": ""2023-08-12T13:58:39Z""
            }
        },
        {
            ""apiVersion"": ""v1"",
            ""kind"": ""Pod"",
            ""metadata"": {
                ""creationTimestamp"": ""2023-08-12T13:58:40Z"",
                ""generateName"": ""local-path-provisioner-547f784dff-"",
                ""labels"": {
                    ""app"": ""local-path-provisioner"",
                    ""pod-template-hash"": ""547f784dff""
                },
                ""name"": ""local-path-provisioner-547f784dff-xr8n6"",
                ""namespace"": ""local-path-storage"",
                ""ownerReferences"": [
                    {
                        ""apiVersion"": ""apps/v1"",
                        ""blockOwnerDeletion"": true,
                        ""controller"": true,
                        ""kind"": ""ReplicaSet"",
                        ""name"": ""local-path-provisioner-547f784dff"",
                        ""uid"": ""38d73f40-e45b-453a-9fd5-fcaa1622bae0""
                    }
                ],
                ""resourceVersion"": ""568"",
                ""uid"": ""f756b4be-e87d-48df-933e-8d05ea0055de""
            },
            ""spec"": {
                ""containers"": [
                    {
                        ""command"": [
                            ""local-path-provisioner"",
                            ""--debug"",
                            ""start"",
                            ""--helper-image"",
                            ""k8s.gcr.io/build-image/debian-base:v2.1.0"",
                            ""--config"",
                            ""/etc/config/config.json""
                        ],
                        ""env"": [
                            {
                                ""name"": ""POD_NAMESPACE"",
                                ""valueFrom"": {
                                    ""fieldRef"": {
                                        ""apiVersion"": ""v1"",
                                        ""fieldPath"": ""metadata.namespace""
                                    }
                                }
                            }
                        ],
                        ""image"": ""docker.io/rancher/local-path-provisioner:v0.0.14"",
                        ""imagePullPolicy"": ""IfNotPresent"",
                        ""name"": ""local-path-provisioner"",
                        ""resources"": {},
                        ""terminationMessagePath"": ""/dev/termination-log"",
                        ""terminationMessagePolicy"": ""File"",
                        ""volumeMounts"": [
                            {
                                ""mountPath"": ""/etc/config/"",
                                ""name"": ""config-volume""
                            },
                            {
                                ""mountPath"": ""/var/run/secrets/kubernetes.io/serviceaccount"",
                                ""name"": ""kube-api-access-b7hrl"",
                                ""readOnly"": true
                            }
                        ]
                    }
                ],
                ""dnsPolicy"": ""ClusterFirst"",
                ""enableServiceLinks"": true,
                ""nodeName"": ""kind-control-plane"",
                ""nodeSelector"": {
                    ""kubernetes.io/os"": ""linux""
                },
                ""preemptionPolicy"": ""PreemptLowerPriority"",
                ""priority"": 0,
                ""restartPolicy"": ""Always"",
                ""schedulerName"": ""default-scheduler"",
                ""securityContext"": {},
                ""serviceAccount"": ""local-path-provisioner-service-account"",
                ""serviceAccountName"": ""local-path-provisioner-service-account"",
                ""terminationGracePeriodSeconds"": 30,
                ""tolerations"": [
                    {
                        ""effect"": ""NoSchedule"",
                        ""key"": ""node-role.kubernetes.io/master"",
                        ""operator"": ""Equal""
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/not-ready"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    },
                    {
                        ""effect"": ""NoExecute"",
                        ""key"": ""node.kubernetes.io/unreachable"",
                        ""operator"": ""Exists"",
                        ""tolerationSeconds"": 300
                    }
                ],
                ""volumes"": [
                    {
                        ""configMap"": {
                            ""defaultMode"": 420,
                            ""name"": ""local-path-config""
                        },
                        ""name"": ""config-volume""
                    },
                    {
                        ""name"": ""kube-api-access-b7hrl"",
                        ""projected"": {
                            ""defaultMode"": 420,
                            ""sources"": [
                                {
                                    ""serviceAccountToken"": {
                                        ""expirationSeconds"": 3607,
                                        ""path"": ""token""
                                    }
                                },
                                {
                                    ""configMap"": {
                                        ""items"": [
                                            {
                                                ""key"": ""ca.crt"",
                                                ""path"": ""ca.crt""
                                            }
                                        ],
                                        ""name"": ""kube-root-ca.crt""
                                    }
                                },
                                {
                                    ""downwardAPI"": {
                                        ""items"": [
                                            {
                                                ""fieldRef"": {
                                                    ""apiVersion"": ""v1"",
                                                    ""fieldPath"": ""metadata.namespace""
                                                },
                                                ""path"": ""namespace""
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    }
                ]
            },
            ""status"": {
                ""conditions"": [
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""Initialized""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""Ready""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:09Z"",
                        ""status"": ""True"",
                        ""type"": ""ContainersReady""
                    },
                    {
                        ""lastProbeTime"": null,
                        ""lastTransitionTime"": ""2023-08-12T13:59:02Z"",
                        ""status"": ""True"",
                        ""type"": ""PodScheduled""
                    }
                ],
                ""containerStatuses"": [
                    {
                        ""containerID"": ""containerd://0b2116f7777be945370f8ca6f5f093957b0c0bc37ad223a14c4fd86e1fc1789c"",
                        ""image"": ""docker.io/rancher/local-path-provisioner:v0.0.14"",
                        ""imageID"": ""sha256:e422121c9c5f97623245b7e600eeb5e223ee623f21fa04da985ae71057d8d70b"",
                        ""lastState"": {},
                        ""name"": ""local-path-provisioner"",
                        ""ready"": true,
                        ""restartCount"": 0,
                        ""started"": true,
                        ""state"": {
                            ""running"": {
                                ""startedAt"": ""2023-08-12T13:59:08Z""
                            }
                        }
                    }
                ],
                ""hostIP"": ""172.18.0.2"",
                ""phase"": ""Running"",
                ""podIP"": ""10.244.0.4"",
                ""podIPs"": [
                    {
                        ""ip"": ""10.244.0.4""
                    }
                ],
                ""qosClass"": ""BestEffort"",
                ""startTime"": ""2023-08-12T13:59:02Z""
            }
        }
    ],
    ""kind"": ""List"",
    ""metadata"": {
        ""resourceVersion"": """"
    }
}";

            #endregion
            Thread.Sleep(50000);
            return View();
        }


        public IActionResult Nodes()
        {
            return View();
        }


        public IActionResult ClusterInfo()
        {
            return View();
        }


        public  IActionResult Privacy()
        {
            return View();
        }

 


        public IActionResult Global()
        {
            return View();
        }

        public async Task<string> GetNameSpaces()
        {
           // List<NameSpaceClass> ls = new List<NameSpaceClass>();
            

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://KubeApi/api/Kube/ListNamespace");
                //  Assert.True(response.IsSuccessStatusCode);
                response.EnsureSuccessStatusCode();
                var jk = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
               // NameSpaceClass nameSpaceClass = new NameSpaceClass();
               // nameSpaceClass.NameSpace = "boo";
                
                return jk;
            }

        }

        public async Task<string> GetInfoPods()
        {

            //var httpContent = new StringContent(JsonConvert.SerializeObject("dave"), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://KubeApi/api/Kube/ListNamespace");
                //  Assert.True(response.IsSuccessStatusCode);
                response.EnsureSuccessStatusCode();
                var jk = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
                return jk;
            }

             
            //return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


/*  Polly
 *  //public async Task<ContentResult> CallApi()
        //public IActionResult CallApi()
        public async Task<IActionResult> CallApi()
        {
         
            using (var httpClient = new HttpClient())
            {
                var maxRetryAttempts = 3;
                var pauseBetweenFailures = TimeSpan.FromSeconds(2);

                var retryPolicy = Policy
                    .Handle<HttpRequestException>()
                    .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

                await retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await httpClient
                    .GetAsync("http://localhost:5001/weatherforecast");
                    //response.EnsureSuccessStatusCode();
                    ViewData["json"] = response;
                });
                 
                 
            }
             

             return View();
 
        }
*/