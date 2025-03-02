package model

import (
	"time"
)

type Notification struct {
	ID        int 		`json:"id"`
	UserID    int 		`json:"userId"`
	Message   string    `json:"message"`
	IsRead    bool      `json:"isRead"`
	CreatedAt time.Time `json:"createdAt" gorm:"autoCreateTime"`
}