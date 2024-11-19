FROM prom/prometheus:latest as image-prometheus-base
COPY ./prometheus.yml /etc/prometheus

VOLUME ./data
EXPOSE 9090