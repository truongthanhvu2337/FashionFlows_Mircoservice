package config

import (
	"log"

	"notificationservice/internal/model"

	"gorm.io/driver/sqlserver"
	"gorm.io/gorm"
)

var DB *gorm.DB

func InitDB() {
	db, err := gorm.Open(sqlserver.Open("sqlserver://sa:12345@host.docker.internal:1433?database=FashionFlows.Notification&trustservercertificate=true"), &gorm.Config{})
	if err != nil {
		log.Println(err)
	}

	db.AutoMigrate(&model.Notification{})
	log.Println("SQL Server connected successfully!")
	DB = db
}
