# 1. Dùng image Go chính thức
FROM golang:1.24 AS build
WORKDIR /app

# 2. Copy module files trước để cache dependency
COPY FashionFlows.Services/Notification/go.mod FashionFlows.Services/Notification/go.sum ./
RUN go mod download

# 3. Copy toàn bộ mã nguồn vào container
COPY FashionFlows.Services/Notification/ ./

# 4. Build ứng dụng từ thư mục cmd/
RUN go build -o app ./cmd

# 5. Dùng alpine cho container chạy nhẹ hơn
FROM alpine:latest AS final
WORKDIR /app
RUN apk add --no-cache libc6-compat

# 6. Copy file binary đã build sang container final
COPY --from=build /app/app ./
RUN chmod +x app

# 7. Mở cổng 8080 và chạy ứng dụng
EXPOSE 8080
ENTRYPOINT ["./app"]
