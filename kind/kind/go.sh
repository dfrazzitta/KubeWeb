#!/bin/bash

kind delete cluster

kind create cluster --config ingressconfig.yaml

kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.13.7/config/manifests/metallb-native.yaml

sleep 60
docker network inspect -f '{{.IPAM.Config}}' kind
kubectl apply -f lb.yaml


kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml

sleep 60

kubectl apply -f https://kind.sigs.k8s.io/examples/ingress/usage.yaml

echo "Thanks for waiting."

sleep 60s

curl localhost/foo/hostname


echo "Thanks for waiting."