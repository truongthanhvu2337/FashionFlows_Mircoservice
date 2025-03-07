
FROM golang:1.24 AS build
WORKDIR /app

COPY FashionFlows.Services/Notification/go.mod FashionFlows.Services/Notification/go.sum ./
RUN go mod download


COPY FashionFlows.Services/Notification/ ./


RUN go build -o app ./cmd


FROM alpine:latest AS final
WORKDIR /app
RUN apk add --no-cache libc6-compat

COPY --from=build /app/app ./
RUN chmod +x app

EXPOSE 8080
ENTRYPOINT ["./app"]
