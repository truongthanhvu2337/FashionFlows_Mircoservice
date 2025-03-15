package main

import (
	"log"
	"notificationservice/config"
	"notificationservice/internal/consumer"
	"notificationservice/internal/handler"
	"notificationservice/internal/repository"
	"notificationservice/internal/server"
	"notificationservice/internal/service"

	"github.com/gin-gonic/gin"
)

func main() {
	config.InitDB()
	conn, ch, err := config.ConnectRabbitMQ()
	if err != nil {
		log.Printf("Could not connect to RabbitMQ: %v", err)
	}
	defer conn.Close()
	defer ch.Close()

	notificationRepo := repository.NewNotificationRepository(config.DB)
	notificationService := service.NewNotificationService(notificationRepo)
	notificationHandler := handler.NewNotificationHandler(notificationService)

	go func() {
		consumer.ConsumeOrderFailedEvent(ch, notificationRepo)
	}()

	go func() {
		consumer.ConsumeCompletedEvent(ch, notificationRepo)
	}()

	router := gin.Default()
	server.SetupRoutes(router, notificationHandler)
	

	router.Run(":8080")
}
