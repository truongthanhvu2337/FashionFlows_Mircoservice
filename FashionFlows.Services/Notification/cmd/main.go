package main

import (
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

	notificationRepo := repository.NewNotificationRepository(config.DB)
	notificationService := service.NewNotificationService(notificationRepo)
	notificationHandler := handler.NewNotificationHandler(notificationService)

	go func() {
		consumer.ConsumeOrderFailedEvent(notificationRepo)
	}()

	go func() {
		consumer.ConsumeCompletedEvent(notificationRepo)
	}()

	router := gin.Default()
	server.SetupRoutes(router, notificationHandler)
	

	router.Run(":8080")
}
