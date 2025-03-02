package server

import (
	"notificationservice/internal/handler"

	"github.com/gin-gonic/gin"
)

func SetupRoutes(router *gin.Engine, notificationHandler *handler.NotificationHandler) {
	api := router.Group("api/v1/notification")
	{
		api.POST("/", notificationHandler.CreateNotification)
		api.GET("/:id", notificationHandler.GetNotificationByID)
		api.GET("/", notificationHandler.GetAllNotifications)
		api.PUT("/:id/read", notificationHandler.MarkAsRead)
		api.DELETE("/:id", notificationHandler.DeleteNotification)
	}
}