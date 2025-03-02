package main

import (
	"notificationservice/config"
	"notificationservice/internal/consumer"
	"notificationservice/internal/handler"
	"notificationservice/internal/repository"
	"notificationservice/internal/server"
	"notificationservice/internal/service"

	_ "notificationservice/cmd/docs"

	"github.com/gin-gonic/gin"
	swaggerFiles "github.com/swaggo/files"
	ginSwagger "github.com/swaggo/gin-swagger"
)

func main() {
	config.InitDB()

	notificationRepo := repository.NewNotificationRepository(config.DB)
	notificationService := service.NewNotificationService(notificationRepo)
	notificationHandler := handler.NewNotificationHandler(notificationService)

	go func() {
		consumer.ConsumeOrderFailedEvent()
	}()

	router := gin.Default()
	server.SetupRoutes(router, notificationHandler)
	router.GET("/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler))
	

	router.Run(":8080")
}
