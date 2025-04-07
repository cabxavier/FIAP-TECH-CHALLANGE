FROM prom/prometheus:latest AS image-prometheus-base
COPY ./prometheus.yml /etc/prometheus

VOLUME ./data
EXPOSE 9090